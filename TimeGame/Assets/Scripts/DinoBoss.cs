
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class DinoBoss : MonoBehaviour
{

    public GameObject spikeRain;
    public GameObject deadDino;
    public Transform player;
    private Animator mAnimator;
    private float startTime;
    private float timeElapsed;
    //private float initialTime;
    private float initialElapsed;
    private float toAttack;
   
    private List<GameObject> spawnedObstacles = new List<GameObject>();


    float rollSpeed = 5.0f; 
    float rollDistance = -1.5f; 
    public float attackRange = 5f;
    public float tailAttackRange = 3f;
    public float throwSpikesCooldown = 3f;
    private float nextThrowTime = 0f;

    //used for time stop ability 
    private float animatorSpeed;

    //so we know when intro is done to start combat
    public Boolean intro = true;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
        mAnimator.SetBool("isIdle", true);
        animatorSpeed = mAnimator.speed;
    }

    // Update is called once per frame

    // when player is behind boss - TAIL ATTACK 
    // when player is at a certain distance away from boss (in the left direction ) roll attack towards the left 
    // when the player is just around , throw spikes (rain attack) 


    void Update()
    {
        //return if time is stopped
        if (TimeStop.timeStopped)
        {
            mAnimator.speed = 0;
            return;
        }
        else
        {
            mAnimator.speed = animatorSpeed;
        }

        //before intro is done
        if (!intro)
        {
            startTime = Time.time;
            //initialTime = Time.time;
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);


            if (player.position.x <= -6f)
            {
                rollAttack();
            }


          //  if (distanceToPlayer > attackRange)
          //  {
          //     // Idle();
         //   }
            else
            {
                if (Time.time > nextThrowTime)
                {
                    throwSpikes();
                    nextThrowTime = Time.time + throwSpikesCooldown;
                }
                else if (isPlayerBehind())
                {
                    tailAttack();
                }
                else
                {
                    idle();
                }
            }
        }
    }



    public void replaceDino()
    {
      

        //used when we want the animator to stop and just have a still image of dead king
        deadDino.SetActive(true);
        //script2.SetActive (true);

        gameObject.SetActive(false);
    }



  

    public void introDone()
    {
        intro = true;
        mAnimator.SetBool("Intro", true);
    }


    bool isPlayerBehind()
    {
        Vector3 toPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, toPlayer);
        return Mathf.Abs(angle) > 90f;
    }

    void idle()
    {
        
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("rollAttack", false);
        mAnimator.SetBool("tailAttack", false);
        mAnimator.SetBool("throwSpikes", false);
    }



    IEnumerator rollAttack()
    {
        
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("rollAttack", true);
        mAnimator.SetBool("tailAttack", false);
        mAnimator.SetBool("rollAttack", true);


        Vector3 targetosition = new Vector3(transform.position.x + rollDistance, transform.position.y, transform.position.z);

        // recording time so that the rolling attack / animation stays until the distance is covered 
        float startTime = Time.time;

        while (Time.time < startTime + Mathf.Abs(rollDistance) / rollSpeed)
        {
            transform.Translate(Vector3.left * rollSpeed * Time.deltaTime);
            yield return null;
        }

        // Move back to the original position
        while (transform.position.x < targetPosition.x)
        {
            transform.Translate(Vector3.right * rollSpeed * Time.deltaTime);
            yield return null;
        }

     
    }

    void tailAttack()
    {
       
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("throwSpikes", false);
        mAnimator.SetBool("tailAttack", true);
        mAnimator.SetBool("rollAttack", false);
    }

    void throwSpikes()
    {
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("throwSpikes", true); 
        mAnimator.SetBool("tailAttack", false);
        mAnimator.SetBool("rollAttack", false);

    

        Vector3 playerPosition = player.position;
        Instantiate(spikeRain, new Vector3(playerPosition.x + 4.0f, -2.5f, 16.985f), Quaternion.identity);

     
    }


}
