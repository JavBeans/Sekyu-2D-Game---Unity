using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.5f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        float newY = originalPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);
    }
}
