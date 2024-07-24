using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveArrows : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    float MovementX;
    float MovementY;

    public Transform player2Location;
    
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;

    private bool StartsMove;
    public Transform base2; // If I switch to player to player, the ones I didn't control at that time will go back to this base2.
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

    }
    
    void HandlePlayerSwitch()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchToPlayer(P1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchToPlayer(P2);
        }
        else if (Input.GetKeyDown(KeyCode.P))
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
        }
    }

    void HandleMovement()
    {
        MovementX = 0;
        MovementY = 0;
        if (StartsMove && rb != null)
        {
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
                Vector3 direction = (base2.position - player.transform.position).normalized;
                float distance = Vector3.Distance(base2.position, player.transform.position);
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
}
