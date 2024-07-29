using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveArrows : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;

    public Transform base1; // If I switch to player to player, the ones I didn't control at that time will go back to this base1.
    public Vector3 offset1;

    public static int SwitcherNumber;

    private Rigidbody2D rb;
    private GameObject activePlayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SwitcherNumber = 1;
        SwitchToPlayer(P1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerScript.gameStarted == true)
        {
            HandlePlayerSwitch();
        }
        UpdateArrowPosition();
    }

    void HandlePlayerSwitch()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchToPlayer(P1);
            SwitcherNumber = 1;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchToPlayer(P2);
            SwitcherNumber = 2;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchToPlayer(P3);
            SwitcherNumber = 3;
        }
    }

    void SwitchToPlayer(GameObject player)
    {
        if (player != null && player.tag != "Prisoner1")
        {
            activePlayer = player;
            rb = player.GetComponent<Rigidbody2D>();
            UpdateArrowPosition();
        }
    }

    /*void HandleInactivePlayers()
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
    }*/

    void UpdateArrowPosition()
    {
        if (activePlayer.tag == "Prisoner1")
        {
            Arrow.SetActive(false);
        }
        else if (Arrow != null && activePlayer != null && activePlayer.tag != "Prisoner1")
        {
            Arrow.SetActive(true);
            Arrow.transform.position = activePlayer.transform.position + offset1;
        }
    }
}
