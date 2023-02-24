using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 8f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;

    //Used for ground checks when jumping.
    private float heightTestPlayer;
    private float widthTestPlayer;
    private Collider2D playerCollider;
    private int layerMaskGround;
    private int jumpCount = 0;

    //Used for wall sliding.
    public bool wallSliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<Collider2D>();
        heightTestPlayer = playerCollider.bounds.extents.y + 0.05f;
        widthTestPlayer = playerCollider.bounds.extents.x + 0.75f;
        layerMaskGround = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (movement > 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(-0.7f, 0.7f, 1);
        } else if (movement < 0f) {
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
            transform.localScale = new Vector3(0.7f, 0.7f, 1);
        } else {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        wallSliding = IsWalled();
        if(Input.GetButtonDown("Jump") && (IsGrounded() || wallSliding || jumpCount <= 1)) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        } else if (wallSliding) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -1.5f, float.MaxValue));
        }

        // if(Input.GetButtonDown("Jump") && (IsGrounded() || jumpCount <= 1)) {
        //     rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        // }

    }

    private bool IsGrounded()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, heightTestPlayer, layerMaskGround);
        bool isGrounded = groundHit.collider != null;
        //Debug.DrawRay(playerCollider.bounds.center, Vector2.down * heightTestPlayer, isGrounded ? Color.green : Color.red, 0.5f);

        //This allows for double-jumping.
        if (!isGrounded) {
            jumpCount++;
        } else {
            jumpCount = 0;
        }
        //Debug.Log($"Jump count: {jumpCount}, groundHit: {groundHit.collider != null}");
        return isGrounded;
    }

        private bool IsWalled()
    {
        RaycastHit2D wallHitLeft = Physics2D.Raycast(playerCollider.bounds.center, Vector2.left, widthTestPlayer, layerMaskGround);
        RaycastHit2D wallHitRight = Physics2D.Raycast(playerCollider.bounds.center, Vector2.right, widthTestPlayer, layerMaskGround);
        bool isWalled = wallHitLeft.collider != null || wallHitRight.collider != null;

        //We don't incremement jump count here because isGrounded already increments.
        if (isWalled) {
            jumpCount = 0;
        }
        //Debug.Log($"Jump count: {jumpCount}, wallHitLeft: {wallHitLeft.collider != null}, wallHitRight: {wallHitRight.collider != null}");
        return isWalled;
    }
    
}
