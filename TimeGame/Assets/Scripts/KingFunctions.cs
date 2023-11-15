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
    public Transform player;
    private Animator mAnimator;
    private float startTime;
    private float timeElapsed;
    private float initialTime;
    private float initialElapsed;
    private float toAttack;
    private int nbForks;
    
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        startTime = Time.time - 3.0f;
        initialTime = Time.time;
        nbForks = 0;
    }

    private void Update()
    {
        timeElapsed = Time.time - startTime;
        if(timeElapsed >= 3.0f)
        {
            startTime += 3.0f;
            toAttack += 1;
            mAnimator.SetFloat("TimedAttack", toAttack );
        }

        initialElapsed = Time.time - initialTime;
        if (initialElapsed >= 25.0f)
        {
            mAnimator.SetFloat("RAGE", 1 );
        }





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

            Instantiate(forkR, new Vector3(-3.85f,-2.57f,0f), Quaternion.identity);
            mAnimator.SetBool("ForkLeft", true);
        
        
    }
    public void spawnForkRight()
    {
        
            Instantiate(forkL, new Vector3(3.14f,-2.57f,0f), Quaternion.identity);
            mAnimator.SetBool("ForkRight", true);
  
    }

    IEnumerator fork()
    {
        if (nbForks == 0)
        {
            Instantiate(forkR, new Vector3(-3.85f,-2.57f,0f), Quaternion.identity);
            nbForks += 1;
            yield return new WaitForSeconds(3);
        }
        if (nbForks == 1)
        {
            Instantiate(forkL, new Vector3(3.14f,-2.57f,0f), Quaternion.identity);
            nbForks += 1;
        }
    }
    
    //maybe storm of chicken attack after ?
    
    

}
