using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TM_Manager : MonoBehaviour
{
    [SerializeField] private Tilemap destructTilemap;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Drill"))
        {
            Debug.Log("touche");
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in other.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                destructTilemap.SetTile(destructTilemap.WorldToCell(hitPosition), null);
            }
        }
    }
}
