using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelMouvement : MonoBehaviour
{
    //Script � mettre sur la partie du niveau qui doit se d�placer;
    //Ajouter la grille qui contient tous les objets qui vont tourner; 
    //Li�s le script LevelSpeedManager;
    
    [Header("PlateformeManager")] 
    [Tooltip("List of oneway platforms according to their axis")]
    [SerializeField] private GameObject[] _plateformX;
    [Tooltip("List of oneway platforms according to their axis")]
    [SerializeField] private GameObject[] _plateformY;
    
    [SerializeField] private Transform _gridToRotate;
    [SerializeField] private  LevelSpeedManager _levelSpeedManager;
    [SerializeField] private  Burst _burst;
    
    
    private bool isTrigger;
    
    private void Start()
    {
        isTrigger = false;
    }
    
    void OnTriggerEnter2D(Collider2D truc)
    {
        //Si le joueur est en contact avec le bouton;
        if (truc.tag == "Player")
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si le joueur n'est plus en contact avec le bouton;
        if (collision.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void FixedUpdate()
    {
        _LevelMovement();
    }

    private void _LevelMovement()
    {
        //Selon la rotation du niveau :
        //Reset / Set le sens plateformes OneWay
        //Set les collisions des plateformes OneWay
        //Consomer le carburant
        //Faire avancer le niveau
        if(_gridToRotate.transform.rotation.eulerAngles.z == 0)
        {
            _ResetPlateformColliderSense(_plateformX);
            _SetPlateformCollider(_plateformY, _plateformX);
            FuelUsed();
            
            transform.position = new Vector2(transform.position.x + _levelSpeedManager.levelSpeed, transform.position.y);
        }
        else if(_gridToRotate.transform.rotation.eulerAngles.z == 180)
        {
            _SetPlateformColliderSense(_plateformX);
            _SetPlateformCollider(_plateformY, _plateformX);
            FuelUsed();
            
            transform.position = new Vector2(transform.position.x - _levelSpeedManager.levelSpeed, transform.position.y);
        }
        else if(_gridToRotate.transform.rotation.eulerAngles.z == 90)
        {
            
            _SetPlateformColliderSense(_plateformY);
            _SetPlateformCollider(_plateformX, _plateformY);
            FuelUsed();

            transform.position = new Vector2(transform.position.x, transform.position.y + _levelSpeedManager.levelSpeed);
            
        }
        else if (_gridToRotate.transform.rotation.eulerAngles.z == 270)
        {
            _ResetPlateformColliderSense(_plateformY);
            _SetPlateformCollider(_plateformX, _plateformY);
            FuelUsed();

            transform.position = new Vector2(transform.position.x, transform.position.y - _levelSpeedManager.levelSpeed);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }
    }

    private void _SetPlateformCollider(GameObject[] plateformsListToDesactive, GameObject[] plateformsListToActivate)
    {
        //Active ou d�sactive les plateformes en fonction de la rotation du niveau;
        //Mettre en Param�tre les listes de plateformes � DESACTIVER puis � ACTIVER;
        foreach (GameObject plateform in plateformsListToDesactive)
        {
            BoxCollider2D boxCollider2D = plateform.GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
        }
        foreach (GameObject plateform in plateformsListToActivate)
        {
            BoxCollider2D boxCollider2D = plateform.GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = true;
        }
    }
    private void _SetPlateformColliderSense(GameObject[] plateformsListToChange)
    {
        //Modifie le sens des OneWay;
        //Param�tre : Listes des plateformes � retourner;
        foreach (GameObject plateform in plateformsListToChange)
        {
            PlatformEffector2D platformEffector2D = plateform.GetComponent<PlatformEffector2D>();
            platformEffector2D.rotationalOffset = 180;
        }
    }

    private void _ResetPlateformColliderSense(GameObject[] plateformsListToReset)
    {
        //Reset le sens des OneWay;
        //Param�tre : Listes des plateformes � reset;
        foreach (GameObject plateform in plateformsListToReset)
        {
            PlatformEffector2D platformEffector2D = plateform.GetComponent<PlatformEffector2D>();
            platformEffector2D.rotationalOffset = 0;
        }
    }
    
    
    public void FuelUsed()
    {
        //Consomation du carburant;
        //Utilis� uniquement lorsque le niveau se d�place;
        if (_levelSpeedManager.fuelTank <= 0f)
        {
            _levelSpeedManager.fuelTank = 0.1f;
        }
        else
        {
            _levelSpeedManager.fuelTank -= _levelSpeedManager.fuelConsumption * Time.deltaTime;
        }
        
    }
}