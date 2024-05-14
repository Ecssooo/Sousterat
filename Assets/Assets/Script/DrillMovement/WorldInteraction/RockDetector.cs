using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetector : MonoBehaviour
{
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    
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
        }
    }
}
