using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject panel;
    static bool howToPlayTriggered;

    private void Start()
    {
        if (howToPlayTriggered == true)
        {
            panel.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            howtoplay();
        }
    }

    void howtoplay()
    {
        howToPlayTriggered = true; // Set the static variable to true
        panel.SetActive(false);
    }
}
