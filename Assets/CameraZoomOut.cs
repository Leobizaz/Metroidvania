using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomOut : MonoBehaviour
{
    public CinemachineVirtualCamera CM_Cam;
    public float orthographic_size = 4;
    float storedSize;
    float defaultSize;
    bool playerIN;
    bool stop;

    private void Start()
    {
        defaultSize = CM_Cam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        if (playerIN)
        {
            CM_Cam.m_Lens.OrthographicSize = Mathf.MoveTowards(storedSize, orthographic_size, 0.3f * Time.deltaTime);
        }
        else if(!playerIN && !stop)
        {
            CM_Cam.m_Lens.OrthographicSize = Mathf.MoveTowards(CM_Cam.m_Lens.OrthographicSize, defaultSize, 0.3f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            storedSize = CM_Cam.m_Lens.OrthographicSize;
            playerIN = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            stop = false;
            Invoke("StopUpdating", 3);
            playerIN = false;
        }
    }

    void StopUpdating()
    {
        stop = true;
    }



}
