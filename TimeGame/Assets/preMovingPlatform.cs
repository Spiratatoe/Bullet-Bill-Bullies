using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preMovingPlatform : MonoBehaviour
{

    public Transform pointA, pointB;
    public int speed;
    Vector2 targetPosition; 
    // Start is called before the first frame update
    void Start()
    {
        targetPosition  = pointB .position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < .1f)
        {

            targetPosition = pointB.position;
        }

        if (Vector2.Distance(transform.position, pointB.position) < .1f)
        {
            targetPosition = pointA.position;

        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
