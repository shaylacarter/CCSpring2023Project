using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : MonoBehaviour
{

    public float walkingSpeed = 2f;
    private Rigidbody2D enemyRigidBody;
    public GameObject edgeChecker;
    public LayerMask ground;
    public bool isFacingRight;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyRigidBody.velocity = transform.right * walkingSpeed;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(edgeChecker.transform.position, 0.2f, ground);
        if (!isGrounded && isFacingRight)
        {
            flipCharacter();
            enemyRigidBody.velocity = -enemyRigidBody.velocity;
        }
        else if (!isGrounded && !isFacingRight) 
        {
            flipCharacter();
            enemyRigidBody.velocity = -enemyRigidBody.velocity;
        }
    }

    void flipCharacter() {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0f, 180, 0f));
    }

    void OnDrawGizmosSelected() {
        if (edgeChecker == null) { return; }
        Gizmos.DrawWireSphere(edgeChecker.transform.position, 0.2f);
    }
}
