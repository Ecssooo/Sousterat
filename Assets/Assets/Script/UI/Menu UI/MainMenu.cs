using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;

    private void Start()
    {
        AudioManager.Instance.PlayBackground(AudioManager.Instance.mainMenu);
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel());

    }

    public void QuitGame()
    {
        Application.Quit();
    }



    IEnumerator LoadLevel()
    {
        
        transition.SetTrigger("FadeIn");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.startSFX);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayBackground(AudioManager.Instance.background);
        SceneManager.LoadSceneAsync(1);
    }
}
