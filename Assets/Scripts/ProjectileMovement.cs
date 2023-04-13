using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 100f;
    public Rigidbody2D body;
    public GameObject bullet;
    public PlayerController player;
    public float bulletRadius = 0.3f; //Range of bullet hitbox.
    public LayerMask enemyLayers; //Put enemies all in a layer or layers so we can detect them in that layer and smack em.
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("PlayerCharacter");
        player = obj.GetComponent<PlayerController>();
        if (player.isFacingLeft)
        {
            body.velocity = transform.right * -speed;
        }
        else
        {
            body.velocity = transform.right * speed;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.transform.position, bulletRadius, enemyLayers);
        if (hitEnemies != null && hitEnemies.Length != 0) {
            Destroy(bullet);
        }
        //dmg them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (this.transform == null) { return; }
        Gizmos.DrawWireSphere(this.transform.position, bulletRadius);
    }
}
