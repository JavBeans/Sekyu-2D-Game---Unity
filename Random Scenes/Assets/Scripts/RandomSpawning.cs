using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    public GameObject[] Prefabs;
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
    // Update is called once per frame

    void RandomSpawn()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-width / 2, width / 2),
            Random.Range(-height / 2, height / 2),0);

        int randomIndex = Random.Range(0, Prefabs.Length);
        GameObject selectedPrefab = Prefabs[randomIndex];

        Instantiate(selectedPrefab, randomPos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height, 0));
    }
}
