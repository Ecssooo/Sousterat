using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("SFX Clips")]
    public AudioClip rotationSFX;
    public AudioClip startSFX;
    public AudioClip burstSFX;
    public AudioClip charbonSFX;
    public AudioClip end_fuelSFX;
    public AudioClip collisionSFX;


    [Header("Music Clips")]
    public AudioClip background;




    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
        
    }

    private void Start()
    {
        
    }



    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayBackground()
    {
        musicSource.clip = background; musicSource.Play();
    }
}
