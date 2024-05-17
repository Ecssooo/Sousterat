using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private bool _cameraZoom=false;
    [SerializeField] private float _zoomMin = 20f;
    [SerializeField] private float _zoomMax = 70f;
    [SerializeField] private float _velocity = 5f;
    private float _zoom;
    private bool _initialZoomDone = false;

    public static CameraZoom Instance { get; private set; }

    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private float _multiplySpeed;
    [SerializeField] private float _multiplyFuelConsumption;

    [SerializeField] private Transform _enemyTarget;
    [SerializeField] private Transform _drillTarget;

    [Header("Param�tres Auto-Zoom")]
    [SerializeField] private GameObject _checkPoint;
    public float zoomTimer;
    public float zoomCooldown;
    private void Awake()
    {
        _camera.m_Lens.OrthographicSize = _zoomMin;
    }
    private void Update()
    {
        _CameraZoom();
    }






    private void _CameraZoom() 
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            _initialZoomDone=true;
            
            if (!_cameraZoom)
            {
                _zoom = _zoomMax;
                _cameraZoom = true;
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed * _multiplySpeed;
                _levelSpeedManager.fuelConsumption = _levelSpeedManager.InitFuelConsumption * _multiplyFuelConsumption;

            }
            else
            {
                _zoom = _zoomMin;
                _cameraZoom = false;
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed;
                _levelSpeedManager.fuelConsumption = _levelSpeedManager.InitFuelConsumption;
            }
            

        }

        if(_initialZoomDone)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoom, Time.deltaTime * _velocity);
        }
       
    }

    


   public void AutoZoom(float _zoomMin,float _zoomMax)
    {
        zoomCooldown += Time.deltaTime;
        if (zoomCooldown >= zoomTimer)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMin, Time.deltaTime * _velocity);
            _camera.Follow = _drillTarget;
            if(zoomCooldown >= zoomTimer+2f)
            {
                Chekcpoint.zoomAvailable = false;
                zoomCooldown = 0;
                _checkPoint.SetActive(false);
            }
        }
        else
        {
            if (_camera.m_Lens.OrthographicSize != _zoomMax && zoomCooldown <= zoomTimer)
            {
                _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMax, Time.deltaTime * _velocity);
                _camera.Follow = _enemyTarget;
            }
        }

    }

}
