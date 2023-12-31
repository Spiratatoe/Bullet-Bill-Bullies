using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehaviour : MonoBehaviour
{
    //movement like platforms
    [SerializeField] private float speed = 2;
    [SerializeField] private Transform[] points; //array of transform points where the platform travels to
    private int i;  //index  of points array


    //Animation Variables
    private Animator mAnimator;
    private SpriteRenderer mSpriteRenderer;
    private bool mWalking = true;

    private float animatorSpeed;


    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
        mSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        mWalking = true;
        i = 0;
        animatorSpeed = mAnimator.speed;

    }

    // Update is called once per frame
    void Update()
    {
        //return if time is stopped
        if (TimeStop.timeStopped)
        {
            mAnimator.speed = 0;
            return;
        }
        else
        {
            mAnimator.speed = animatorSpeed;
        }

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
        if (mWalking && !mAnimator.GetBool("isDying"))
        {
          
            float movingTo = (transform.position.x - points[i].position.x);
            mSpriteRenderer.flipX = (movingTo < 0) ? false : true;
            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }

        mAnimator.SetBool("isWalking", mWalking);

    }

    public void playBite()
    {
        FindObjectOfType<AudioManager>().Play("dinobite");
    }

    public void playSword()
    {
        FindObjectOfType<AudioManager>().Play("knightsword");
    }

    public void playDeath()
    {
        FindObjectOfType<AudioManager>().Play("deathEnemy");
    }
    private IEnumerator StandStill()
    {
        mWalking = false;
        yield return new WaitForSeconds(2f);
        mWalking = true;
    }

    //do damage to the player if you touch them
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !TimeStop.timeStopped)
        {
            StartCoroutine(Attack(collision));
        }
    }
    
    IEnumerator Attack(Collider2D collision)
    {

        mAnimator.SetBool("isAttacking", true);
        PlayerControls playerComponent = collision.gameObject.GetComponent<PlayerControls>();
        playerComponent.TakeDamage((int)1);
        yield return new WaitForSeconds(0.3f);
        mAnimator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(points[0].position, 0.2f);
        Gizmos.DrawWireSphere(points[1].position, 0.2f);
        Gizmos.DrawLine(points[0].position, points[1].position);
    }
}
