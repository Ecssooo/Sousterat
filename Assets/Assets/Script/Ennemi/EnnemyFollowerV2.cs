using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyFollowerV2 : MonoBehaviour
{

    [Header("Paramètres Shaking")]
    [SerializeField] private float _shakingForce;
    [SerializeField] private float _shakingTime;

    [Header("Paramètres Vitesse")]
    public float minSpeed = 2.0f;  
    public float maxSpeed = 5.0f;
    [SerializeField] float _burstSpeed;
    [SerializeField]private GameObject target;
    [SerializeField] private bool isTouch=false;

    
    private float _currentSpeed;
    private float directionX;
    private float directionY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         directionX = Mathf.Sign(target.transform.position.x - transform.position.x);
         directionY = Mathf.Sign(target.transform.position.y - transform.position.y);
        float distanceToPlayer = Vector2.Distance(transform.position, target.transform.position);  // Distance au joueur

        if (isTouch)
        {
            transform.position = new Vector3(transform.position.x +minSpeed*Time.deltaTime, transform.position.y ,transform.position.z);


        }

        else
        {
            if (distanceToPlayer < 5f)
            {
                //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, minSpeed * Time.deltaTime);
                _currentSpeed = minSpeed;
                Mouvement(_currentSpeed);
            }
            else
            {
                //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, maxSpeed * Time.deltaTime);
                _currentSpeed = maxSpeed;
                Mouvement(_currentSpeed);
            }
        }
       
       
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Touché");
            
            isTouch = true;
            gameObject.layer = 7;
            CameraShake.instance.ShakeCamera(_shakingForce, _shakingTime);
            StartCoroutine(Disparition());
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

}
