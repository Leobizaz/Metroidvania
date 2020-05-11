using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    public static CameraShake current;

    public static float ShakeDuration;          
    public static float ShakeAmplitude;         
    public static float ShakeFrequency;
    public static bool Shaking = false;
    private float ShakeElapsedTime = 0f;
    public static bool CanShake = true;
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    /// <summary>
    /// //////////////////////////////Pra chamar ele, é só colocar CameraShake.ShakeCamera(X, Y, Z); em algum script///////////////////////////////
    /// </summary>
    void Start()
    {
        current = this;
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

   
    void Update()
    {
        
        if (Shaking == true)
        {
            ShakeElapsedTime = ShakeDuration;
            Invoke("CancelShake", ShakeDuration);
        }

       
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
           
            if (ShakeElapsedTime > 0)
            {
                
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
                
            }
        }

        
    }
    public void ShakeCamera(float Duration, float Amplitude, float Frequency)
    {
        CanShake = true;
        if (CanShake == true)
        {
            ShakeDuration = Duration;
            ShakeAmplitude = Amplitude;
            ShakeFrequency = Frequency;
            Shaking = true;
            Invoke("CancelShake", Duration);
        }
        CanShake = false;
    }

    void CancelShake()
    {
        CanShake = false;
        Shaking = false;
    }
}
