using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordFade : MonoBehaviour
{
    public float fadeDuration = 1f;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeDuration));
            yield return null;
        }
        Destroy(gameObject); // Destroy the text object after fading out
    }
}
