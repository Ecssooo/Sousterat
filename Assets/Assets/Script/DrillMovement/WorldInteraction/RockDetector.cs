using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetector : MonoBehaviour
{
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private ActiveRotationHoraire _rotationHoraire;
    [SerializeField] private ActiveRotationAHoraire _rotationAntiHoraire;
    
    
    void Update()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        
        if (hit.collider != null)
        {
           Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Charbon")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Coal;
            }
            else if (hit.collider.gameObject.tag == "Calcite")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Calcite;
            }
            else if (hit.collider.gameObject.tag == "Granite")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Granite;
            }
            else if (hit.collider.gameObject.tag == "Dirt")
            {
                _levelSpeedManager.mineState = LevelSpeedManager.MineState.Dirt;
            }
            else if (hit.collider.gameObject.tag == "Edge")
            {
                _RandomRotation();
            }
        }
    }

    private void _RandomRotation()
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
