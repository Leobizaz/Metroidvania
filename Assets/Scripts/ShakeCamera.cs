using UnityEngine;
using System;
using Cinemachine;
public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera current;

    public CinemachineVirtualCamera v_camera;
    CinemachineBasicMultiChannelPerlin v_cameraNoise;
    bool shaking;
    float shakeElapsedTime;
    public float shakeDuration;
    public float shakeAmplitude;
    public float shakeFrequency;

    private void Awake()
    {
        current = this;
        v_cameraNoise = v_camera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shakeElapsedTime > 0)
        {
            shaking = true;
            v_cameraNoise.m_AmplitudeGain = shakeAmplitude;
            v_cameraNoise.m_FrequencyGain = shakeFrequency;

            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            shaking = false;
            v_cameraNoise.m_AmplitudeGain = 0f;
            shakeElapsedTime = 0f;
        }
    }

    public void Shake(float duration, float amplitude, float frequency)
    {
        shakeAmplitude = amplitude;
        shakeFrequency = frequency;
        shakeDuration = duration;

        shakeElapsedTime = duration;
    }

}
