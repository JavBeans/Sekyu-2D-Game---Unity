using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player1_Member2Script : MonoBehaviour
{
    public float speed;
    public float slowspeed;
    public float sprintSpeed;
    public static float sprintReductionRate = 20f;
    public static float fatigueReductionRate = 20f;
    public static float fatigueRecoverRate = 10f;
    float MovementX;
    float MovementY;
    public float timeCount;
    public float sprintValue = 100f;
    public float fatigueValue = 100f;

    private Rigidbody2D rb;
    public Transform playerLocation;
    public Transform Base1PrisonLocation;
    public Vector3 offset1;
    public Vector3 offset2;

    public Text textPlayer1;
    public Text textTime;
    public Text textSprint;
    public Text textFatigue;

    private bool isInsideBase;
    private bool isCaptured;

    public GameObject player2_1;
    public GameObject player2_2;
    public GameObject player2_3;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (GameManagerScript.gameStarted == true)
        {
            HandleMovement();
        }

        if (MoveWASD.SwitcherNumber == 2)
        {
            textSprint.text = "Sprint: " + sprintValue.ToString("F0") + "%";
            textFatigue.text = "Fatigue: " + fatigueValue.ToString("F0") + "%";
        }

        if (isInsideBase && isCaptured == false)
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
                HandlePlayerCollision(player2_1, playerTimeCount);
            }
            else if (other.gameObject == player2_2)
            {
                playerTimeCount = player2_2.GetComponent<Player2_Member2Script>().timeCount;
                HandlePlayerCollision(player2_2, playerTimeCount);
            }
            else if (other.gameObject == player2_3)
            {
                playerTimeCount = player2_3.GetComponent<Player2_Member3Script>().timeCount;
                HandlePlayerCollision(player2_3, playerTimeCount);
            }
        }
    }
    private void HandlePlayerCollision(GameObject otherPlayer, float otherPlayerTimeCount)
    {
        if (timeCount > otherPlayerTimeCount)
        {
            isCaptured = true;
            StartCoroutine(CapturePlayer(this.gameObject)); //capture This Player
            textTime.text = timeCount.ToString("Captured");
            textPlayer1.gameObject.SetActive(false);
        }
    }
    private IEnumerator CapturePlayer(GameObject player)
    {
        player.tag = "Prisoner1";
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            while (Vector3.Distance(player.transform.position, Base1PrisonLocation.position) > 0.1f)
            {
                Vector3 direction = (Base1PrisonLocation.position - player.transform.position).normalized;
                playerRb.velocity = direction * speed;
                yield return null;
            }
            playerRb.velocity = Vector2.zero; // Stop player movement
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

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        float currentSpeed = speed;
        if (MoveWASD.SwitcherNumber == 2 && rb != null && gameObject.tag != "Prisoner1")
        {
            if (Input.GetKey(KeyCode.LeftShift) && sprintValue > 0)
            {
                currentSpeed = sprintSpeed;
                sprintValue = Mathf.Max(sprintValue - sprintReductionRate * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                MovementY = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                MovementY = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                MovementX = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                MovementX = 1;
            }
            if (MovementX != 0 || MovementY != 0 && fatigueValue > 0)
            {
                if (fatigueValue > 0)
                {
                    fatigueValue = Mathf.Max(fatigueValue - fatigueReductionRate * Time.deltaTime, 0);
                }
                else
                {
                    currentSpeed = slowspeed;
                }
            }
            else
            {
                if (fatigueValue < 100)
                {
                    fatigueValue = Mathf.Max(fatigueValue + fatigueRecoverRate * Time.deltaTime, 0);
                }
            }
        }
        else
        {
            if (fatigueValue < 100)
            {
                fatigueValue = Mathf.Max(fatigueValue + fatigueRecoverRate * Time.deltaTime, 0);
            }
        }
        rb.velocity = new Vector2(MovementX * currentSpeed, MovementY * currentSpeed);
    }
}
