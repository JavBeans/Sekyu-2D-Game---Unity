using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Base1Script : MonoBehaviour
{
    public Text textWin;
    public Text textScore;


    
    public static int score2;
    public static int playernumber1;
    public static bool gamefinish;

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

        }
    }

    void updatedScore()
    {
        textScore.text = $"{Base2Script.score1} | {score2}";
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
