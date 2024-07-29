using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutScript : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public float timetofade;

    void Update()
    {
        if (fadeIn == true)
        {
            if (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += timetofade * Time.deltaTime;
                if (canvasgroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut == true)
        {
            if (canvasgroup.alpha >= 0)
            {
                canvasgroup.alpha -= timetofade * Time.deltaTime;
                if (canvasgroup.alpha <= 0)
                {
                    canvasgroup.alpha = 0;
                    fadeOut = false;
                }
            }
        }
    }
    public void Fadein()
    {
        fadeIn = true;
        fadeOut = false;
    }
    public void Fadeout()
    {
        fadeOut = true;
        fadeIn = false;
    }
}
