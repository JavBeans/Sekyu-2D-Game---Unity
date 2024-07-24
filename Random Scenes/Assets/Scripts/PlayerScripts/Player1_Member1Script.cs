using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player1_Member1Script : MonoBehaviour
{
    public Transform playerLocation;
    public Vector3 offset1;
    public Vector3 offset2;

    public Text textPlayer1;
    public Text textTime;

    public float timeCount;

    private bool isInsideBase;

    public GameObject player2_1;
    public GameObject player2_2;
    public GameObject player2_3;


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
        if (other.gameObject.CompareTag("Base1"))
        {
            timeCount = 0;
            textTime.text = timeCount.ToString("F2");
            isInsideBase = false;
        }
        if (other.gameObject.CompareTag("Player2"))
        {
            float playerTimeCount = 0;

            if (other.gameObject == player2_1)
            {
                playerTimeCount = player2_1.GetComponent<Player2_Member1Script>().timeCount;
                if (timeCount < playerTimeCount)
                {
                    Destroy(other.gameObject); // Destroy Player2
                }
                else
                {
                    Destroy(gameObject); // Destroy Player1
                }
            }
            else if (other.gameObject == player2_2)
            {
                playerTimeCount = player2_2.GetComponent<Player2_Member2Script>().timeCount;
                if (timeCount < playerTimeCount)
                {
                    Destroy(other.gameObject); // Destroy Player2
                }
                else
                {
                    Destroy(gameObject); // Destroy Player1
                }
            }
            else if (other.gameObject == player2_3)
            {
                playerTimeCount = player2_3.GetComponent<Player2_Member3Script>().timeCount;
                if (timeCount < playerTimeCount)
                {
                    Destroy(other.gameObject); // Destroy Player2
                }
                else
                {
                    Destroy(gameObject); // Destroy Player1
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Base1"))
        {
            textTime.text = timeCount.ToString("F2");
            isInsideBase = true;
        }
    }
}
