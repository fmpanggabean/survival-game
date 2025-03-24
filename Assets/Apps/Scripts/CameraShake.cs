using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;
    public float shakeDuration = 0.3f; 
    public float shakeIntensity = 10f;  

    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (noise != null)
        {
            noise.m_AmplitudeGain = 0;
        }
    }

    public void Shake()
    {
        if (noise != null)
        {
            StartCoroutine(ShakeRoutine());
        }
    }

    private IEnumerator ShakeRoutine()
    {
        noise.m_AmplitudeGain = shakeIntensity;
        yield return new WaitForSeconds(shakeDuration);
        noise.m_AmplitudeGain = 0; 
    }
}
