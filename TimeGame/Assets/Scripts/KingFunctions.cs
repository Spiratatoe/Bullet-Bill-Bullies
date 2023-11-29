using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class KingFunctions : MonoBehaviour
{
    public GameObject chicken;
    public GameObject forkL;
    public GameObject forkR;
    public GameObject deadKing;
    public Transform player;
    private Animator mAnimator;
    private float startTime;
    private float timeElapsed;
    //private float initialTime;
    private float initialElapsed;
    private float toAttack;
    private int nbForks;
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    

    //used for time stop ability 
    private float animatorSpeed;
    
    //so we know when intro is done to start combat
    public Boolean intro = false;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        nbForks = 0;
        animatorSpeed = mAnimator.speed;
    }

    private void Update()
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
        {   //start of boss fight 
            
            //get every instance of 3 seconds passed
            timeElapsed = Time.time - startTime;
            if(timeElapsed >= 4.0f)
            {
                startTime += 4.0f;
                toAttack += 1;
                mAnimator.SetFloat("TimedAttack", toAttack );
            }
            //new phase when boss hp is below 60% 
            if (gameObject.GetComponent<EnemyHP>().getPercentage())
            {
                mAnimator.SetFloat("RAGE", 1 );
            }
        }

        
    }

    public void replaceKing()
    {
        //destroy the forks
        foreach (var obstacleToDestroy in spawnedObstacles) {
            Destroy(obstacleToDestroy);
            Debug.Log("wasDestroyed");
        }
        
        //used when we want the animator to stop and just have a still image of dead king
        deadKing.SetActive(true);
        //script2.SetActive (true);
    
        gameObject.SetActive(false);
    }
    public void chickenThrown()
    {
        toAttack = 0;
        mAnimator.SetFloat("TimedAttack", 0 );
    }
    // spawn a chicken leg 
    public void spawnChicken()
    {
        Instantiate(chicken, new Vector3(player.position.x + 4.0f,-2.5f,16.985f), Quaternion.identity);
        //Instantiate(chicken, new Vector3(player.position.x + 4.5f,-2.5f,16.985f), Quaternion.identity);
    }
    
    //make a fork on right side first 
    public void spawnForkLeft()
    {

            GameObject forksL = Instantiate(forkR, new Vector3(-3.85f,-2.57f,0f), Quaternion.identity);
            spawnedObstacles.Add(forksL);
            mAnimator.SetBool("ForkLeft", true);
        
        
    }
    public void spawnForkRight()
    {
        
            GameObject forksR = Instantiate(forkL, new Vector3(3.14f,-2.57f,0f), Quaternion.identity);
            spawnedObstacles.Add(forksR);
            mAnimator.SetBool("ForkRight", true);
  
    }

    public void introDone()
    {
        intro = true;
        mAnimator.SetBool("Intro", true );
    }

    IEnumerator fork()
    {
        if (nbForks == 0)
        {
            GameObject forksR = Instantiate(forkR, new Vector3(-3.85f,-2.57f,0f), Quaternion.identity);
            spawnedObstacles.Add(forksR);
            nbForks += 1;
            yield return new WaitForSeconds(3); //if not the animation will spawn both forks
        }
        if (nbForks == 1)
        {
            GameObject forksL =Instantiate(forkL, new Vector3(3.14f,-2.57f,0f), Quaternion.identity);
            spawnedObstacles.Add(forksL);
            nbForks += 1;
        }
    }
    
    //maybe storm of chicken attack after ?
    
    

}
