using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] dead;

    // Update is called once per frame

    void Update()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].tag == "Prisoner1")
            {
                dead[i].SetActive(true);
            }
        }
    }
}
