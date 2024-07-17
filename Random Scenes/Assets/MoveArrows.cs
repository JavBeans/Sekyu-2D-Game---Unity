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
    public Vector3 offset1;
    public Vector3 offset2;
    public Text textPlayer2;
    public Text textPlayer2Label;
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

        Vector3 targetPosition1 = player2Location.position + offset1;
        Vector3 targetPosition2 = player2Location.position + offset2;
        textPlayer2.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textPlayer2Label.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);
    }
    void HandlePlayerSwitch()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activePlayer = P1;
            rb = P1.GetComponent<Rigidbody2D>();
            StartsMove = true;

        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            activePlayer = P2;
            rb = P2.GetComponent<Rigidbody2D>();
            StartsMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.P))
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
        }
        rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);
    }
}
