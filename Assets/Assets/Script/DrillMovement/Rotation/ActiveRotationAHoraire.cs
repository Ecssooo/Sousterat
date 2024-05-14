using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveRotationAHoraire : MonoBehaviour
{
    [SerializeField] private Transform grid;


    private bool isTrigger = false;
    private bool isRotating = false;
    void OnTriggerEnter2D(Collider2D truc)
    {
        //Si le joueur est en contact avec le bouton
        if(truc.tag == "Player")
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Si le joueur n'est plus en contact avec le bouton
        if(collision.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void Update()
    {
        //Check toutes les frames si le joueur est en contact avec le bouton
        //Si c'est le cas, le joueur peut appuyer sur E pour activer la rotation
        if(isTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartRotation();
            }
        }
    }

    //Fonction qui permet de faire tourner le niveau de 90�
    private IEnumerator Rotate(Vector3 angles, float duration)
    {
        isRotating = true;
        Quaternion startRotation = grid.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            grid.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
            yield return null;
        }

        grid.transform.rotation = endRotation;
        isRotating = false;
    }

    //Fonction qui permet de lancer la rotation
    public void StartRotation()
    {
        if (!isRotating)
        {
            StartCoroutine(Rotate(new Vector3(0, 0, 90), 1));
        }
    }
}

