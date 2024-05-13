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

    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private float _multiplySpeed;

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

            }
            else
            {
                _zoom = _zoomMin;
                _cameraZoom = false;
                _levelSpeedManager.levelSpeed = _levelSpeedManager.InitLevelSpeed ;
            }
            

        }

        if(_initialZoomDone)
        {
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoom, Time.deltaTime * _velocity);
        }
       
    }

   IEnumerator AutoZoom()
    {
        _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMax, Time.deltaTime * _velocity);
        yield return new WaitForSeconds(2f);
        _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize, _zoomMin, Time.deltaTime * _velocity);

    }
}
