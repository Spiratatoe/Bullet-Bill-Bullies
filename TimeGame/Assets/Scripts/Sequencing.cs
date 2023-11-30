using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencing : MonoBehaviour
{
    
    //what needs to be done
    //the king wakes up
    //start the dialogue 
    //check when done
    //king can now be damaged and starts attack ... 
    //set king boolean start to go 
    //when boss dies, start newest sequence of dialogue
    
    public GameObject script;
    
    //public GameObject scriptManager; 
    public GameObject king;
    public GameObject DialogueMan;
    public GameObject Portal;


    private float startTime;
    private float currentTime;
    private float timeElapsed;
    private float Timer;
    private bool doOnlyOnce = true;
    private bool doOnlyOnce2 = true;
    private bool doOnlyOnce3 = true;
    //public GameObject script2;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // KingFunctions.GetInstance().chicken;
        currentTime = Time.time;
        timeElapsed = currentTime - startTime;
        if (DialogueManager.GetInstance().done)
        {
            //no need to continue setting the values
            if (doOnlyOnce)
            {
                //this way we dont toggle off later on in sequence when we actually want invincibility
                king.GetComponent<EnemyHP>().toggleInvincibleOff();
                king.GetComponent<KingFunctions>().introDone();
                script.SetActive(false);
                Debug.Log("done thanks");
                doOnlyOnce = false;
            }

            if (king.GetComponent<EnemyHP>().isDead && doOnlyOnce2)
            {
                if (doOnlyOnce3)
                {
                    Timer = Time.time;
                    doOnlyOnce3 = false;
                }
                
                if (currentTime - Timer > 4.0f)
                {
                    //script2.SetActive (true);
                    DialogueMan.GetComponent<DialogueManager>().Reload();
                    doOnlyOnce2 = false;
                    //also spawn in the teleporter
                    Portal.SetActive(true);
                }
                
                
            }
            
        }
        else
        {
            if (timeElapsed < 3.0f)
            {
                // dont start until initial animation is done
                script.SetActive(false);
                // also make sure king invincible
                king.GetComponent<EnemyHP>().toggleInvincibleOn();
                
            }
            else
            {
                // we should still not be able to hit king + no fight can start 
                king.GetComponent<EnemyHP>().toggleInvincibleOn();
                script.SetActive (true);
            }
        }
        
    }

}
