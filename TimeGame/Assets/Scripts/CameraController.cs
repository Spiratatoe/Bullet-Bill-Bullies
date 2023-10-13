using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //we cant just drag in the player because it wont work when changing scenes!
        target = PlayerControls.transform; 
    }

    // Update is called once per frame
    void LateUpdate() //different then update, used so there is no lag between player and camera 
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z );   
    }
}
