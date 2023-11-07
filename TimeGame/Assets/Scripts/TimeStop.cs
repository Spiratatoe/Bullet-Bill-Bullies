using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    [SerializeField] float stopTime = 2f;
    [SerializeField] float cooldown = 2f;
    //[SerializeField] Rigidbody2D[] bodies;
    private bool canStop = true;
    public static bool timeStopped = false;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetButtonDown("Ability") && canStop)
        {
            StartCoroutine(DoTimeStop());
        }
    }


     IEnumerator DoTimeStop()
    {
        //Update booleans
        canStop = false;
        timeStopped = true;

        //for (int i=0; i < bodies.Length; i++)
        //{
        //    bodies[i].constraints = RigidbodyConstraints2D.FreezeAll;
        //}

        //stop for time:
        yield return new WaitForSeconds(stopTime);
        timeStopped = false;

		//for (int i = 0; i < bodies.Length; i++)
		//{
		//	bodies[i].constraints = RigidbodyConstraints2D.None;
		//	bodies[i].constraints = RigidbodyConstraints2D.FreezeRotation;
		//}



        //wait for cooldown
		yield return new WaitForSeconds(cooldown);
		canStop = true;
	}
}
