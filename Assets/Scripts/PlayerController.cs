using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animate;

    public float speed = 8f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;

    //Used for ground checks when jumping/wall sliding.
    private float heightTestPlayer;
    private float widthTestPlayer;
    private Collider2D playerCollider;
    private int layerMaskGround;
    public int jumpCount = 0;
    public bool canJumpAgain;

    //Used for tracking power-ups.
    public bool canDoubleJump;
    public bool canWallJump;

    //Used for tracking the player's state.
    public bool inFreeFall = false;
    public bool grounded = false;
    public bool wallSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        heightTestPlayer = playerCollider.bounds.extents.y + 0.05f;
        widthTestPlayer = playerCollider.bounds.extents.x + 0.1f;
        layerMaskGround = LayerMask.GetMask("Ground");

        //Used for animations.
        animate = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Handle player movement an animations if they're currently moving.
        movement = Input.GetAxis("Horizontal");
        if (movement > 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
            animate.SetFloat("Speed", 1);
        } else if (movement < 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
            animate.SetFloat("Speed", 1);
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            animate.SetFloat("Speed", 0);
        }

        //Checks the current player state.
        wallSliding = canWallJump ? TouchingWall() : false;
        grounded = Grounded();
        jumpCount = (grounded || wallSliding) ? 0 : jumpCount;
        canJumpAgain = (jumpCount) < (canDoubleJump ? 1 : 0);

        //If the player pressed the key to jump
        //Check if they're either grounded, or wallsliding, or haven't used up all their jumps yet.
        if(Input.GetButtonDown("Jump") && (grounded || wallSliding || canJumpAgain)) {
            //If they're currently grounded or wallsliding, we want to reset their jump count.
            //If they're not either grounded or wallsliding, then we want to increment the number of jumps they've used by 1.
            jumpCount = (grounded || wallSliding) ? 0 : jumpCount + 1;

            //Now handle updating the player's velocity.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);

            //If they're jumping from a wall, then just assume we're in freefall.
            if (wallSliding) {
                animate.SetBool("InFreeFall", true);
            }
        } else if (wallSliding) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -1.5f, float.MaxValue));
        }

        //If the player is currently in free-fall, check if they've landed yet.
        if (inFreeFall && grounded) {
            inFreeFall = false;
            animate.SetBool("InFreeFall", false);
        } else if (!grounded && !inFreeFall) {
            inFreeFall = true;
            animate.SetBool("InFreeFall", true);
        }

    }

    private bool Grounded() {
        //Debug.DrawRay(playerCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);
        RaycastHit2D groundHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        return groundHit.collider != null;
    }

    private bool TouchingWall() {
        RaycastHit2D wallHitLeft = Physics2D.Raycast(playerCollider.bounds.center, Vector2.left, widthTestPlayer, layerMaskGround);
        RaycastHit2D wallHitRight = Physics2D.Raycast(playerCollider.bounds.center, Vector2.right, widthTestPlayer, layerMaskGround);
        return wallHitLeft.collider != null || wallHitRight.collider != null;
    }
    
}
