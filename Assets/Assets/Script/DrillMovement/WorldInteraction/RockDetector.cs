using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetector : MonoBehaviour
{
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private ActiveRotationHoraire _rotationHoraire;
    [SerializeField] private ActiveRotationAHoraire _rotationAntiHoraire;
    [SerializeField] private CameraShake _cameraShake;
    
    [SerializeField] private SASManager _sasManager;
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Charbon")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Coal;
                SASManager.mineCoal = true;
            }
            else if (hit.collider.gameObject.tag == "Calcite")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Calcite;
                SASManager.mineCoal = false;
            }
            else if (hit.collider.gameObject.tag == "Granite")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Granite;
                SASManager.mineCoal = false;
            }
            else if (hit.collider.gameObject.tag == "Dirt")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Dirt;
                SASManager.mineCoal = false;
            }
            else if (hit.collider.gameObject.tag == "Edge")
            {
                _RandomRotation();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.collision_3SFX);
            }else if (hit.collider.gameObject.tag == "Ennemy")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Enemy;
            }
            else if (hit.collider.gameObject.tag == "Mine")
            {
                _RandomRotation();
                hit.collider.gameObject.SetActive(false);
                _levelSpeedManager.fuelTank -= _levelSpeedManager.mineFuelLost;
                _cameraShake.ShakeCamera(10f, 0.1f);
            }
        }
    }

    public void _RandomRotation()
    {
        int randomNumber = Random.Range(1, 2);
        if (randomNumber == 1)
        {
            _rotationHoraire.StartRotation();
        }
        else if (randomNumber == 2)
        {
            _rotationAntiHoraire.StartRotation();
        }
    }
}
