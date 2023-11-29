using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    private float speed = 0.001f;

    //objects to stop during timestop
    private Rigidbody2D rb;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //get components
        rb = gameObject.GetComponent<Rigidbody2D>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        ps.Play();
    }

    // Update is called once per frame
    void Update()
    {

        //timestop constraints
        if (TimeStop.timeStopped)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            ps.Pause();
            return;
        }
        else
        { 
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            if (!ps.isPlaying)
            {
                ps.Play();
            }
        }
        
        transform.position += -transform.up * speed;
      
        

        // destroy asteroid if below equal or below -2 in the y position
        if (transform.position.y <= -2)
        {
            Destroy(gameObject);
        }

    }





    // -1 player hp if the asteroid hits them
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerControls playerComponent = collision.gameObject.GetComponent<PlayerControls>();
            playerComponent.TakeDamage((int)1);
         
        }


        // destory object when it comes in contact with anything 
        Destroy(gameObject);

    }
}
