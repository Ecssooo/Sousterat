using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; }
    private CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
    [SerializeField] private float _shakeDuration = 3f;
    private float _shakeTimer = 0f;
    [SerializeField] private float _intensity =2f;
    [SerializeField]private float _shakeInterval = 5f; 
    private bool _isShaking = false;

    private void Awake()
    {
        instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
       basicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    private void Start()
    {
        
        basicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }

    private void Update()
    {
        
        if (_isShaking)
        {
            
            _shakeTimer -= Time.deltaTime;

           
            if (_shakeTimer <= 0)
            {
                _isShaking = false;
                
                basicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
        else
        {
            
            _shakeTimer += Time.deltaTime;

            
            if (_shakeTimer >= _shakeInterval)
            {
                
                ShakeCamera(_intensity, _shakeDuration);
                _isShaking = true;
            }
        }
    }

    public void ShakeCamera(float intensity, float duration)
    {
        
        basicMultiChannelPerlin.m_AmplitudeGain = intensity;

        
        _shakeTimer = duration;
    }


}
