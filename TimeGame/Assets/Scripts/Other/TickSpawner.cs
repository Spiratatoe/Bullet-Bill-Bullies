using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private GameObject messagePrefab;

    public void SpawnMessage(string msg) {
        var messageObj = Instantiate(messagePrefab,  GetSpawnPosition(), Quaternion.identity);
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GameObject.Find("DamageTickCanvas").GetComponent<Canvas>().worldCamera = mainCamera;
        messageObj.GetComponent<FloatingTick>().SetMessage(msg);
    }

    public Vector3 GetSpawnPosition() {
       return transform.position + (Vector3) initialPosition;
    }

}
