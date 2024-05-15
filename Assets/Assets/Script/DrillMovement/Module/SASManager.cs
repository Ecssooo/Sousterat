using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SASManager : MonoBehaviour
{
    [Header("SAS GameObject")]
    [SerializeField] private GameObject _coal;

    [Header("Timer")] 
    [SerializeField] private float _coalRespawnTimer;

    [Header("Animator")] [SerializeField] private Animator _animator;

    public int _coalAvailable = 0;
    
    private float _coalRespawnCooldown;
    private bool _coalHere;
    public bool mineCoal;
    
    private bool isTrigger;
    public bool playerHasCoal;

    private bool hasPlayed;
    private void Update()
    {
        _CheckCoal();
            if(isTrigger && !playerHasCoal)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _coal.SetActive(false);
                    _coalHere = false;
                    playerHasCoal = true;
                    hasPlayed = false;
                    _coalAvailable--;
                    if (_coalAvailable <= 0)
                    {
                        _animator.SetBool("Available", false);
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
                _coalHere = true;
                _coal.SetActive(true);
                _coalAvailable++;
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

    private void _CheckCoal()
    {
        if (!_coal.activeInHierarchy)
        {
            _coalHere = false;
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
