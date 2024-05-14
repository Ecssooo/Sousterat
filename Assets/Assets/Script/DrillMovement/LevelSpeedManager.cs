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
    public float CurrentLevelSpeed;
    
    [Header("Multiplier Fuel")]
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForEmplty;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForQuarter;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForHalf;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForFull;
    
    [Header("Multiplier Rock")]
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForDirt;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForCoal;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForGranite;
    [Tooltip("Float for percentage (0-1)")]
    [SerializeField] private float MultiplierForCalcite;
    
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

    [Header("Mine")]
    public MineState mineState = MineState.Dirt;
    public enum MineState
    {
        Dirt,
        Coal,
        Granite,
        Calcite
    }

    private MineState _lastMineState;
    
    private void Start()
    {
        levelSpeed = InitLevelSpeed;
        CurrentLevelSpeed = InitLevelSpeed;
        fuelConsumption = InitFuelConsumption;
    }

    private void Update()
    {
        _UpdateFuelState();
        _UpdateMinedRock();
        if (_fuelTankState != _lastFuelTankState || mineState != _lastMineState)
        {
            _lastFuelTankState = _fuelTankState;
            _lastMineState = mineState;
            _UpdateLevelSpeed();
        }
    }
    
    private void _UpdateLevelSpeed()
    {
        switch (_fuelTankState)
        {
            case FuelTankState.Empty:
                levelSpeed = CurrentLevelSpeed * MultiplierForEmplty;
                break;
            case FuelTankState.Quarter:
                levelSpeed = CurrentLevelSpeed * MultiplierForQuarter;
                break;
            case FuelTankState.Half:
                levelSpeed = CurrentLevelSpeed * MultiplierForHalf;
                break;
            case FuelTankState.Full:
                levelSpeed = CurrentLevelSpeed * MultiplierForFull;
                break;
        }
    }
    private void _UpdateMinedRock()
    {
        switch (mineState)
        {
            case MineState.Dirt:
                CurrentLevelSpeed = InitLevelSpeed * MultiplierForDirt;
                break;
            case MineState.Coal:
                CurrentLevelSpeed = InitLevelSpeed * MultiplierForCoal;
                break;
            case MineState.Granite:
                CurrentLevelSpeed = InitLevelSpeed * MultiplierForGranite;
                break;
            case MineState.Calcite:
                CurrentLevelSpeed = InitLevelSpeed * MultiplierForCalcite;
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
