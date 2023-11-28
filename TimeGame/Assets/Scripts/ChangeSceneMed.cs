using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("does this collide");
        SceneManager.LoadScene("MedievalBoss");
        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("does this collide exit");
        //SceneManager.LoadScene("MedievalBoss");
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("it do be staying");
    }
}
