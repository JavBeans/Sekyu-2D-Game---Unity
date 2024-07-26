using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base2Script : MonoBehaviour
{
    
    public Text textWin;
    public Text textScore;

    public static int score1;
    public static int playernumber2;

    private void Start()
    {
        updatedScore();
        playernumber2 = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player1") && Base1Script.gamefinish == false)
        {
            textWin.gameObject.SetActive(true);
            textWin.text = "Player 1 Wins!";
            score1++;
            Base1Script.gamefinish = true;
            updatedScore();
            StartCoroutine(RestartScene());
            GameManagerScript.gameStarted = false;
        }
        else if (other.gameObject.CompareTag("Player2"))
        {

        }
    }
    void updatedScore()
    {
        textScore.text = $"{score1} | {Base1Script.score2}";
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
