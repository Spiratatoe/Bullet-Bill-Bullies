using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    // Variables set in the inspector
    [Header("Stats")]
    [SerializeField] private float mWalkSpeed;
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] static int hp, maxHP = 5;

    [Header("Game components")]
    [SerializeField] private LayerMask mWhatIsGround;
    [SerializeField] private Transform mGroundCheck;
    [SerializeField] private Transform forkPoint;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject deathPanel;

    private float kGroundCheckRadius = 0.1f;
    [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;
    private Vector3 mVelocity = Vector3.zero; //target for smoothings

    // Booleans used to coordinate with the animator's state machine
    private bool mRunning;
    private bool mMoving;
    private bool mGrounded;
    private bool mFalling;
    private bool mAttacking;
    private bool mTakingDamage;
    private bool mDead;

    // References to Player's components
    private Animator mAnimator;
    private Rigidbody2D rb;
    private SpriteRenderer mSpriteRenderer;
    private bool facingRight = true;
    private float horizontal = 0f;
    private bool justJumped = false;

    //Dashing variables
    [Header("Dashing Settings")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float mDashForce;
    [SerializeField] private float mDashTime;
    [SerializeField] private float mDashCD;

    private float yBoundary;
    private static bool firstLoad = true;

    private void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        
        //having this heals the player every new scene
        if (firstLoad)
        {
            hp = maxHP;
            firstLoad = false;
        }

        // UI initialization
        healthText.text = ""+ hp;
        deathPanel.SetActive(false);

        //find the lower bound of the map
        GameObject oob = GameObject.Find("OutOfBounds");
        if (oob != null)
        {
            yBoundary = oob.transform.position.y;
        }
        else
        {
            yBoundary = -10000;
        }
    }

    private void Update()
    {
        if (hp <= 0 && !mTakingDamage)
        {
            StartCoroutine(Die());
            mDead = true;
        }

        // freezer the player if dialogue is happening OR if dead
        if ((GameObject.Find("DialogueManager") != null && DialogueManager.GetInstance().playing) || mDead)
        {
            return;
        }

        //Take damage if you fall out of bounds
        if (transform.position.y <= yBoundary)
        {
            TakeDamage(100);
        }

        


        if (isDashing){
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        mMoving = !Mathf.Approximately(horizontal, 0f);
        

        if (mMoving)
        {
            gameObject.transform.SetParent(null);
        }


        UpdateGrounded();

        //JUMP
        if (mGrounded && Input.GetButtonDown("Jump")){
                justJumped = true;
                mAnimator.SetBool("isJumping", true);
        }
        
        // Run is [Left Shift]
        mRunning = Input.GetButton("Run");

        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isMoving", mMoving);

        if (!mTakingDamage && Input.GetButtonDown("Attack") && !mAttacking)
        {
            if (mAttacking && mTakingDamage) return;
            mAttacking = true;
            mAnimator.SetBool("isAttacking", mAttacking);
            StartCoroutine(Attack());
        }


        if (Input.GetButtonDown("Dash") && canDash && !mTakingDamage)
        {
            StartCoroutine(Dash());
        }   

    }
    
    private void FixedUpdate()
    {
        //freezer the player if dialogue is happening
        if (TryGetComponent<DialogueManager>(out DialogueManager manager))
        {
            if (DialogueManager.GetInstance().playing)
            {
                return;
            }
        }
        if (isDashing){
            return;
        }
        
        MoveCharacter();

    }


    private bool UpdateGrounded()
    {
        mGrounded = Physics2D.OverlapCircle(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        mAnimator.SetBool("isJumping", !mGrounded);
        return mGrounded;
    }

    public void playWalkSound()
    {
        FindObjectOfType<AudioManager>().Play("walk");
    }

    public void playAttackSound()
    {
        FindObjectOfType<AudioManager>().Play("attack");
    }
    private void MoveCharacter()
    {

        if (mMoving){
            
            //No smoothing:
            //rb.velocity = new Vector2(horizontal* (mRunning ? mRunSpeed : mWalkSpeed) * 10f * Time.fixedDeltaTime, rb.velocity.y)

            //Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(horizontal* (mRunning ? mRunSpeed : mWalkSpeed) * 10f * Time.fixedDeltaTime, rb.velocity.y);
            //And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref mVelocity, mMovementSmoothing);

            FaceDirection(horizontal < 0f ? Vector2.left : Vector2.right);
        }

        //jump
        if (justJumped)
        {
            justJumped = false;
            rb.AddForce(new Vector2(rb.velocity.x, mJumpForce * 11f));//rb.velocity = new Vector2(rb.velocity.x, mJumpForce);

        }

    }

    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite not needed  because flipping player
        //mSpriteRenderer.flipX = direction == Vector2.right ? false : true;

        facingRight = direction == Vector2.right ? true : false;

        Vector3 localScale = transform.localScale;

        //Flip the player
        if (!facingRight && localScale.x >= 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        if (facingRight && localScale.x <= 0)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    public void TakeDamage(int damage)
    {
        if (mAttacking || mTakingDamage || isDashing) return; //cant take damage while hitting or if just took or if dashing
        mTakingDamage = true;
        hp -= damage;
        if (hp <= 0) { hp = 0; }
        healthText.text = "" + hp;
        mAnimator.SetBool("isTakingDamage", mTakingDamage);

        StartCoroutine(TookDamage());
        if (hp <= 0)
        {
            Debug.Log("You would have died");
        }

    }

    public void AddMaxHP(int toAdd)
    {
        maxHP += toAdd;
        hp += toAdd;
        healthText.text = "" + hp;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        mAnimator.SetBool("isDashing", true);
        //TO PLAY WITH reset y speed
        // rb.velocity = new Vector2(rb.velocity.x, 0f);

        // rb.AddForce(new Vector2(mDashForce * (facingRight? 1f:-1f), 0f), ForceMode2D.Impulse);
        rb.velocity = new Vector2(mDashForce * (facingRight? 1f:-1f), 0f);
        
        //TODO anim
        yield return new WaitForSeconds(mDashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        mAnimator.SetBool("isDashing", false);
        //TO PLAY WITH reset 
        rb.velocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(mDashCD);
        canDash = true;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        mAttacking = false;
        mAnimator.SetBool("isAttacking", mAttacking);
    }
    private IEnumerator TookDamage()
    {
        yield return new WaitForSeconds(1f);
        mTakingDamage = false;
        mAnimator.SetBool("isTakingDamage", false);

    }

    private IEnumerator Die()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        mAnimator.SetBool("isDying", true);
        yield return new WaitForSeconds(1f);
        deathPanel.SetActive(true);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(forkPoint.position + new Vector3(-0.2f, 0.045f, 0f), new Vector3(0.59f,0.59f,0f));


    }
}