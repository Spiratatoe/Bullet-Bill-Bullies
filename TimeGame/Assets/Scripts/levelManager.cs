using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public List<GameObject> asteroidPrefab;
    public float spawnTime = 0.5f;

    void Start()
    {
        StartCoroutine(spawnAsteroids());
    }

    IEnumerator spawnAsteroids()
    {
        while (true)
        {
            int randomInteger = Random.Range(0, asteroidPrefab.Count);
            Instantiate(asteroidPrefab[randomInteger], new Vector3(Random.Range(-25f, 50f), 6f, 0f), Quaternion.identity);
            randomInteger = Random.Range(0, asteroidPrefab.Count);
            Instantiate(asteroidPrefab[randomInteger], new Vector3(Random.Range(-25f, 50f), 6f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
