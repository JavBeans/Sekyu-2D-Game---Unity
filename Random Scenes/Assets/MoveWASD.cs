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

    private Rigidbody2D rb;
    private GameObject activePlayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
        StartsMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerSwitch();
        HandleMovement();

        Vector3 targetPosition1 = player1Location.position + offset1;
        Vector3 targetPosition2 = player1Location.position + offset2;
        textPlayer1.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textPlayer1Label.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);
    }
    void HandlePlayerSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activePlayer = P1;
            rb = P1.GetComponent<Rigidbody2D>();
            StartsMove = true;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activePlayer = P2;
            rb = P2.GetComponent<Rigidbody2D>();
            StartsMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activePlayer = P3;
            rb = P3.GetComponent<Rigidbody2D>();
            StartsMove = true;
        }
    }

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        if (StartsMove == true)
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
}
