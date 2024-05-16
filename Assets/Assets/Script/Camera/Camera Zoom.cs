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

    public float zoomTimer;
    public float zoomCooldown;
   public void AutoZoom(float _zoomMin,float _zoomMax)
    {
        if (zoomCooldown >= zoomTimer)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMin, 0.1f);
            if (_camera.m_Lens.OrthographicSize == _zoomMin)
            {
                Chekcpoint.zoomAvailable = false;
                zoomCooldown = 0;
            }
        }
        else
        {
            if (_camera.m_Lens.OrthographicSize <= _zoomMax && zoomCooldown <= zoomTimer)
            {
                _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMax, Time.deltaTime * _velocity);
            }
        }

    }

}
