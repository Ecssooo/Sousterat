using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalePulse : MonoBehaviour
{
    [SerializeField]private Transform pulseTransform;
    private float range;
    [SerializeField]private float rangeMax;
    [SerializeField] float rangeSpeed;
    

    private void Awake()
    {
        //pulseTransform = transform.Find("FinalePulse");
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
        range += rangeSpeed * Time.deltaTime;
        if (range > rangeMax)
        {
            range = 0f;
        }
        pulseTransform.localScale = new Vector3(range, range);
    }
}
