
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

            if (distanceToPlayer > attackRange)
            {
                Idle();
            }
            else
            {
                if (Time.time > nextThrowTime)
                {
                    ThrowSpikes();
                    nextThrowTime = Time.time + throwSpikesCooldown;
                }
                else if (distanceToPlayer < tailAttackRange && IsPlayerBehind())
                {
                    TailAttack();
                }
                else
                {
                    Attack();
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


   



    void SpikeThrowingAttack()
    {
        // Implement spike throwing logic
        // For example, instantiate the spikeRain prefab
        Instantiate(spikeRain, new Vector3(player.position.x + 4.0f, -2.5f, 16.985f), Quaternion.identity);
    }




  

    public void introDone()
    {
        intro = true;
        mAnimator.SetBool("Intro", true);
    }


    bool IsPlayerBehind()
    {
        Vector3 toPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, toPlayer);
        return Mathf.Abs(angle) > 90f;
    }

    void Idle()
    {
        // Set animation bools
        mAnimator.SetBool("isIdle", true);
        mAnimator.SetBool("rollAttack", false);
        mAnimator.SetBool("tailAttack", false);
    }

    void Attack()
    {
        // Set animation bools
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("rollAttack", true);
        mAnimator.SetBool("tailAttack", false);
    }

    void TailAttack()
    {
        // Set animation bools
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("throwSpikes", false);
        mAnimator.SetBool("tailAttack", true);
    }

    void ThrowSpikes()
    {
        // Set animation bools
        mAnimator.SetBool("isIdle", false);
        mAnimator.SetBool("throwSpikes", true); // You can use the same attack animation for throwing spikes
        mAnimator.SetBool("tailAttack", false);

        // Logic to throw spikes at player's initial position

        Vector3 playerPosition = player.position;
        Instantiate(spikeRain, new Vector3(playerPosition.x + 4.0f, -2.5f, 16.985f), Quaternion.identity);

        // Implement your logic for throwing spikes at player's initial position
        // Instantiate spikes, apply force, etc.
    }


}
