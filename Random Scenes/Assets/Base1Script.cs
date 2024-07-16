using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base1Script : MonoBehaviour
{
    public static int score2;


    public Text textWin;
    public Text textScore;
    public static bool gamefinish;

    public static int playernumber1;
    private void Start()
    {
        updatedScore();
        gamefinish = false;
        playernumber1 = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player2") && gamefinish == false)
        {
            textWin.gameObject.SetActive(true);
            textWin.text = "Player 2 Wins!";
            score2++;
            gamefinish = true;
            updatedScore();
            StartCoroutine(RestartScene());
        }
        else if (other.gameObject.CompareTag("Player1"))
        {
            playernumber1 = 2;
            Base2Script.playernumber2 = 1;
        }
    }

    void updatedScore()
    {
        textScore.text = $"{Base2Script.score1} | {score2}";
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
