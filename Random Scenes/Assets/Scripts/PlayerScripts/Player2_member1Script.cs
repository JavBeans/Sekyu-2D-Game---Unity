using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player2_Member1Script : MonoBehaviour
{
    public float speed;
    public float slowspeed;
    public float sprintSpeed;
    public static float sprintReductionRate = 20f;
    public static float staminaReductionRate = 5f;
    public static float staminaRecoverRate = 3f;
    float MovementX;
    float MovementY;
    public float timeCount;
    public float sprintValue = 100f;
    public float staminaValue = 100f;
    public float MaxstaminaValue = 100f;
    public float MaxsprintValue = 100f;

    private Rigidbody2D rb;
    public Transform playerLocation;
    public Transform BasePrisonLocation;
    public Vector3 offset1;
    public Vector3 offset2;

    public TextMeshProUGUI textPlayer;
    public Text textTime;

    private bool isInsideBase;
    private bool isCaptured;

    private Coroutine speedCoroutine;

    public GameObject player1_1;
    public GameObject player1_2;
    public GameObject player1_3;

    public ParticleSystem dust;

    public Image staminaBar;
    public Image sprintBar;

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

        if (MoveArrows.SwitcherNumber == 1 && !isCaptured)
        {
            staminaBar.fillAmount = staminaValue / MaxstaminaValue;
            sprintBar.fillAmount = sprintValue / MaxsprintValue;
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
        if (sprintValue > MaxsprintValue)
        {
            sprintValue = 100;
        }
        if (staminaValue > MaxstaminaValue)
        {
            staminaValue = 100;
        }
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
        if (other.gameObject.CompareTag("Water"))
        {
            sprintValue += 50;
            staminaValue += 100;

            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Slipper"))
        {
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            speedCoroutine = StartCoroutine(IncreaseSpeedForDuration(3, 3)); // Increase speed by 3 for 3 seconds
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Mud"))
        {
            if (speedCoroutine != null)
            {
                StopCoroutine(speedCoroutine);
            }
            speedCoroutine = StartCoroutine(DecreaseSpeedForDuration(1, 3)); // Decrease speed by 1 for 3 seconds
            Destroy(other.gameObject);
        }
    }

    private IEnumerator IncreaseSpeedForDuration(float amount, float duration)
    {
        speed = amount;
        yield return new WaitForSeconds(duration);
        speed = 2;
    }

    private IEnumerator DecreaseSpeedForDuration(float amount, float duration)
    {
        speed = amount;
        yield return new WaitForSeconds(duration);
        speed = 2;
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
                playerRb.velocity = direction * 10;
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
        if (MoveArrows.SwitcherNumber == 1 && rb != null && gameObject.tag != "Prisoner1")
        {
            if (Input.GetKey(KeyCode.RightShift) && sprintValue > 0)
            {
                dust.Play();
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
            if (MovementX != 0 || MovementY != 0 && staminaValue > 0)
            {
                if (staminaValue > 0)
                {
                    staminaValue = Mathf.Max(staminaValue - staminaReductionRate * Time.deltaTime, 0);
                }
                else
                {
                    currentSpeed = slowspeed;
                }
            }
            else
            {
                if (staminaValue < 100)
                {
                    staminaValue = Mathf.Max(staminaValue + staminaRecoverRate * Time.deltaTime, 0);
                }
            }
        }
        else
        {
            if (staminaValue < 100)
            {
                staminaValue = Mathf.Max(staminaValue + staminaRecoverRate * Time.deltaTime, 0);
            }
        }
        rb.velocity = new Vector2(MovementX * currentSpeed, MovementY * currentSpeed);
    }
}
