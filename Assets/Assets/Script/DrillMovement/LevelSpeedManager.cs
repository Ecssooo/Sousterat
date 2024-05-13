using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSpeedManager : MonoBehaviour
{
    //Script à mettre sur la partie du niveau qui doit se déplacer
    //Penser à remplir toutes les variables 
    //Respecter la range indiqué en tooltip pour chaque variable
    
    [Header("Level Speed")]
    public float levelSpeed;
    public float InitLevelSpeed;
    
    [Header("Multiplier Fuel")]
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForEmplty;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForQuarter;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForHalf;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForFull;
    
    [Header("Fuel Tank")]
    [Tooltip("Float for fuel level (0-100)")]
    public float fuelTank;
    private FuelTankState _fuelTankState = FuelTankState.Full;
    [Tooltip("Float for Consumption (0-1)")]
    public float fuelConsumption;
    public float InitFuelConsumption;
    public enum FuelTankState
    {
        Empty,
        Quarter,
        Half,
        Full,
    }
    private FuelTankState _lastFuelTankState;

    private void Start()
    {
        levelSpeed = InitLevelSpeed;
        fuelConsumption = InitFuelConsumption;
    }

    private void Update()
    {
        _UpdateFuelState();
        if (_fuelTankState != _lastFuelTankState)
        {
            _lastFuelTankState = _fuelTankState;
            _UpdateLevelSpeed();
        }
    }
    
    private void _UpdateLevelSpeed()
    {
        switch (_fuelTankState)
        {
            case FuelTankState.Empty:
                levelSpeed = InitLevelSpeed * MultiplierForEmplty;
                break;
            case FuelTankState.Quarter:
                levelSpeed = InitLevelSpeed *MultiplierForQuarter;
                break;
            case FuelTankState.Half:
                levelSpeed = InitLevelSpeed *MultiplierForHalf;
                break;
            case FuelTankState.Full:
                levelSpeed = InitLevelSpeed * MultiplierForFull;
                break;
        }
    }

    private void _UpdateFuelState()
    {
        
        if (fuelTank > 90)
        {
            _fuelTankState = FuelTankState.Full;
            
        }else if (fuelTank > 20 && fuelTank < 90)
        {
            _fuelTankState = FuelTankState.Half;
            
        }else if (fuelTank > 0 && fuelTank < 10)
        {
            _fuelTankState = FuelTankState.Quarter;
            
        }else if (fuelTank <= 0)
        {
            _fuelTankState = FuelTankState.Empty;
            
        }
    }
}
