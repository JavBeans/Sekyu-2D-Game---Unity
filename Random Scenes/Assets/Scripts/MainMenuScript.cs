using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    FadeInOutScript fade;

    private void Start()
    {
        fade = FindObjectOfType<FadeInOutScript>();
    }

    public void StartButton()
    {
        
        StartCoroutine(ChangeScene());
    }
    public void QuitButton()
    {

        StartCoroutine(Quit());
    }

    public IEnumerator ChangeScene()
    {
        fade.Fadein();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public IEnumerator Quit()
    {
        fade.Fadein();
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
