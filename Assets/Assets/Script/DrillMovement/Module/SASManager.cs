using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SASManager : MonoBehaviour
{
    [Header("SAS GameObject")]
    [SerializeField] private GameObject _coal;

    [Header("Animator")] [SerializeField] private Animator _animator;
    private bool hasPlayed;

    [Header("CoalManager")]
    [SerializeField] private float _coalRespawnTimer;
    [SerializeField] private int _maxCoalAvailable;
    public int _coalAvailable = 0;
    public static bool mineCoal;
    public static bool playerHasCoal;
    
    private float _coalRespawnCooldown;
    
    private bool isTrigger;

    private void Update()
    {
        if(isTrigger && !playerHasCoal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_coalAvailable > 0)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.charbonSFX);
                    _coal.SetActive(false);
                    playerHasCoal = true;
                    hasPlayed = false;
                    _coalAvailable--;
                    if (_coalAvailable <= 0)
                    {
                        _animator.SetBool("Available", false);
                    }
                }
            }
        }
        else
        {
            _CoalRespawn();
        }
    }

    private void _CoalRespawn()
    {
        if (mineCoal)
        {
            _coalRespawnCooldown += Time.deltaTime;
            if (_coalRespawnCooldown > _coalRespawnTimer)
            {
                _coalRespawnCooldown = 0;
                _coal.SetActive(true);
                if (_coalAvailable + 1 <= _maxCoalAvailable)
                {
                    _coalAvailable++;
                }
                if (!hasPlayed)
                {
                    _animator.SetTrigger("Spawn");
                    hasPlayed = true;
                    if(_coalAvailable >= 0)
                    {
                        _animator.SetBool("Available", true);
                    }
                }
            }
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
            isTrigger = false;
        }
    }
}
