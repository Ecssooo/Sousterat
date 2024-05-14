using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TM_Collider : MonoBehaviour
{
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private SASManager _sasManager;
    
    
    [SerializeField] private float Terre;
    [SerializeField] private float Granite;
    [SerializeField] private float Calcite;
    [SerializeField] private float Charbon;
    [SerializeField] private float Ver;

    private void OnTriggerEnter2D(Collider2D other)
    {
        SpeedColliderManager(other.tag);
    }


    private void SpeedColliderManager(string tag)
    {
        switch (tag)
        {
            case "Terre":
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * Terre;
                _sasManager.mineCoal = false;
                break;
            case "Granite":
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * Granite;
                _sasManager.mineCoal = false;
                break;
            case "Calcite":
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * Calcite;
                _sasManager.mineCoal = false;
                break;
            case "Charbon":
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * Charbon;
                _sasManager.mineCoal = true;
                break;
            case "Ver":
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * Ver;
                _sasManager.mineCoal = false;
                break;
        }
    }
}
