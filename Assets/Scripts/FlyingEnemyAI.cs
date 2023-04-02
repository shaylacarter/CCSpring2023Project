using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAI : MonoBehaviour
{
    public float speed;
    public float sightRange;
    public float fireRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject enemyBullet;
    public GameObject firePoint;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector2.Distance(player.position, this.transform.position);
        if (playerDist < sightRange && playerDist > fireRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (playerDist <= fireRange && nextFireTime < Time.time)
        {
            Instantiate(enemyBullet, firePoint.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnDrawGismozSelected() 
    {
        if (this.transform == null) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.DrawWireSphere(this.transform.position, fireRange);
    }
}
