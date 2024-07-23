using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BasesScript : MonoBehaviour
{
    public GameObject[] playernumbering;
    public Text[] textnumbering;
    public static bool[] isInsideBase;
    public int[] playerValue;

    private void Start()
    {
        // Initialize the isInsideBase array
        isInsideBase = new bool[playernumbering.Length];
        playerValue = new int[playernumbering.Length];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            for (int i = 0; i < playernumbering.Length; i++)
            {
                if (other.gameObject == playernumbering[i])
                {
                    isInsideBase[i] = true;
                    playerValue[i] = 0; // Set playervalue to 0 for this player
                    textnumbering[i].text = $"#{playerValue[i]}"; // Update the corresponding Text element
                    break;
                }

            }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
            for (int i = 0; i < playernumbering.Length; i++)
            {
                if (other.gameObject == playernumbering[i])
                {
                    isInsideBase[i] = false;
                    break;
                }
            }

            for (int i = 0; i < playernumbering.Length; i++)
            {
                if (isInsideBase[i] == false)
                {
                    playerValue[i] += 1;
                    textnumbering[i].text = $"#{playerValue[i]}";
                }
            }
    }
}
