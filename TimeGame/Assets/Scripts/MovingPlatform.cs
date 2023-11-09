using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points; //array of transform points where the platform travels to
    private int i;//index  of points array
  
    void Start()
    {
        transform.position = points[0].position;
		i = 0;
    }

    void Update()
	{
		if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
		{
			i++;
			if (i == points.Length)
			{
				i = 0;
			}
		}
		if (!TimeStop.timeStopped) //stop moving if the player used timestop
		{
			transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
		}
		
	}

	//These functions allow the player to move with the platform
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.position.y > transform.position.y && collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(transform);
		}	
	}
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.transform.position.y > transform.position.y && collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(transform);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(null);
		}
	}


	private void OnDrawGizmos()
	{
		for (int i =0; i<points.Length-1; i++)
        {
			Gizmos.DrawWireSphere(points[i].position, 0.2f);
			Gizmos.DrawLine(points[i].position, points[i+1].position);
		}
		Gizmos.DrawWireSphere(points[points.Length-1].position, 0.2f);
	}
}
