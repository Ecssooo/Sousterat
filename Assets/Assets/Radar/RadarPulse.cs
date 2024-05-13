using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class RadarPulse : MonoBehaviour
{
    private Transform pulseTransform;

    [SerializeField] private Transform pfRadarPing;
    private float range;
    [SerializeField]private float rangeMax;
    [SerializeField] float rangeSpeed ;
    private float fadeRange;
    private SpriteRenderer pulseSpriteRenderer;
    private Color pulseColor;
    private List<Collider2D> alreadyPingedColliderList;

    private void Awake()
    {
        pulseTransform = transform.Find("Pulse");
        pulseSpriteRenderer = pulseTransform.GetComponent<SpriteRenderer>();
        pulseColor = pulseSpriteRenderer.color;
        fadeRange = 15f;
        alreadyPingedColliderList = new List<Collider2D>();
    }

    private void Update()
    {
        
        range += rangeSpeed * Time.deltaTime;
        if(range > rangeMax)
        {
            range = 0f;
            alreadyPingedColliderList.Clear();
        }
        pulseTransform.localScale = new Vector3 (range, range);

        RaycastHit2D[] raycastHit2DArray= Physics2D.CircleCastAll(transform.position, range/2f, Vector2.zero);
        foreach (RaycastHit2D raycastHit2D in raycastHit2DArray)
        {
            if (raycastHit2D.collider.CompareTag("RadarMark"))
            {

                if (raycastHit2D.collider != null)
                {
                    if (!alreadyPingedColliderList.Contains(raycastHit2D.collider))
                    {
                        alreadyPingedColliderList.Add(raycastHit2D.collider);
                        Transform radarPingTransform = Instantiate(pfRadarPing, raycastHit2D.point, Quaternion.identity);
                        RadarPing radarPing = radarPingTransform.GetComponent<RadarPing>();
                        if (raycastHit2D.collider.gameObject.GetComponent<EnnemyFollowerV2>() != null)
                        {
                            radarPing.SetColor(new Color(1, 0, 0));
                        }
                        if (raycastHit2D.collider.gameObject.GetComponent<Snake>() != null)
                        {
                            radarPing.SetColor(new Color(0, 0, 1));
                        }


                        radarPing.SetDisapearTimer(rangeMax / rangeSpeed * 1.5f);

                    }

                }
            }

        
            
        }
        if(range>rangeMax- fadeRange)
        {
            pulseColor.a = Mathf.Lerp(0f, 1f, (rangeMax - range) / fadeRange);
        }
        else
        {
            pulseColor.a = 1f;
        }
        pulseSpriteRenderer.color = pulseColor;
        
    }
}
