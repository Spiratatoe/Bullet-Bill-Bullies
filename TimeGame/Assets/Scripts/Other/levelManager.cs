


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public List<GameObject> asteroidPrefab;
    public float spawnInterval = 0.2f;
    private Camera mainCamera;
    public GameObject player; 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {

            // because the asteroids are spawned withtin the camera view frame, asteroids can be spawned in cases where
            // the ceiling is far above the camera y fieldview. So, to prevent this, I specified the x positions where 
            // the asteroids can spawn 
            float playerPositionX = player.transform.position.x;
            float playerPositionY = player.transform.position.y;
            if (playerPositionX < 21f || (playerPositionX > 26.4f && playerPositionX < 31.6f) || playerPositionX > 47.5f || playerPositionY >= 3)
            {
                SpawnAsteroid();
            }
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