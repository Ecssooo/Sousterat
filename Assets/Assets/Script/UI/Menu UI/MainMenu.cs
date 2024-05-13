using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator transition;
   

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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.transitionSFX);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayBackground();
        SceneManager.LoadSceneAsync(1);
    }
}
