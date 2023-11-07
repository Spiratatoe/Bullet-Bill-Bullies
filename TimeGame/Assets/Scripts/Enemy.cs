using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] int hp, maxHP = 3;


    //movement like platforms
    [SerializeField] private float speed=2;
    [SerializeField] private Transform[] points; //array of transform points where the platform travels to
    private int i;//index  of points array

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = maxHP;
        
        i = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //This movement is similar to the moving platform might need to change later on
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        if (!TimeStop.timeStopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    //do damage to the player if you touch them
	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            PlayerControls playerComponent = collision.gameObject.GetComponent<PlayerControls>();
            playerComponent.TakeDamage((int)1);
        }
    }
}
