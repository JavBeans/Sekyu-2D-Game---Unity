using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject panel;
    static bool howToPlayTriggered;
    public static bool gameStarted;
    public GameObject Sekyu;
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Countdown());
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

    IEnumerator Countdown()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "Isa!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Duwa!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Tatlo!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        Sekyu.SetActive(true);
        yield return new WaitForSeconds(1f);
        Sekyu.SetActive(false);
        gameStarted = true;
    }
}
