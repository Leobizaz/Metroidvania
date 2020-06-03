using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCrystal : MonoBehaviour
{
    private Material mat;
    private SpriteRenderer rend;
    public Camera cam;
    RenderTexture renderTex;

    [ExecuteInEditMode]

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        mat = rend.material;
        renderTex = new RenderTexture(500, 500, 0, RenderTextureFormat.ARGB32);
        renderTex.wrapMode = TextureWrapMode.Repeat;
        renderTex.Create();
        cam.targetTexture = renderTex;

        mat.SetTexture("_RenderTex", renderTex);

    }


}
