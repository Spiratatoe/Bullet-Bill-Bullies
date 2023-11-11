using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hp, maxHP = 3;

    //movement like platforms
    [SerializeField] private float speed=2;
    [SerializeField] private Transform[] points; //array of transform points where the platform travels to
    private int i;//index  of points array

    //Animation Variables
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    private bool mDying = false;
    private bool mWalking = true;


    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        mWalking = true;
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
                StartCoroutine(StandStill());
            }
        }
        if (mWalking && !TimeStop.timeStopped && !mDying)
        {
            float movingTo = (transform.position.x - points[i].position.x);
            mSpriteRenderer.flipX = (movingTo < 0)? false: true;
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }

        mAnimator.SetBool("isWalking", mWalking);
        mAnimator.SetBool("isTimeStopped", TimeStop.timeStopped);

    }
    private IEnumerator StandStill()
    {
        mWalking = false;
        yield return new WaitForSeconds(2f);
        mWalking = true;
    }


    public void TakeDamage(int damage)
    {
        hp -= damage;
        mAnimator.SetBool("isTakingDamage", true);
        StartCoroutine(TookDamage());
    }

    private IEnumerator TookDamage()
    {
        
        if (hp > 0) //check if hase died
        {
            yield return new WaitForSeconds(1f);
            mAnimator.SetBool("isTakingDamage", false);
        }
        else{ 
            mAnimator.SetBool("isDying", true);
            mDying = true;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        } 
    }

    //do damage to the player if you touch them
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            PlayerControls playerComponent = collision.gameObject.GetComponent<PlayerControls>();
            playerComponent.TakeDamage((int)1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(points[0].position, 0.2f);
        Gizmos.DrawWireSphere(points[1].position, 0.2f);
        Gizmos.DrawLine(points[0].position, points[1].position);
    }
}
