using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestroyer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private LayerMask _TMSpriteLayer;
    private void Update()
    {
        RaycastHit2D hitPosition = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            1f,
            _TMSpriteLayer);
        if (hitPosition != null)
        {
            DestroyTile(transform.position);
        }
    }

    public void DestroyTile(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        
        tilemap.SetTile(cellPosition, null);
    }
    
}
