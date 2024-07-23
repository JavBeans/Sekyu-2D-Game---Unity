using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveWASD : MonoBehaviour
{
    public float speed;
    float MovementX;
    float MovementY;
    public Transform player1Location;
    public Vector3 offset1;
    public Vector3 offset2;
    public Text textPlayer1;
    public Text textPlayer1Label;
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    private bool StartsMove;
    public Transform base1; // If I switch to player to player, the ones I didn't control at that time will go back to this base1.
    private float stopDistance = 0.1f;

    private Rigidbody2D rb;
    private GameObject activePlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        StartsMove = false;
        rb = P1.GetComponent<Rigidbody2D>(); // Set the initial Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerSwitch();
        HandleMovement();
        HandleInactivePlayers();

        Vector3 targetPosition1 = player1Location.position + offset1;
        Vector3 targetPosition2 = player1Location.position + offset2;
        textPlayer1.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textPlayer1Label.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);
    }

    void HandlePlayerSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToPlayer(P1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToPlayer(P2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToPlayer(P3);
        }
    }

    void SwitchToPlayer(GameObject player)
    {
        activePlayer = player;
        rb = player.GetComponent<Rigidbody2D>();
        StartsMove = true;
    }

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        if (StartsMove)
        {
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
        }
        rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);
    }

    void HandleInactivePlayers()
    {
        if (activePlayer != P1)
        {
            MoveToBase(P1);
        }
        if (activePlayer != P2)
        {
            MoveToBase(P2);
        }
        if (activePlayer != P3)
        {
            MoveToBase(P3);
        }
    }

    void MoveToBase(GameObject player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        Vector3 direction = (base1.position - player.transform.position).normalized;
        float distance = Vector3.Distance(base1.position, player.transform.position);
        if (distance > stopDistance)
        {
            playerRb.velocity = direction* speed * Time.deltaTime;
        }
        else
        {
            playerRb.velocity = Vector2.zero; // Stop movement
        }
    }
    
}
