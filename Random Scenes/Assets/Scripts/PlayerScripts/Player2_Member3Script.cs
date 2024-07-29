using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player2_Member3Script : MonoBehaviour
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
    public Transform BasePrisonLocation;
    public Vector3 offset1;
    public Vector3 offset2;

    public Text textPlayer;
    public Text textTime;
    public Text textSprint;
    public Text textFatigue;

    private bool isInsideBase;
    private bool isCaptured;

    public GameObject player1_1;
    public GameObject player1_2;
    public GameObject player1_3;


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

        if (MoveArrows.SwitcherNumber == 3)
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
        textPlayer.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
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
        if (timeCount > otherPlayerTimeCount)
        {
            isCaptured = true;
            StartCoroutine(CapturePlayer(this.gameObject)); //capture This Player
            textTime.text = timeCount.ToString("Captured");
            textPlayer.gameObject.SetActive(false);
        }
    }
    private IEnumerator CapturePlayer(GameObject player)
    {
        player.tag = "Prisoner1";
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            while (Vector3.Distance(player.transform.position, BasePrisonLocation.position) > 0.1f)
            {
                Vector3 direction = (BasePrisonLocation.position - player.transform.position).normalized;
                playerRb.velocity = direction * speed;
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

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        float currentSpeed = speed;
        if (MoveArrows.SwitcherNumber == 3 && rb != null && gameObject.tag != "Prisoner1")
        {
            if (Input.GetKey(KeyCode.RightShift) && sprintValue > 0)
            {
                currentSpeed = sprintSpeed;
                sprintValue = Mathf.Max(sprintValue - sprintReductionRate * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MovementY = 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MovementY = -1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MovementX = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
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
