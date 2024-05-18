using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] Text _textToBlink; 
    [SerializeField] float blinkDuration = 1f; 

    private float timer;
    private bool isBlinking = true;

    void Start()
    {
        if (_textToBlink == null)
        {
            _textToBlink = GetComponent<Text>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blinkDuration)
        {
            isBlinking = !isBlinking;
            _textToBlink.enabled = isBlinking;
            timer = 0f;
        }
    }
}
