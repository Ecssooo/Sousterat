using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIFuelManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fuelGauge;
    
    [Header("Fuel Tank")]
    [SerializeField] private LevelSpeedManager _levelSpeedManager;

    private void Update()
    {
        fuelGauge.fillAmount = _levelSpeedManager.fuelTank / 100f;
    }
}
