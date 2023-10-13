using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{


    //jacks changes 

    public static PlayerControls instance; //prevent duplicates 

    // Variables set in the inspector
    [SerializeField] private float mWalkSpeed;
    [SerializeField] private float mRunSpeed;
    [SerializeField] private float mJumpForce;
    [SerializeField] private LayerMask mWhatIsGround;
    [SerializeField] private Transform mGroundCheck;

    private float kGroundCheckRadius = 0.1f;

    // Booleans used to coordinate with the animator's state machine
    private bool mRunning;
    private bool mMoving;
    private bool mGrounded;
    private bool mFalling;

    // References to Player's components
    private Animator mAnimator;
    private Rigidbody2D mRigidBody2D;
    private SpriteRenderer mSpriteRenderer;

    private void Start()
    {
        
        //prevents dups
        if(instance == null){//when the game starts 
            instance = this; 
        } else {
            Destroy(gameObject); 
        }

        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mSpriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateGrounded();
        MoveCharacter();

        // TODO: Update animator's variables

        mAnimator.SetBool("isMoving", mMoving);
        
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

        float horizontal = Input.GetAxis("Horizontal");
        mMoving = !Mathf.Approximately(horizontal, 0f);
        if (mMoving)
        {
            mRigidBody2D.velocity = new Vector2(horizontal* (mRunning ? mRunSpeed : mWalkSpeed), mRigidBody2D.velocity.y);
            FaceDirection(horizontal < 0f ? Vector2.left : Vector2.right);
        }

        // Jump is space
        if (mGrounded && Input.GetButtonDown("Jump"))
            mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, mJumpForce);
    }


    private void FaceDirection(Vector2 direction)
    {
        // Flip the sprite
        mSpriteRenderer.flipX = direction == Vector2.right ? false : true;
    }
}