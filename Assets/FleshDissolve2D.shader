// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit/FleshDissolve2D"
{
	Properties
	{
		_MainTex("", 2D) = "white" {}
		_EffectRadius("Sphere radius", Range( 0 , 50)) = 0
		_SphereHardness("Sphere Hardness", Float) = 0
		_EffectPosition("Sphere position", Vector) = (0,0,0,0)
		_Range("Range", Float) = 4
		_Tiling("Tiling", Vector) = (5,5,0,0)
		_Step("Step", Float) = 0
		_Colorrange("Color range", Float) = 5
		_Primarycolor("Cor mudanca", Color) = (0,0,0,0)
		_Noisescale("Noise scale", Float) = 1
		_MainColor("MainColor", Color) = (1,1,1,1)
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
			CBUFFER_START( UnityPerMaterial )
			float4 _MainColor;
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float _Range;
			float2 _Tiling;
			float _Noisescale;
			float4 _Primarycolor;
			float _Colorrange;
			float _Step;
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
				float4 tex2DNode4 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_85_0 = ( _MainColor * tex2DNode4 );
				float3 ase_worldPos = IN.ase_texcoord3.xyz;
				float3 objToWorld15 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_16_0 = ( objToWorld15 + _EffectPosition );
				float3 temp_output_5_0_g9 = ( ( ase_worldPos - temp_output_16_0 ) / _EffectRadius );
				float dotResult8_g9 = dot( temp_output_5_0_g9 , temp_output_5_0_g9 );
				float clampResult10_g9 = clamp( dotResult8_g9 , 0.0 , 1.0 );
				float temp_output_6_0 = pow( clampResult10_g9 , _SphereHardness );
				float temp_output_25_0 = ( _EffectRadius + _Range );
				float3 temp_output_5_0_g8 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_25_0 );
				float dotResult8_g8 = dot( temp_output_5_0_g8 , temp_output_5_0_g8 );
				float clampResult10_g8 = clamp( dotResult8_g8 , 0.0 , 1.0 );
				float temp_output_24_0 = pow( clampResult10_g8 , _SphereHardness );
				float2 uv018 = IN.texCoord0.xy * _Tiling + float2( 0,0 );
				float2 panner35 = ( 1.0 * _Time.y * float2( 0,0 ) + uv018);
				float simplePerlin2D17 = snoise( panner35*_Noisescale );
				simplePerlin2D17 = simplePerlin2D17*0.5 + 0.5;
				float temp_output_29_0 = ( tex2DNode4.a * ( temp_output_6_0 * ( ( temp_output_24_0 * ( temp_output_24_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) ) );
				float temp_output_39_0 = ( temp_output_25_0 + _Colorrange );
				float3 temp_output_5_0_g6 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_39_0 );
				float dotResult8_g6 = dot( temp_output_5_0_g6 , temp_output_5_0_g6 );
				float clampResult10_g6 = clamp( dotResult8_g6 , 0.0 , 1.0 );
				float temp_output_38_0 = pow( clampResult10_g6 , _SphereHardness );
				float4 temp_cast_0 = (temp_output_38_0).xxxx;
				float4 temp_output_57_0 = ( ( _Primarycolor * step( ( ( temp_output_38_0 * ( temp_output_38_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) , _Step ) ) - temp_cast_0 );
				float4 blendOpSrc101 = ( temp_output_85_0 * temp_output_29_0 );
				float4 blendOpDest101 = temp_output_57_0;
				float4 lerpBlendMode101 = lerp(blendOpDest101,(( blendOpDest101 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest101 ) * ( 1.0 - blendOpSrc101 ) ) : ( 2.0 * blendOpDest101 * blendOpSrc101 ) ),0.46);
				
				float4 Color = ( saturate( lerpBlendMode101 ));
				float Mask = temp_output_29_0;
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
			CBUFFER_START( UnityPerMaterial )
			float4 _MainColor;
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float _Range;
			float2 _Tiling;
			float _Noisescale;
			float4 _Primarycolor;
			float _Colorrange;
			float _Step;
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
				float4 tex2DNode4 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_85_0 = ( _MainColor * tex2DNode4 );
				float3 ase_worldPos = IN.ase_texcoord5.xyz;
				float3 objToWorld15 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_16_0 = ( objToWorld15 + _EffectPosition );
				float3 temp_output_5_0_g9 = ( ( ase_worldPos - temp_output_16_0 ) / _EffectRadius );
				float dotResult8_g9 = dot( temp_output_5_0_g9 , temp_output_5_0_g9 );
				float clampResult10_g9 = clamp( dotResult8_g9 , 0.0 , 1.0 );
				float temp_output_6_0 = pow( clampResult10_g9 , _SphereHardness );
				float temp_output_25_0 = ( _EffectRadius + _Range );
				float3 temp_output_5_0_g8 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_25_0 );
				float dotResult8_g8 = dot( temp_output_5_0_g8 , temp_output_5_0_g8 );
				float clampResult10_g8 = clamp( dotResult8_g8 , 0.0 , 1.0 );
				float temp_output_24_0 = pow( clampResult10_g8 , _SphereHardness );
				float2 uv018 = IN.texCoord0.xy * _Tiling + float2( 0,0 );
				float2 panner35 = ( 1.0 * _Time.y * float2( 0,0 ) + uv018);
				float simplePerlin2D17 = snoise( panner35*_Noisescale );
				simplePerlin2D17 = simplePerlin2D17*0.5 + 0.5;
				float temp_output_29_0 = ( tex2DNode4.a * ( temp_output_6_0 * ( ( temp_output_24_0 * ( temp_output_24_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) ) );
				float temp_output_39_0 = ( temp_output_25_0 + _Colorrange );
				float3 temp_output_5_0_g6 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_39_0 );
				float dotResult8_g6 = dot( temp_output_5_0_g6 , temp_output_5_0_g6 );
				float clampResult10_g6 = clamp( dotResult8_g6 , 0.0 , 1.0 );
				float temp_output_38_0 = pow( clampResult10_g6 , _SphereHardness );
				float4 temp_cast_0 = (temp_output_38_0).xxxx;
				float4 temp_output_57_0 = ( ( _Primarycolor * step( ( ( temp_output_38_0 * ( temp_output_38_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) , _Step ) ) - temp_cast_0 );
				float4 blendOpSrc101 = ( temp_output_85_0 * temp_output_29_0 );
				float4 blendOpDest101 = temp_output_57_0;
				float4 lerpBlendMode101 = lerp(blendOpDest101,(( blendOpDest101 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest101 ) * ( 1.0 - blendOpSrc101 ) ) : ( 2.0 * blendOpDest101 * blendOpSrc101 ) ),0.46);
				
				float4 Color = ( saturate( lerpBlendMode101 ));
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
			CBUFFER_START( UnityPerMaterial )
			float4 _MainColor;
			float4 _MainTex_ST;
			float3 _EffectPosition;
			float _EffectRadius;
			float _SphereHardness;
			float _Range;
			float2 _Tiling;
			float _Noisescale;
			float4 _Primarycolor;
			float _Colorrange;
			float _Step;
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
				float4 tex2DNode4 = tex2D( _MainTex, uv_MainTex );
				float4 temp_output_85_0 = ( _MainColor * tex2DNode4 );
				float3 ase_worldPos = IN.ase_texcoord2.xyz;
				float3 objToWorld15 = mul( GetObjectToWorldMatrix(), float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 temp_output_16_0 = ( objToWorld15 + _EffectPosition );
				float3 temp_output_5_0_g9 = ( ( ase_worldPos - temp_output_16_0 ) / _EffectRadius );
				float dotResult8_g9 = dot( temp_output_5_0_g9 , temp_output_5_0_g9 );
				float clampResult10_g9 = clamp( dotResult8_g9 , 0.0 , 1.0 );
				float temp_output_6_0 = pow( clampResult10_g9 , _SphereHardness );
				float temp_output_25_0 = ( _EffectRadius + _Range );
				float3 temp_output_5_0_g8 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_25_0 );
				float dotResult8_g8 = dot( temp_output_5_0_g8 , temp_output_5_0_g8 );
				float clampResult10_g8 = clamp( dotResult8_g8 , 0.0 , 1.0 );
				float temp_output_24_0 = pow( clampResult10_g8 , _SphereHardness );
				float2 uv018 = IN.texCoord0.xy * _Tiling + float2( 0,0 );
				float2 panner35 = ( 1.0 * _Time.y * float2( 0,0 ) + uv018);
				float simplePerlin2D17 = snoise( panner35*_Noisescale );
				simplePerlin2D17 = simplePerlin2D17*0.5 + 0.5;
				float temp_output_29_0 = ( tex2DNode4.a * ( temp_output_6_0 * ( ( temp_output_24_0 * ( temp_output_24_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) ) );
				float temp_output_39_0 = ( temp_output_25_0 + _Colorrange );
				float3 temp_output_5_0_g6 = ( ( ase_worldPos - temp_output_16_0 ) / temp_output_39_0 );
				float dotResult8_g6 = dot( temp_output_5_0_g6 , temp_output_5_0_g6 );
				float clampResult10_g6 = clamp( dotResult8_g6 , 0.0 , 1.0 );
				float temp_output_38_0 = pow( clampResult10_g6 , _SphereHardness );
				float4 temp_cast_0 = (temp_output_38_0).xxxx;
				float4 temp_output_57_0 = ( ( _Primarycolor * step( ( ( temp_output_38_0 * ( temp_output_38_0 - simplePerlin2D17 ) ) + simplePerlin2D17 ) , _Step ) ) - temp_cast_0 );
				float4 blendOpSrc101 = ( temp_output_85_0 * temp_output_29_0 );
				float4 blendOpDest101 = temp_output_57_0;
				float4 lerpBlendMode101 = lerp(blendOpDest101,(( blendOpDest101 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest101 ) * ( 1.0 - blendOpSrc101 ) ) : ( 2.0 * blendOpDest101 * blendOpSrc101 ) ),0.46);
				
				float4 Color = ( saturate( lerpBlendMode101 ));

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
303;73;1184;600;616.6456;536.6016;1.778111;True;False
Node;AmplifyShaderEditor.CommentaryNode;80;-1793.84,971.0372;Inherit;False;2133.823;662.1454;Edge Color;11;55;37;42;41;57;43;38;56;47;19;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2539.83,839.5037;Inherit;False;Property;_Range;Range;4;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-2559.21,572.1828;Inherit;False;Property;_EffectRadius;Sphere radius;1;0;Create;False;0;0;False;0;0;0;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;19;-1706.407,899.1959;Inherit;False;Property;_Tiling;Tiling;5;0;Create;True;0;0;False;0;5,5;5,5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-2329.163,777.5135;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-2520.26,1024.17;Inherit;False;Property;_Colorrange;Color range;8;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;82;-1783.925,455.5192;Inherit;False;1504.6;493.9274;Secondary Mask;7;24;30;31;33;76;35;17;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;9;-2716.881,241.8676;Inherit;False;Property;_EffectPosition;Sphere position;3;0;Create;False;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1490.864,884.1093;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TransformPositionNode;15;-2834.145,36.72619;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;76;-1188.762,624.9489;Inherit;False;Property;_Noisescale;Noise scale;14;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-2447.162,196.99;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-2602.764,674.243;Inherit;False;Property;_SphereHardness;Sphere Hardness;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;39;-2323.806,987.6636;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;35;-1166.929,726.4787;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;17;-1005.154,614.6495;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;24;-1733.925,560.405;Inherit;True;SphereMask;-1;;8;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;38;-1743.84,1315.536;Inherit;True;SphereMask;-1;;6;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;41;-973.7244,1500.183;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;30;-745.897,590.7751;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-606.4014,505.5192;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-770.279,1440.56;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;81;-1920.189,127.8801;Inherit;False;1759.103;303;Primary mask;3;34;6;98;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-562.5439,1177.518;Inherit;False;Property;_Step;Step;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;6;-1870.189,177.8801;Inherit;True;SphereMask;-1;;9;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;5;-880.4865,-239.3491;Inherit;True;Property;_MainTex;;0;0;Create;False;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-556.676,1276.536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-433.3248,674.4205;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;47;-257.6114,1021.037;Inherit;False;Property;_Primarycolor;Cor mudanca;10;0;Create;False;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;55;-351.7262,1181.833;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-330.0861,179.9413;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;84;-421.681,-290.6548;Inherit;False;Property;_MainColor;MainColor;15;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-499.5837,-100.3527;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;25.09869,80.78246;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-27.09883,1283.247;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;15.07416,-86.54257;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;110;-248.6484,477.0404;Inherit;False;945.6858;404.294;Distortion;4;90;86;96;89;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;392.2065,-54.21572;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;57;165.9825,1231.699;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;79;-1787.932,1661.966;Inherit;False;2390.355;705.557;Red color;13;66;64;73;75;63;65;58;72;59;62;61;74;60;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;44.4049,529.1292;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-550.7681,1967.466;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;72;-15.92484,2101.073;Inherit;True;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;5.25;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;103;939.5469,841.6483;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;104;722.981,1050.292;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;73;433.4232,1927.013;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;102;723.0099,960.7792;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;64;171.8904,1922.628;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-246.0798,2159.524;Inherit;False;Property;_Fresnelscale;Fresnel scale;13;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-256.0798,2252.523;Inherit;False;Property;_Fresnelpower;Fresnel power;12;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-764.3709,2131.489;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;71;1221.371,439.3587;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;66;-251.7034,1711.966;Inherit;False;Property;_Secondcolor;Second color;11;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-2230.874,1577.19;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;86;147.7193,766.3334;Inherit;False;Property;_Distortionammount;Distortion ammount;16;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;101;1089.41,-20.83692;Inherit;False;Overlay;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.46;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-2537.123,1216.35;Inherit;False;Property;_Secondarycolorrange;Secondary color range;9;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-21.19081,1974.177;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;98;-331.1009,334.647;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;58;-1737.932,2006.465;Inherit;True;SphereMask;-1;;7;988803ee12caf5f4690caee3c8c4a5bb;0;3;15;FLOAT3;0,0,0;False;14;FLOAT;0;False;12;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;89;528.0373,527.0402;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;90;-198.6484,646.9467;Inherit;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;105;1229.235,919.4253;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-557.636,1868.448;Inherit;False;Property;_Secondstep;Second step;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;62;-345.8182,1872.762;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;59;-967.8163,2191.112;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;114;1279.349,-235.7015;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Forward;0;2;Sprite Forward;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;113;1279.349,-235.7015;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;1;New Amplify Shader;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Normal;0;1;Sprite Normal;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=NormalsRendering;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;112;1340.043,-117.05;Float;False;True;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;Unlit/FleshDissolve2D;199187dac283dbe4a8cb1ea611d70c58;True;Sprite Lit;0;0;Sprite Lit;6;False;False;False;True;2;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;0;0;True;2;5;False;-1;10;False;-1;3;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;LightMode=Universal2D;False;0;Hidden/InternalErrorShader;0;0;Standard;1;Vertex Position;1;0;3;True;True;True;False;;0
WireConnection;25;0;7;0
WireConnection;25;1;26;0
WireConnection;18;0;19;0
WireConnection;16;0;15;0
WireConnection;16;1;9;0
WireConnection;39;0;25;0
WireConnection;39;1;40;0
WireConnection;35;0;18;0
WireConnection;17;0;35;0
WireConnection;17;1;76;0
WireConnection;24;15;16;0
WireConnection;24;14;25;0
WireConnection;24;12;8;0
WireConnection;38;15;16;0
WireConnection;38;14;39;0
WireConnection;38;12;8;0
WireConnection;41;0;38;0
WireConnection;41;1;17;0
WireConnection;30;0;24;0
WireConnection;30;1;17;0
WireConnection;31;0;24;0
WireConnection;31;1;30;0
WireConnection;42;0;38;0
WireConnection;42;1;41;0
WireConnection;6;15;16;0
WireConnection;6;14;7;0
WireConnection;6;12;8;0
WireConnection;43;0;42;0
WireConnection;43;1;17;0
WireConnection;33;0;31;0
WireConnection;33;1;17;0
WireConnection;55;0;43;0
WireConnection;55;1;37;0
WireConnection;34;0;6;0
WireConnection;34;1;33;0
WireConnection;4;0;5;0
WireConnection;29;0;4;4
WireConnection;29;1;34;0
WireConnection;56;0;47;0
WireConnection;56;1;55;0
WireConnection;85;0;84;0
WireConnection;85;1;4;0
WireConnection;46;0;85;0
WireConnection;46;1;29;0
WireConnection;57;0;56;0
WireConnection;57;1;38;0
WireConnection;96;0;98;0
WireConnection;96;1;90;0
WireConnection;61;0;60;0
WireConnection;61;1;17;0
WireConnection;72;2;74;0
WireConnection;72;3;75;0
WireConnection;103;0;85;0
WireConnection;103;1;102;0
WireConnection;104;0;73;0
WireConnection;73;0;64;0
WireConnection;73;1;72;0
WireConnection;102;0;57;0
WireConnection;64;0;65;0
WireConnection;64;1;58;0
WireConnection;60;0;58;0
WireConnection;60;1;59;0
WireConnection;71;0;85;0
WireConnection;71;1;105;0
WireConnection;68;0;39;0
WireConnection;68;1;67;0
WireConnection;101;0;46;0
WireConnection;101;1;57;0
WireConnection;65;0;66;0
WireConnection;65;1;62;0
WireConnection;98;0;6;0
WireConnection;58;15;16;0
WireConnection;58;14;68;0
WireConnection;58;12;8;0
WireConnection;89;0;96;0
WireConnection;89;1;86;0
WireConnection;105;0;103;0
WireConnection;105;1;104;0
WireConnection;62;0;61;0
WireConnection;62;1;63;0
WireConnection;59;0;58;0
WireConnection;59;1;17;0
WireConnection;112;1;101;0
WireConnection;112;2;29;0
ASEEND*/
//CHKSM=BC451633C9C2C7C64A60CE695740BA51A78BE7BD