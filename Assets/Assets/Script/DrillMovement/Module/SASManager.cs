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
    
    private float _coalRespawnCooldown;
    private bool _coalHere;
    public bool mineCoal;
    
    
    private bool isTrigger;
    public bool playerHasCoal;
    private void Update()
    {
        _CheckCoal();
        if (_coalHere)
        {
            _coalRespawnCooldown = 0;
            if(isTrigger && !playerHasCoal)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _coal.SetActive(false);
                    _coalHere = false;
                    playerHasCoal = true;
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
                _coal.SetActive(true);
                _coalHere = true;
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
