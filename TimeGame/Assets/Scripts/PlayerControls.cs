using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // Variables set in the inspector
    [SerializeField] private float mWalkSpeed;
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] private LayerMask mWhatIsGround;
    [SerializeField] private Transform mGroundCheck;
    private float kGroundCheckRadius = 0.1f;
    [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;
    private Vector3 mVelocity = Vector3.zero; //target for smoothings

    // Booleans used to coordinate with the animator's state machine
    private bool mRunning;
    private bool mMoving;
    private bool mGrounded;
    private bool mFalling;

    // References to Player's components
    private Animator mAnimator;
    private Rigidbody2D rb;
    private SpriteRenderer mSpriteRenderer;
    private bool facingRight = true;
    private float horizontal = 0f;
    private bool justJumped = false;

    //Dashing variables
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float mDashForce;
    [SerializeField] private float mDashTime;
    [SerializeField] private float mDashCD;

    private void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mSpriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDashing){
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        mMoving = !Mathf.Approximately(horizontal, 0f);
        mAnimator.SetBool("isMoving", mMoving);
        
        UpdateGrounded();
        //JUMP
        if (mGrounded && Input.GetButtonDown("Jump")){
                justJumped = true;
        }
            
        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }
    
    private void FixedUpdate()
    {
        if (isDashing){
            return;
        }
        
        MoveCharacter();

        //jump
        if (justJumped){
            justJumped = false;
            rb.AddForce(new Vector2(rb.velocity.x, mJumpForce*10f));//rb.velocity = new Vector2(rb.velocity.x, mJumpForce);
        }
        
    }


    private bool UpdateGrounded()
    {
        mGrounded = Physics2D.OverlapCircle(mGroundCheck.position, kGroundCheckRadius, mWhatIsGround);
        return mGrounded;
    }

    private void MoveCharacter()
    {
        // Run is [Left Shift]
        mRunning = Input.GetButton("Run");

        if (mMoving){
            //No smoothing:
            //rb.velocity = new Vector2(horizontal* (mRunning ? mRunSpeed : mWalkSpeed) * 10f * Time.fixedDeltaTime, rb.velocity.y);

            //Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(horizontal* (mRunning ? mRunSpeed : mWalkSpeed) * 10f * Time.fixedDeltaTime, rb.velocity.y);
            //And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref mVelocity, mMovementSmoothing);

            FaceDirection(horizontal < 0f ? Vector2.left : Vector2.right);
        }
        
    }


    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite
        mSpriteRenderer.flipX = direction == Vector2.right ? false : true;
        facingRight = direction == Vector2.right ? true : false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        //TO PLAY WITH reset y speed
        // rb.velocity = new Vector2(rb.velocity.x, 0f);

        // rb.AddForce(new Vector2(mDashForce * (facingRight? 1f:-1f), 0f), ForceMode2D.Impulse);
        rb.velocity = new Vector2(mDashForce * (facingRight? 1f:-1f), 0f);
        
        //TODO anim
        yield return new WaitForSeconds(mDashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;   

        //TO PLAY WITH reset 
        rb.velocity = new Vector2(0f, 0f);

        yield return new WaitForSeconds(mDashCD);
        canDash = true;
    }

    
}