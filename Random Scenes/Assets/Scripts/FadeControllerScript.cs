using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControllerScript : MonoBehaviour
{
    FadeInOutScript fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOutScript>();
        fade.Fadeout();
    }
}
