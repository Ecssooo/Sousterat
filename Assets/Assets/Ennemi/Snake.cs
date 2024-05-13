using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float closeEnoughThreshold = 0.3f;

    private Transform _target;
    private int _destPoint = 0;

    void Start()
    {
      _target = _waypoints[0];
        transform.position = _target.position;
    }

    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized* moveSpeed* Time.deltaTime,Space.World);

        if(Vector3.Distance(transform.position, _target.position) < closeEnoughThreshold)
        {
            _destPoint = (_destPoint+1) % _waypoints.Length;
            _target= _waypoints[_destPoint];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(CollisionOnEnnemi());
            StartCoroutine(Disparition());
            
        }
    }

    IEnumerator CollisionOnEnnemi()
    {
        gameObject.layer = 7;
        yield return new WaitForSeconds(1f);
        gameObject.layer = 0;
    }

    IEnumerator Disparition()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;



    }
}

 



