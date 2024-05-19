using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [Header("Particules")] 
    [SerializeField] private GameObject _coalParticule;

    [Header("Link script")] 
    [SerializeField] private SASManager _sasManager;


    private void Update()
    {
        if (SASManager.playerHasCoal)
        {
            ActiveGameObject(_coalParticule);
        }
        else
        {
            DesactiveGameObject(_coalParticule);
        }
    }
    
    private void ActiveGameObject(GameObject _gameObject)
    {
        if(!_gameObject.activeInHierarchy)
            _gameObject.SetActive(true);
    }

    private void DesactiveGameObject(GameObject _gameObject)
    {
        if(_gameObject.activeInHierarchy)
            _gameObject.SetActive(false);
    }
}
