using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioData : MonoBehaviour
{

    public GameObject ButtonInventario;
    public GameObject ButtonDialogo;
    public GameObject ButtonCriaturas;
    public GameObject ButtonVoltar;

    public GameObject InventarioTab;
    public GameObject DialogoTab;
    public GameObject CriaturasTab;

    public float distIn;
    public float distCre;
    public float distDia;

    private float distOriginCre;
    private float distOriginDia;
    private float distOriginVol;

    void Start()
    {
        distOriginCre = ButtonCriaturas.transform.position.y;
        distOriginDia = ButtonDialogo.transform.position.y;
        distOriginVol = ButtonVoltar.transform.position.y;
    }

    public void Inventario()
    {
        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, distOriginDia);
        ButtonCriaturas.transform.position = new Vector2(ButtonCriaturas.transform.position.x, distOriginCre);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, distOriginVol);

        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, ButtonDialogo.transform.position.y + distIn);
        ButtonCriaturas.transform.position = new Vector2(ButtonCriaturas.transform.position.x, ButtonCriaturas.transform.position.y + distIn);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, ButtonVoltar.transform.position.y + distIn);
        InventarioTab.SetActive(true);
        DialogoTab.SetActive(false);
        CriaturasTab.SetActive(false);
    }

    public void Criaturas()
    {
        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, distOriginDia);
        ButtonCriaturas.transform.position = new Vector2(ButtonCriaturas.transform.position.x, distOriginCre);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, distOriginVol);

        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, ButtonDialogo.transform.position.y + distCre);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, ButtonVoltar.transform.position.y + distCre);
        InventarioTab.SetActive(false);
        DialogoTab.SetActive(false);
        CriaturasTab.SetActive(true);
    }
    public void Dialogo()
    {
        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, distOriginDia);
        ButtonCriaturas.transform.position = new Vector2(ButtonCriaturas.transform.position.x, distOriginCre);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, distOriginVol);

        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, ButtonVoltar.transform.position.y + distDia);
        InventarioTab.SetActive(false);
        DialogoTab.SetActive(true);
        CriaturasTab.SetActive(false);
    }

    public void Voltar()
    {
        ButtonDialogo.transform.position = new Vector2(ButtonDialogo.transform.position.x, distOriginDia);
        ButtonCriaturas.transform.position = new Vector2(ButtonCriaturas.transform.position.x, distOriginCre);
        ButtonVoltar.transform.position = new Vector2(ButtonVoltar.transform.position.x, distOriginVol);

        InventarioTab.SetActive(false);
        DialogoTab.SetActive(false);
        CriaturasTab.SetActive(false);
    }
}
