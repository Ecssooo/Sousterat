using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _mainScreen;

    private void Start()
    {
        AudioManager.Instance.PlayBackground(AudioManager.Instance.mainMenu);
    }
    public void PlayGame()
    {
        AudioManager.Instance.StopMusic();
        _mainScreen.SetActive(false);
        _loadingScreen.SetActive(true);
        

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("FadeIn");
        _loadingScreen.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.startSFX);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayBackground(AudioManager.Instance.background);
        SceneManager.LoadSceneAsync(1);
    }
}
