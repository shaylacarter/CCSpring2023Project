using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public GameObject bulletTarget;
    public float bulletSpeed;
    public GameObject bullet;
    public Rigidbody2D bulletBody;
    public float bulletRadius = 0.2f; //Range of bullet hitbox.
    public LayerMask playerLayer; //use player's layer so ranged enemy can deal damage.
    // Start is called before the first frame update
    void Start()
    {
        bulletBody = this.GetComponent<Rigidbody2D>();
        bulletTarget = GameObject.FindGameObjectWithTag("Player");
        Vector2 fireDirection = (bulletTarget.transform.position - this.transform.position).normalized * bulletSpeed;
        bulletBody.velocity = new Vector2(fireDirection.x, fireDirection.y);
        Destroy(this.gameObject, 2);
    }

    void OnBecameInvisible()
    {
        Destroy(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(this.transform.position, bulletRadius, playerLayer);
        if (hitPlayer != null && hitPlayer.Length != 0)
        {
            Destroy(bullet);
        }
        //dmg them
        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Hit " + player.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (this.transform == null) { return; }
        Gizmos.DrawWireSphere(this.transform.position, bulletRadius);
    }
}
