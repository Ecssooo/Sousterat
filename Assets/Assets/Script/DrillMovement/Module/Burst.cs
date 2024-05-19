using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    [Header("LevelSpeed")] 
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private CameraShake _cameraShake;
    
    [Header("Burst stats")]
    [SerializeField] private float _burstSpeed;
    [SerializeField] private float _burstDuration;
    [SerializeField] private float _burstConsumption;
    private float _burstTimer;
    [SerializeField] private float _burstCooldown;
    private float _burstCooldownTimer;
    private bool _burstAvailable = true;

    [Header("UI")] [SerializeField] private Animator _animator;
    
    public bool _isBurst = false;
    private bool _isTrigger;
    private void Update()
    {
        if (_burstCooldown <= _burstCooldownTimer)
        {
            _burstAvailable = true;
        }
        else
        {
            _burstCooldownTimer += Time.deltaTime;
        }
        if (_isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_burstAvailable)
                {
                    _isBurst = true;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.burstSFX);
                    if (_levelSpeedManager.fuelTank <= _burstConsumption || _levelSpeedManager.fuelTank <= 10)
                    {
                        _isBurst = false;
                        _cameraShake.ShakeCamera(1.5f, 0.2f);
                    }
                }
            }
        }
        if (_isBurst)
        {
            _Burst();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isTrigger = false;
        }
    }

    private void _Burst()
    {
        _burstTimer += Time.deltaTime;
        if (_burstTimer <= _burstDuration)
        {
            
            _animator.SetBool("IsBurst", true);
            _levelSpeedManager.levelSpeed = _burstSpeed;
            _levelSpeedManager.fuelConsumption = _burstConsumption;
        }
        else
        {
            _animator.SetBool("IsBurst", false);
            _isBurst = false;
            _burstTimer = 0f;
            _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed;
            _levelSpeedManager.fuelConsumption = _levelSpeedManager.InitFuelConsumption;
            _burstCooldownTimer = 0;
            _burstAvailable = false;
        }
    }
}
 