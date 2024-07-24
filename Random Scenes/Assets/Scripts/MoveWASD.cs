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

    public GameObject Arrow;
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

    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerSwitch();
        HandleMovement();
        HandleInactivePlayers();
        UpdateArrowPosition();
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
        if (player != null)
        {
            activePlayer = player;
            rb = player.GetComponent<Rigidbody2D>();
            StartsMove = true;
            UpdateArrowPosition();
        }
    }

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        if (StartsMove && rb != null)
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
            rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);
        }
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
        if (player != null)
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector3 direction = (base1.position - player.transform.position).normalized;
                float distance = Vector3.Distance(base1.position, player.transform.position);
                if (distance > stopDistance)
                {
                    playerRb.velocity = direction * speed * Time.deltaTime;
                }
                else
                {
                    playerRb.velocity = Vector2.zero; // Stop movement
                }
            }
        }
    }
    void UpdateArrowPosition()
    {
        if (activePlayer == null)
        {
            Arrow.SetActive(false);
        }
        else if (Arrow != null && activePlayer != null)
        {
            Arrow.SetActive(true);
            Arrow.transform.position = activePlayer.transform.position + offset1;
        }
    }

}
