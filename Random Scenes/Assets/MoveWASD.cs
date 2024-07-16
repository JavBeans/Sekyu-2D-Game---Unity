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

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MovementX = 0;
        MovementY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(MovementX * speed * Time.deltaTime, MovementY * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W))
        {
            MovementY = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovementY = -1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovementX = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovementX = 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            MovementX = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            MovementX = 0;
        }
        Vector3 targetPosition1 = player1Location.position + offset1;
        Vector3 targetPosition2 = player1Location.position + offset2;
        textPlayer1.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textPlayer1Label.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);
        if(Base1Script.playernumber1 == 2)
        {
            textPlayer1Label.text = "Predator";
        }
        else
        {
            textPlayer1Label.text = "Prey";
        }
    }
}
