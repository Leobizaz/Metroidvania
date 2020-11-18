using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPrologo : MonoBehaviour
{
    public GameObject fade;

    private void OnEnable()
    {
        fade.SetActive(true);
        Invoke("LoadScene", 1.5f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("VideoPrologo");
    }

}
