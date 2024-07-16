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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovementY = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovementY = -1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovementX = -1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovementX = 1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            MovementY = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MovementX = 0;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            MovementX = 0;
        }
        Vector3 targetPosition1 = player2Location.position + offset1;
        Vector3 targetPosition2 = player2Location.position + offset2;
        textPlayer2.transform.position = Camera.main.WorldToScreenPoint(targetPosition1);
        textPlayer2Label.transform.position = Camera.main.WorldToScreenPoint(targetPosition2);
        if (Base2Script.playernumber2 == 2)
        {
            textPlayer2Label.text = "Predator";
        }
        else
        {
            textPlayer2Label.text = "Prey";
        }
    }
}
