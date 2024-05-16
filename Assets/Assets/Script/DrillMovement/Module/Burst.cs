using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour
{
    [Header("LevelSpeed")] 
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    
    [Header("Burst stats")]
    [SerializeField] private float _burstSpeed;
    [SerializeField] private float _burstDuration;
    [SerializeField] private float _burstConsumption;
    public float _burstCooldown;

    [Header("UI")] [SerializeField] private Animator _animator;
    
    public bool _isBurst = false;
    private bool _isTrigger;
    private void Update()
    {
        if (_isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isBurst = true;
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
        _burstCooldown += Time.deltaTime;
        if (_burstCooldown <= _burstDuration)
        {
            _animator.SetBool("IsBurst", true);
            _levelSpeedManager.levelSpeed = _burstSpeed;
            _levelSpeedManager.fuelConsumption = _burstConsumption;
        }
        else
        {
            _animator.SetBool("IsBurst", false);
            _isBurst = false;
            _burstCooldown = 0f;
            _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed;
            _levelSpeedManager.fuelConsumption = _levelSpeedManager.InitFuelConsumption;
        }
    }
}
 