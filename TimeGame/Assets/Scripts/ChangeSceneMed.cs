using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeSceneMed : MonoBehaviour
{

    public GameObject canvas;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.Q))
        {

            if (SceneManager.GetActiveScene().name == "Medieval")
            {

                Debug.Log("does this collide");
                SceneManager.LoadScene("MedievalBoss");
            }


            else if (SceneManager.GetActiveScene().name == "Prehistoric")
            {
                Debug.Log("does this collide");
                SceneManager.LoadScene("PrehistoricBoss");
            }

        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canvas.SetActive(false);
    }
}

