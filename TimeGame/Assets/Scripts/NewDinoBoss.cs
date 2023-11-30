using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class NewDinoBoss : MonoBehaviour
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


    float rollSpeed = 5f;
    float rollDistance = 5f;
    public float attackRange = 5f;
    public float tailAttackRange = 3f;
    public float throwSpikesCooldown = 5f;

    //used for time stop ability 
    private float animatorSpeed;

    //so we know when intro is done to start combat
    public bool intro = true;

    //variable to idicate what its doing
    private bool attacking = false;
    private bool canThrowSpikes = true;
    private bool rollingOut = false;
    private bool rollingIn = false;
    private Vector2 targetPos;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        animatorSpeed = mAnimator.speed;
        startTime = Time.time;

    }

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


        if (!attacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            //ROLL ATTACK
            if (player.position.x <= -6f) 
            {
                //Get positions
                originalPos = new Vector2(transform.position.x, transform.position.y);
                targetPos = new Vector2(transform.position.x - rollDistance, transform.position.y);

                //set booleans
                mAnimator.SetBool("rollAttack", true);
                attacking = true;   //lock out of other attacks
                rollingOut = true; //to start rolling
            }
            //SPIKE ATTACK
            else if (canThrowSpikes)
            {
                StartCoroutine(ThrowSpikes());
            }
            //TAIL ATTACK
            else if (isPlayerBehind())
            {
                StartCoroutine(TailAttack());
            }
            else
            {
                
            }
            
        }

        //Rolling towards target
        if (rollingOut)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, rollSpeed * Time.deltaTime);
            if (transform.position.x <= targetPos.x)
            {
                rollingOut = false;
                rollingIn = true;
            }
        }
        //moving back to original
        if (rollingIn)
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPos, rollSpeed * Time.deltaTime);
            if (transform.position.x >= originalPos.x)
            {
                mAnimator.SetBool("rollAttack", false);
                rollingIn = false;           
                attacking = false;
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
        return (player.position.x >= transform.position.x);

        //Vector3 toPlayer = player.position - transform.position;
        //float angle = Vector3.Angle(transform.forward, toPlayer);
        //return Mathf.Abs(angle) > 90f;
    }

    IEnumerator TailAttack()
    {
        attacking = true;
        mAnimator.SetBool("tailAttack", true);
        yield return new WaitForSeconds(0.8f);
        mAnimator.SetBool("tailAttack", false);
        attacking = false;
    }

    IEnumerator ThrowSpikes()
    {
        Debug.Log("Started");
        //Start Animation
        mAnimator.SetBool("throwSpikes", true);
        attacking = true;
        canThrowSpikes = false;

        yield return new WaitForSeconds(0.6f);  //wait to middle to spawn spikes

        //create spikes
        Vector3 playerPosition = player.position;
        GameObject newSpikes = Instantiate(spikeRain, gameObject.transform.parent);

        newSpikes.transform.position = new Vector3(player.position.x, 6f, -0.03f);

        yield return new WaitForSeconds(0.9f);

        mAnimator.SetBool("throwSpikes", false);
        attacking = false;

        yield return new WaitForSeconds(throwSpikesCooldown);
        canThrowSpikes = true;


    }


}
