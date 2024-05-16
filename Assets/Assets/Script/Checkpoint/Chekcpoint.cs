using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chekcpoint : MonoBehaviour
{
    [SerializeField] GameObject _ennemyDrill;
    [SerializeField] Transform _waypoint;
    [SerializeField] CameraZoom _zoom;

    [Header("Paramètres Auto-Zoom")]
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;
    public static bool zoomAvailable;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (zoomAvailable)
        {
            _zoom.AutoZoom(_zoomMin,_zoomMax);
            _zoom.zoomCooldown += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          _ennemyDrill.SetActive(true);
            Vector3 spawnPosition = _waypoint.position;
            _ennemyDrill.transform.position = spawnPosition;
            zoomAvailable = true;
        };
    }
}
