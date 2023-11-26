


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public List<GameObject> asteroidPrefab;
    public float spawnInterval = 0.2f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnAsteroid()
    {
        // random integer for choosing asteroid prefab
        int randomInteger = Random.Range(0, asteroidPrefab.Count);


        // camera viewport

        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // get random coordinates for viewport
        float randomViewportX = Random.Range(0f, 1f);
        float randomViewportY = Random.Range(0f, 1f);

        // change from viewport to world coordinates
        Vector3 spawnPositionViewport = new Vector3(randomViewportX, 1f, mainCamera.nearClipPlane);
        Vector3 spawnPositionWorld = mainCamera.ViewportToWorldPoint(spawnPositionViewport);

        // spawn asteroids
        Instantiate(asteroidPrefab[randomInteger], spawnPositionWorld, Quaternion.identity);
    }
}