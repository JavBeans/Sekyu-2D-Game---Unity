using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePanel;
    bool isPaused = false;
    FadeInOutScript fade;

    private void Start()
    {
        fade = FindObjectOfType<FadeInOutScript>();
    }

    void Update()
    {
        HandlePause();
        
    }
    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }


    public void Restart()
    {
        Base1Script.score2 = 0;
        Base2Script.score1 = 0;
        GameManagerScript.gameStarted = false;
        Continue();
        StartCoroutine(RestartScene());
    }

    public void MainMenu()
    {
        StartCoroutine(mainMenu());
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public IEnumerator mainMenu()
    {
        Continue();
        fade.Fadein();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
