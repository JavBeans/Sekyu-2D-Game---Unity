using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    public GameObject[] Prefabs;
    public GameObject textPrefab;
    public float width = 1;
    public float height = 1;
    public float spawnInterval = 5f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void RandomSpawn()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2), 0);

        int randomIndex = Random.Range(0, Prefabs.Length);
        GameObject selectedPrefab = Prefabs[randomIndex];

        GameObject spawnedObject = Instantiate(selectedPrefab, randomPos, Quaternion.identity);
        DestroyableObject destroyable = spawnedObject.AddComponent<DestroyableObject>();
        destroyable.textPrefab = textPrefab;
        destroyable.identifier = randomIndex; // Set the identifier based on the prefab index
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height, 0));
    }
}

public class DestroyableObject : MonoBehaviour
{
    public GameObject textPrefab;
    public int identifier; // Add an identifier to determine which text to show

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            ShowText();
        }
    }

    private void ShowText()
    {
        Vector3 textPosition = Camera.main.WorldToScreenPoint(transform.position);
        GameObject textInstance = Instantiate(textPrefab, textPosition, Quaternion.identity, GameObject.Find("Canvas").transform);

        TextMeshProUGUI textComponent = textInstance.GetComponent<TextMeshProUGUI>();
        switch (identifier)
        {
            case 0:
                textComponent.text = "Hydrated!";
                break;
            case 1:
                textComponent.text = "Speed+!";
                break;
            case 2:
                textComponent.text = "Slowed!";
                break;
        }

        StartCoroutine(FadeOutAndDestroy(textComponent));
    }

    private IEnumerator FadeOutAndDestroy(TextMeshProUGUI textComponent)
    {
        float fadeDuration = 1f;
        Color originalColor = textComponent.color;

        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            textComponent.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeDuration));
            yield return null;
        }
        Destroy(textComponent.gameObject); // Destroy the text object after fading out
    }
}
