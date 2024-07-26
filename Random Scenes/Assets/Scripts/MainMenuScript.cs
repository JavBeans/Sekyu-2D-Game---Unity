using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    FadeInOutScript fade;
    public GameObject option;

    private void Start()
    {
        fade = FindObjectOfType<FadeInOutScript>();
    }

    public void StartButton()
    {
        Base1Script.score2 = 0;
        Base2Script.score1 = 0;
        StartCoroutine(ChangeScene());
    }

    public void OptionButton()
    {

        option.SetActive(true);
    }
    public void ExitOptionButton()
    {
        option.SetActive(false);
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
