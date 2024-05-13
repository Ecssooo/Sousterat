using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TopDetector : MonoBehaviour
{
    [Header("Detection")]

    [SerializeField] private Transform[] _detectionPoints;
    [SerializeField] private float _detectionLenght = 0.1f;
    [SerializeField] private LayerMask _TopLayerMask;
    

    public bool DetectWallNearByTop()
    {
        foreach(Transform detectionPointRight in _detectionPoints)
        {
            RaycastHit2D hitResult = Physics2D.Raycast(
                detectionPointRight.position,
                Vector2.up,
                _detectionLenght,
                _TopLayerMask);
            if(hitResult.collider != null)
            {
                return true;
            }
        }
        return false;
    }
    
}