using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    [SerializeField] private int damageAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnParticleCollision(GameObject collision)
    {
        Debug.Log("Hitting player");
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().TakeDamage(damageAmount);
        }
    }

}
