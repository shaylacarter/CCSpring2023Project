using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EnemyDamageHandler : MonoBehaviour, IDamageable
{

    //The rigidbody of the enemy, so we can apply velocity changes.
    public Rigidbody2D rb;

    //The amount of health the enemy has initially.
    public float health = 100;

    //The particle of the enemy exploding into smoke.
    public GameObject deathParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Nothing to do here!
    }

    public void OnHit(int damage, Vector2 knockback) {
        //When they're hit, have them take damage.
        TakeDamage(damage);
        //Once we're done with that, update their velocity so they're "knocked back."
        if (rb != null) {
            rb.AddRelativeForce(knockback, ForceMode2D.Impulse);
        }
    }

    public void OnHit(int damage) {
        TakeDamage(damage);
    }

    public void OnObjectDestroyed() {

    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            health = 0;
            Instantiate(deathParticle, transform.position, Quaternion.identity);

            //Once we've killed the enemy, make their music quieter and add it to the level background track.
            AudioSource audioSource = GetComponentInChildren<AudioSource>();
            GameObject player = GameObject.FindWithTag("Player");
            if (audioSource != null && player != null) {
                audioSource.gameObject.transform.SetParent(player.transform);
                audioSource.volume = (audioSource.volume / 2);
            }

            Object.Destroy(this.gameObject);
        }
    }

    public void Heal(int healAmount) {
        health += healAmount;
        if (health > 100) {
            health = 100;
        }
    }

}
