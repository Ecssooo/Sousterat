using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineManager : MonoBehaviour
{
    //Script à mettre sur le GameObject qui servira de moteur
    //Le GO "_coal" est le GameObject coal qui est lié au SAS
    //Penser a mettre liés les script LevelSpeedManager et SASManager
    [Header("Engine GameObject")] [SerializeField]
    private GameObject _coal;

    [Header("Coal Stats")]
    [SerializeField] private float _coalValue;

    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private SASManager _sasManager;

    private bool _isTrigger;

    private void Update()
    {
        if (_isTrigger && _sasManager.playerHasCoal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Coal Added");
                _AddFuel();
            }
            
        }
    }

    private void _AddFuel()
    {
        _levelSpeedManager.fuelTank += _CalculFuelDiff();
        _sasManager.playerHasCoal = false;
    }

    private float _CalculFuelDiff()
    {
        //Calcul pour éviter de mettre plus de fuel que le maximum (100)
        float currentFuel = _levelSpeedManager.fuelTank + _coalValue;
        if (currentFuel >= 100)
        {
            return _coalValue - (currentFuel - 100);
        }

        return _coalValue;
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
}
