using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player2_Member1Script : MonoBehaviour
{
    public Transform playerLocation;
    public Transform base2Location;
    public Vector3 offset1;
    public Vector3 offset2;

    public Text textPlayer1;
    public Text textTime;

    public float timeCount;
    float captureSpeed = 5f;

    private bool isInsideBase;

    public GameObject player1_1;
    public GameObject player1_2;
    public GameObject player1_3;


    void Update()
    {
        if (isInsideBase)
        {
            timeCount += Time.deltaTime;
            textTime.text = timeCount.ToString("F2"); // Display time with two decimal places
        }
        Vector3 targetPosition1 = playerLocation.position + offset1;
        Vector3 targetPosition2 = playerLocation.position + offset2;
        textPlayer1.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textTime.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Base2"))
        {
            timeCount = 0;
            textTime.text = timeCount.ToString("F2");
            isInsideBase = false;
        }

        if (other.gameObject.CompareTag("Player1"))
        {
            float playerTimeCount = 0;

            if (other.gameObject == player1_1)
            {
                playerTimeCount = player1_1.GetComponent<Player1_Member1Script>().timeCount;
                HandlePlayerCollision(player1_1, playerTimeCount);
            }
            else if (other.gameObject == player1_2)
            {
                playerTimeCount = player1_2.GetComponent<Player1_Member2Script>().timeCount;
                HandlePlayerCollision(player1_2, playerTimeCount);
            }
            else if (other.gameObject == player1_3)
            {
                playerTimeCount = player1_3.GetComponent<Player1_Member3Script>().timeCount;
                HandlePlayerCollision(player1_3, playerTimeCount);
            }
        }
    }
    private void HandlePlayerCollision(GameObject otherPlayer, float otherPlayerTimeCount)
    {
        if (timeCount < otherPlayerTimeCount)
        {
            StartCoroutine(CapturePlayer(otherPlayer)); //capture opponent
        }
    }
    private IEnumerator CapturePlayer(GameObject player)
    {
        player.tag = "Prisoner1";
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            while (Vector3.Distance(player.transform.position, base2Location.position) > 0.1f)
            {
                Vector3 direction = (base2Location.position - player.transform.position).normalized;
                playerRb.velocity = direction * captureSpeed;
                yield return null;
            }
            playerRb.velocity = Vector2.zero; // Stop player movement
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Base2"))
        {
            textTime.text = timeCount.ToString("F2");
            isInsideBase = true;
        }
    }
}
