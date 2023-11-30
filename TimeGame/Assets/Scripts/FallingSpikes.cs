using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private ParticleSystem ps;
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        //timestop constraints
        if (TimeStop.timeStopped)
        {
            ps.Pause();
            return;
        }
        else
        {
            if (!ps.isPlaying)
            {
                ps.Play();

            }
        }

    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
    
    private void OnParticleCollision(GameObject collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().TakeDamage(damageAmount);
        }
    }

}
