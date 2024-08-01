using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base2Script : MonoBehaviour
{
    public TextMeshProUGUI textWin;
    public TextMeshProUGUI textScore;

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
