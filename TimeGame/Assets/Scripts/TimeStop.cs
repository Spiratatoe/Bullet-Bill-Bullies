using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeStop : MonoBehaviour
{
    [SerializeField] int stopTime = 2;
    [SerializeField] int cooldown = 2;
    [SerializeField] private TextMeshProUGUI timerText;
    //[SerializeField] Rigidbody2D[] bodies;
    private bool canStop = true;
    public static bool timeStopped = false;

    void Start()
    {
        timerText.text = " ";
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
        
        //play sound
        FindObjectOfType<AudioManager>().Play("timestop");
        
        //for (int i=0; i < bodies.Length; i++)
        //{
        //    bodies[i].constraints = RigidbodyConstraints2D.FreezeAll;
        //}

        //stop for time:
        int countdown = stopTime;
        timerText.text = "" + countdown;
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown -= 1;
            timerText.text = "" + countdown;
        }
        
        timeStopped = false;

        //for (int i = 0; i < bodies.Length; i++)
        //{
        //	bodies[i].constraints = RigidbodyConstraints2D.None;
        //	bodies[i].constraints = RigidbodyConstraints2D.FreezeRotation;
        //}



        //wait for cooldown
        countdown = cooldown;
        timerText.text = "" + countdown;
        timerText.color = Color.gray;

        while (countdown > 0)
        {
            yield return new WaitForSeconds(1f);
            countdown -= 1;
            timerText.text = "" + countdown;
        }

        //yield return new WaitForSeconds(cooldown);
        canStop = true;
        timerText.text = " ";
        timerText.color = Color.white;

    }
}
