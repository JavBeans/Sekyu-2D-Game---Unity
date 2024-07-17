using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject panel;
    bool pressedKey = true;
    
    void Update()
    {
        if (Input.anyKeyDown && pressedKey == true)
        {
            howtoplay();
        }
    }
    void howtoplay()
    {
        pressedKey = false;
        panel.SetActive(false);
    }
}
