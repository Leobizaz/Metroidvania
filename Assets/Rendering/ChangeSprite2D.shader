// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ChangeSprite2D"
{
	Properties
	{
		_MainTex("Default platform", 2D) = "white" {}
		_EffectRadius("Sphere radius", Range( 0 , 50)) = 0
		_SphereHardness("Sphere Hardness", Float) = 1
		_NoiseHardness("Noise Hardness", Float) = 0.67
		_EffectPosition("Sphere position", Vector) = (0,0,0,0)
		_NoiseScale("Noise Scale", Float) = 2.07
		_mask("mask", 2D) = "white" {}
		_Range("Range", Float) = 1
		_Hardness("Hardness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }

		Cull Off
		HLSLINCLUDE
		#pragma target 2.0
		ENDHLSL

		
		Pass
		{
			Name "Sprite Lit"
			Tags { "LightMode"="Universal2D" }
			
			Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 70108

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_0
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_1
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_2
			#pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_3

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITELIT

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
			
			#if USE_SHAPE_LIGHT_TYPE_0
			SHAPE_LIGHT(0)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_1
			SHAPE_LIGHT(1)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_2
			SHAPE_LIGHT(2)
			#endif

			#if USE_SHAPE_LIGHT_TYPE_3
			SHAPE_LIGHT(3)
			#endif

			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

			

			sampler2D _MainTex;
			sampler2D _mask;
			CBUFFER_START( UnityPerMaterial )
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float4 _mask_ST;
			float _Range;
			float _NoiseHardness;
			float _NoiseScale;
			float _Hardness;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float4 screenPosition : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D(_AlphaTex); SAMPLER(sampler_AlphaTex);
				float _EnableAlphaTexture;
			#endif

			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord3.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;
				o.screenPosition = ComputeScreenPos( o.clipPos, _ProjectionParams.x );
				return o;
			}

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 uv_MainTex = IN.texCoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode5 = tex2D( _MainTex, uv_MainTex );
				float3 ase_worldPos = IN.ase_texcoord3.xyz;
				float3 objToWorld9 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_10_0 = ( objToWorld9 + _EffectPosition );
				float3 temp_output_5_0_g2 = ( ( ase_worldPos - temp_output_10_0 ) / _EffectRadius );
				float dotResult8_g2 = dot( temp_output_5_0_g2 , temp_output_5_0_g2 );
				float clampResult10_g2 = clamp( dotResult8_g2 , 0.0 , 1.0 );
				float temp_output_19_0 = saturate( ( 1.0 - pow( clampResult10_g2 , _SphereHardness ) ) );
				float4 temp_cast_0 = (temp_output_19_0).xxxx;
				float2 uv_mask = IN.texCoord0.xy * _mask_ST.xy + _mask_ST.zw;
				float4 tex2DNode33 = tex2D( _mask, uv_mask );
				float3 temp_output_5_0_g3 = ( ( ase_worldPos - temp_output_10_0 ) / ( _EffectRadius + _Range ) );
				float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
				float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
				float2 uv027 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner36 = ( 1.0 * _Time.y * float2( 0,0 ) + uv027);
				float simplePerlin2D26 = snoise( panner36*_NoiseScale );
				simplePerlin2D26 = simplePerlin2D26*0.5 + 0.5;
				float4 temp_cast_1 = (( pow( clampResult10_g3 , _NoiseHardness ) - simplePerlin2D26 )).xxxx;
				float4 temp_cast_2 = (temp_output_19_0).xxxx;
				float4 lerpResult50 = lerp( ( ( tex2DNode33 - temp_cast_1 ) + temp_output_19_0 ) , temp_cast_2 , float4( 0,0,0,0 ));
				float4 temp_output_98_0 = ( saturate( ( ( temp_output_19_0 * step( temp_cast_0 , lerpResult50 ) ) * _Hardness ) ) + float4( 0,0,0,0 ) );
				float4 lerpResult17 = lerp( tex2DNode5 , float4( 0,0,0,0 ) , temp_output_98_0);
				
				float lerpResult21 = lerp( tex2DNode5.a , 0.0 , temp_output_98_0.r);
				
				float4 Color = saturate( lerpResult17 );
				float Mask = lerpResult21;
				float3 Normal = float3( 0, 0, 1 );

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D(_AlphaTex, sampler_AlphaTex, IN.texCoord0.xy);
					Color.a = lerp ( Color.a, alpha.r, _EnableAlphaTexture);
				#endif
				
				Color *= IN.color;

				return CombinedShapeLightShared( Color, Mask, IN.screenPosition.xy / IN.screenPosition.w );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Sprite Normal"
			Tags { "LightMode"="NormalsRendering" }
			
			Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 70108

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITENORMAL

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
			
			

			sampler2D _MainTex;
			sampler2D _mask;
			CBUFFER_START( UnityPerMaterial )
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float4 _mask_ST;
			float _Range;
			float _NoiseHardness;
			float _NoiseScale;
			float _Hardness;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float3 normalWS : TEXCOORD2;
				float4 tangentWS : TEXCOORD3;
				float3 bitangentWS : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord5.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;
				v.tangent.xyz = v.tangent.xyz;

				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;

				float3 normalWS = TransformObjectToWorldNormal( v.normal );
				o.normalWS = NormalizeNormalPerVertex( normalWS );
				float4 tangentWS = float4( TransformObjectToWorldDir( v.tangent.xyz ), v.tangent.w );
				o.tangentWS = normalize( tangentWS );
				o.bitangentWS = cross( normalWS, tangentWS.xyz ) * tangentWS.w;
				return o;
			}

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 uv_MainTex = IN.texCoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode5 = tex2D( _MainTex, uv_MainTex );
				float3 ase_worldPos = IN.ase_texcoord5.xyz;
				float3 objToWorld9 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_10_0 = ( objToWorld9 + _EffectPosition );
				float3 temp_output_5_0_g2 = ( ( ase_worldPos - temp_output_10_0 ) / _EffectRadius );
				float dotResult8_g2 = dot( temp_output_5_0_g2 , temp_output_5_0_g2 );
				float clampResult10_g2 = clamp( dotResult8_g2 , 0.0 , 1.0 );
				float temp_output_19_0 = saturate( ( 1.0 - pow( clampResult10_g2 , _SphereHardness ) ) );
				float4 temp_cast_0 = (temp_output_19_0).xxxx;
				float2 uv_mask = IN.texCoord0.xy * _mask_ST.xy + _mask_ST.zw;
				float4 tex2DNode33 = tex2D( _mask, uv_mask );
				float3 temp_output_5_0_g3 = ( ( ase_worldPos - temp_output_10_0 ) / ( _EffectRadius + _Range ) );
				float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
				float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
				float2 uv027 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner36 = ( 1.0 * _Time.y * float2( 0,0 ) + uv027);
				float simplePerlin2D26 = snoise( panner36*_NoiseScale );
				simplePerlin2D26 = simplePerlin2D26*0.5 + 0.5;
				float4 temp_cast_1 = (( pow( clampResult10_g3 , _NoiseHardness ) - simplePerlin2D26 )).xxxx;
				float4 temp_cast_2 = (temp_output_19_0).xxxx;
				float4 lerpResult50 = lerp( ( ( tex2DNode33 - temp_cast_1 ) + temp_output_19_0 ) , temp_cast_2 , float4( 0,0,0,0 ));
				float4 temp_output_98_0 = ( saturate( ( ( temp_output_19_0 * step( temp_cast_0 , lerpResult50 ) ) * _Hardness ) ) + float4( 0,0,0,0 ) );
				float4 lerpResult17 = lerp( tex2DNode5 , float4( 0,0,0,0 ) , temp_output_98_0);
				
				float4 Color = saturate( lerpResult17 );
				float3 Normal = float3( 0, 0, 1 );
				
				Color *= IN.color;

				return NormalsRenderingShared( Color, Normal, IN.tangentWS.xyz, IN.bitangentWS, IN.normalWS);
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Sprite Forward"
			Tags { "LightMode"="UniversalForward" }

			Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZTest LEqual
			ZWrite Off
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define ASE_SRP_VERSION 70108

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA

			#define _SURFACE_TYPE_TRANSPARENT 1
			#define SHADERPASS_SPRITEFORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			

			sampler2D _MainTex;
			sampler2D _mask;
			CBUFFER_START( UnityPerMaterial )
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float4 _mask_ST;
			float _Range;
			float _NoiseHardness;
			float _NoiseScale;
			float _Hardness;
			CBUFFER_END


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				float4 texCoord0 : TEXCOORD0;
				float4 color : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			#if ETC1_EXTERNAL_ALPHA
				TEXTURE2D( _AlphaTex ); SAMPLER( sampler_AlphaTex );
				float _EnableAlphaTexture;
			#endif

			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			

			VertexOutput vert( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float3 ase_worldPos = mul(GetObjectToWorldMatrix(), v.vertex).xyz;
				o.ase_texcoord2.xyz = ase_worldPos;
				
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.normal = v.normal;

				VertexPositionInputs vertexInput = GetVertexPositionInputs( v.vertex.xyz );

				o.texCoord0 = v.uv0;
				o.color = v.color;
				o.clipPos = vertexInput.positionCS;

				return o;
			}

			half4 frag( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 uv_MainTex = IN.texCoord0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode5 = tex2D( _MainTex, uv_MainTex );
				float3 ase_worldPos = IN.ase_texcoord2.xyz;
				float3 objToWorld9 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_10_0 = ( objToWorld9 + _EffectPosition );
				float3 temp_output_5_0_g2 = ( ( ase_worldPos - temp_output_10_0 ) / _EffectRadius );
				float dotResult8_g2 = dot( temp_output_5_0_g2 , temp_output_5_0_g2 );
				float clampResult10_g2 = clamp( dotResult8_g2 , 0.0 , 1.0 );
				float temp_output_19_0 = saturate( ( 1.0 - pow( clampResult10_g2 , _SphereHardness ) ) );
				float4 temp_cast_0 = (temp_output_19_0).xxxx;
				float2 uv_mask = IN.texCoord0.xy * _mask_ST.xy + _mask_ST.zw;
				float4 tex2DNode33 = tex2D( _mask, uv_mask );
				float3 temp_output_5_0_g3 = ( ( ase_worldPos - temp_output_10_0 ) / ( _EffectRadius + _Range ) );
				float dotResult8_g3 = dot( temp_output_5_0_g3 , temp_output_5_0_g3 );
				float clampResult10_g3 = clamp( dotResult8_g3 , 0.0 , 1.0 );
				float2 uv027 = IN.texCoord0.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner36 = ( 1.0 * _Time.y * float2( 0,0 ) + uv027);
				float simplePerlin2D26 = snoise( panner36*_NoiseScale );
				simplePerlin2D26 = simplePerlin2D26*0.5 + 0.5;
				float4 temp_cast_1 = (( pow( clampResult10_g3 , _NoiseHardness ) - simplePerlin2D26 )).xxxx;
				float4 temp_cast_2 = (temp_output_19_0).xxxx;
				float4 lerpResult50 = lerp( ( ( tex2DNode33 - temp_cast_1 ) + temp_output_19_0 ) , temp_cast_2 , float4( 0,0,0,0 ));
				float4 temp_output_98_0 = ( saturate( ( ( temp_output_19_0 * step( temp_cast_0 , lerpResult50 ) ) * _Hardness ) ) + float4( 0,0,0,0 ) );
				float4 lerpResult17 = lerp( tex2DNode5 , float4( 0,0,0,0 ) , temp_output_98_0);
				
				float4 Color = saturate( lerpResult17 );

				#if ETC1_EXTERNAL_ALPHA
					float4 alpha = SAMPLE_TEXTURE2D( _AlphaTex, sampler_AlphaTex, IN.texCoord0.xy );
					Color.a = lerp( Color.a, alpha.r, _EnableAlphaTexture );
				#endif

				Color *= IN.color;

				return Color;
			}

			ENDHLSL
		}
		
	}
	CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=17700
303;73;1184;600;-807.8373;287.0817;1;True;False
Node;AmplifyShaderEditor.Vector3Node;8;-1965.08,909.6095;Inherit;False;Property;_EffectPosition;Sphere position;7;0;Create;False;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-1597.496,1712.578;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TransformPositionNode;9;-2034.273,1641.377;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;11;-1937.809,1084.174;Inherit;False;Property;_EffectRadius;Sphere radius;3;0;Create;False;0;0;False;0;0;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-1834.277,1308.658;Inherit;False;Property;_Range;Range;11;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-2720.626,1549.016;Inherit;False;Property;_SphereHardness;Sphere Hardness;4;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-2709.954,1669.08;Inherit;False;Property;_NoiseHardness;Noise Hardness;5;0;Create;False;0;0;False;0;0.67;0.67;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-1293.162,1862.844;Inherit;False;Property;_NoiseScale;Noise Scale;8;0;Create;True;0;0;False;0;2.07;2.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;36;-1287.026,1703.937;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-1603.499,893.8596;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-1595.202,1209.857;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;26;-1059.351,1702.734;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;7;-1273.068,944.375;Inherit;True;SphereMask;-1;;2;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;22;-1234.619,1187.272;Inherit;True;SphereMask;-1;;3;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;18;-264.7078,839.0032;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;33;-128.0337,992.979;Inherit;True;Property;_mask;mask;9;0;Create;True;0;0;False;0;-1;None;e39fb1e6834a1a649ba980665a287a8f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;29;-470.5039,1225.615;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;34;187.3157,1121.07;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;19;-73.20318,781.2587;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;345.9355,839.8949;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;50;531.8033,734.7092;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;44;798.1207,579.9764;Inherit;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;1054.746,474.488;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;63;840.2373,815.3285;Inherit;False;Property;_Hardness;Hardness;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;1279.606,597.2158;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;3;-744.4694,-171.8189;Inherit;True;Property;_MainTex;Default platform;0;0;Create;False;0;0;False;0;None;bc873cbef86719a44a4ce4c44cca8734;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SaturateNode;64;1424.611,587.2297;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;98;1666.04,824.4435;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;5;-511.4268,-159.4545;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;17;1607.529,-177.7171;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;77;-353.671,2156.845;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;88;1429.209,1600.996;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;21;1547.406,11.07604;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;1284.204,1610.982;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;965.7828,1792.33;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector3Node;92;-2013.134,2281.087;Inherit;False;Property;_SecondEffectPosition1;Second Sphere position;6;0;Create;False;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;83;442.8401,2052.551;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;256.9723,2157.737;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;81;98.35251,2438.912;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;79;-559.4671,2543.457;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;75;-1362.031,2262.217;Inherit;True;SphereMask;-1;;4;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;76;-1323.582,2505.114;Inherit;True;SphereMask;-1;;5;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT;0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;94;-1651.553,2265.337;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;96;-1657.732,2592.037;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;-2000.339,2466.354;Inherit;False;Property;_SecondEffectRadius1;Second Sphere Radius;2;0;Create;False;0;0;False;0;0;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1896.807,2690.838;Inherit;False;Property;_SecondRange1;Second Range;10;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;84;709.1575,1897.818;Inherit;True;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;80;-162.1664,2099.101;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;4;-765.6588,90.64919;Inherit;True;Property;_SecondTex;Changed platform;1;0;Create;False;0;0;False;0;None;02e42bf50f199aa498797cb0c3ba607c;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SaturateNode;20;1825.214,-163.1393;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;6;-529.6268,156.4456;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;69;1898.986,-83.60992;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Normal;0;1;Sprite Normal;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=NormalsRendering;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;70;1898.986,-83.60992;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Forward;0;2;Sprite Forward;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;68;2023.986,-124.6099;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;ChangeSprite2D;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Lit;0;0;Sprite Lit;6;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=Universal2D;False;0;Hidden/InternalErrorShader;0;0;Standard;1;Vertex Position;1;0;3;True;True;True;False;;0
WireConnection;36;0;27;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;41;0;11;0
WireConnection;41;1;42;0
WireConnection;26;0;36;0
WireConnection;26;1;28;0
WireConnection;7;15;10;0
WireConnection;7;14;11;0
WireConnection;7;12;12;0
WireConnection;22;15;10;0
WireConnection;22;14;41;0
WireConnection;22;12;25;0
WireConnection;18;0;7;0
WireConnection;29;0;22;0
WireConnection;29;1;26;0
WireConnection;34;0;33;0
WireConnection;34;1;29;0
WireConnection;19;0;18;0
WireConnection;47;0;34;0
WireConnection;47;1;19;0
WireConnection;50;0;47;0
WireConnection;50;1;19;0
WireConnection;44;0;19;0
WireConnection;44;1;50;0
WireConnection;60;0;19;0
WireConnection;60;1;44;0
WireConnection;62;0;60;0
WireConnection;62;1;63;0
WireConnection;64;0;62;0
WireConnection;98;0;64;0
WireConnection;5;0;3;0
WireConnection;17;0;5;0
WireConnection;17;2;98;0
WireConnection;77;0;75;0
WireConnection;88;0;87;0
WireConnection;21;0;5;4
WireConnection;21;2;98;0
WireConnection;87;0;85;0
WireConnection;87;1;63;0
WireConnection;85;0;80;0
WireConnection;85;1;84;0
WireConnection;83;0;82;0
WireConnection;83;1;80;0
WireConnection;82;0;81;0
WireConnection;82;1;80;0
WireConnection;81;0;33;0
WireConnection;81;1;79;0
WireConnection;79;0;76;0
WireConnection;79;1;26;0
WireConnection;75;15;94;0
WireConnection;75;14;97;0
WireConnection;75;12;12;0
WireConnection;76;14;96;0
WireConnection;76;12;25;0
WireConnection;94;0;9;0
WireConnection;94;1;92;0
WireConnection;96;0;97;0
WireConnection;96;1;95;0
WireConnection;84;0;80;0
WireConnection;84;1;83;0
WireConnection;80;0;77;0
WireConnection;20;0;17;0
WireConnection;6;0;4;0
WireConnection;68;1;20;0
WireConnection;68;2;21;0
ASEEND*/
//CHKSM=C536401B430A88CDF85945E9AC265E859665BFF5