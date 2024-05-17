using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFollowerV2 : MonoBehaviour
{
    [Header("Parametre durée de vie")]
    [SerializeField] private float _ennemyDuration = 5f;
    public static float _timer;


    [Header("Param�tres Shaking")]
    [SerializeField] private float _shakingForce;
    [SerializeField] private float _shakingTime;

    [Header("Param�tres Vitesse")]
    [SerializeField] float _minSpeed = 2.0f;  
    [SerializeField] float _maxSpeed = 5.0f;
    [SerializeField] float _touchSpeed;
    [SerializeField] float _burstSpeed;
    [SerializeField] private bool isTouch=false;
    

    [Header("Param�tres du cible")]
    [SerializeField] private GameObject target;
    [SerializeField] private BoxCollider2D _collider2d;

    [Header("Parametre de la foreuse")] 
    [SerializeField] private LevelSpeedManager _levelSpeedManager;
    [SerializeField] private RockDetector _rockDetector;
    [SerializeField] private float _fuelLost;
    
    
    private float _currentSpeed;
    private float directionX;
    private float directionY;
    

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _ennemyDuration)
        {
            StartCoroutine(WaitDestroy());
        }
        else
        {
            directionX = Mathf.Sign(target.transform.position.x - transform.position.x);
            directionY = Mathf.Sign(target.transform.position.y - transform.position.y);
            float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);  // Distance au joueur

            if (isTouch)
            {
                transform.position = new Vector3(transform.position.x + _touchSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                

            }

            else
            {
                if (distanceToPlayer < 40f)
                {
                    //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, minSpeed * Time.deltaTime);
                    _currentSpeed = _minSpeed;
                    Mouvement(_currentSpeed);
                }
                else
                {
                    //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, maxSpeed * Time.deltaTime);
                    _currentSpeed = _maxSpeed;
                    Mouvement(_currentSpeed);
                }
            }
        }
         
       
       
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touch�");
            
            isTouch = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collision_1SFX);
            _collider2d.enabled = false;
            CameraShake.instance.ShakeCamera(_shakingForce, _shakingTime);
            _rockDetector._RandomRotation();
            _levelSpeedManager.fuelTank -= _fuelLost;
            StartCoroutine(WaitDestroy());
            
        }

    }
    
    IEnumerator Disparition()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.2f);
       

    }


    void Mouvement(float speed)
    {
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > Mathf.Abs(target.transform.position.y - transform.position.y))
        {
            
            transform.Translate(Vector2.right * directionX * speed * Time.deltaTime);
        }
        else 
        {
            
            transform.Translate(Vector2.up * directionY * speed * Time.deltaTime);
        }
    }

    IEnumerator Burst()
    {
        Mouvement(_burstSpeed);
        yield return new WaitForSeconds(1f);
        Mouvement(_currentSpeed);
    }

    IEnumerator WaitDestroy()
    {
        
        yield return new WaitForSeconds(1f);
        _collider2d.enabled = true;
        gameObject.SetActive(false);
        isTouch = false;
    }
}
