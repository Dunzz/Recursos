using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Animation_CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private float shakeTimer;
    private float originalAmplitude;

    private void Awake()
    {
        noise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originalAmplitude = noise.m_AmplitudeGain;
    }

    public void ShakeCamera(float intensity, float duration)
    {
        // Establece la intensidad y la duración del shake
        noise.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Reduce el temporizador
            shakeTimer -= Time.deltaTime;

            // Si el temporizador llega a cero, reinicia la amplitud
            if (shakeTimer <= 0f)
            {
                noise.m_AmplitudeGain = originalAmplitude;
            }
        }
    }
}
