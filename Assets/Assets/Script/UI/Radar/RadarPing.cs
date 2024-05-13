using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;
    private float disappearTime;
    private float disappeaeMax;
    private Color color;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        disappeaeMax = 1f;
        disappearTime = 0f;
        color = new Color(1, 1, 1, 1f);

    }

    private void Update()
    {
        disappearTime += Time.deltaTime;
        color.a= Mathf.Lerp(disappeaeMax,0f, disappearTime/disappeaeMax);
        spriteRenderer.color = color;

        if (disappearTime >= disappeaeMax)
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public void SetDisapearTimer(float disappearTimeMax)
    {
        this.disappeaeMax=disappearTimeMax;
        disappearTime=0f;
    }
}
