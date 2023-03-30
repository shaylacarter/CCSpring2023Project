using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerDamageHandler : MonoBehaviour, IDamageable
{

    public Rigidbody2D rb;
    PlayerController playerController;
    public GameObject hitParticle;

    //Handles player health and damage effects.
    public float health = 100;
    public HealthBarShrink healthBarScript;
    public SpriteRenderer spriteRenderer;
    Color originalColor;
    float Flashtime = .15f;
    public AudioMixerSnapshot normalAudio;
    public AudioMixerSnapshot damagedAudio;
    public AudioMixerSnapshot deathAudio;

    public bool dying;
    public GameObject deathParticle;
    public Vector3 deathPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        dying = false;
        deathAudio.TransitionTo(0.0f);
        normalAudio.TransitionTo(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (dying) {
            transform.position = deathPosition;
        }
    }

    public void OnHit(int damage, Vector2 knockback) {
        TakeDamage(damage);
        rb.AddRelativeForce(knockback, ForceMode2D.Impulse);
        GameObject damageEffect = Instantiate(hitParticle, transform.position, Quaternion.identity) as GameObject;
        damageEffect.transform.parent = transform;
    }

    public void OnHit(int damage) {
        TakeDamage(damage);
    }

    public void OnObjectDestroyed() {

    }

    void DamageFlashStart()
    {
        spriteRenderer.color = Color.red;
        Invoke("DamageFlashStop", Flashtime);
        
    }

    void DamageFlashStop()
    {
        spriteRenderer.color = originalColor;
    }

    public void TakeDamage(int damageAmount) {
        if (dying) {
            return;
        }
        health -= damageAmount;
        if (health <= 0) {
            health = 0;
            StopAllCoroutines();
            StartCoroutine(DeathAnimation());
            healthBarScript.Damage(damageAmount);
            return;
        }
        healthBarScript.Damage(damageAmount);
        StartCoroutine(DamageBuffer());
    }

    public void Heal(int healAmount) {
        health += healAmount;
        if (health > 100) {
            health = 100;
        }
        healthBarScript.Heal(healAmount);
    }

    IEnumerator DamageBuffer(){ 
        damagedAudio.TransitionTo(0.1f);
        DamageFlashStart();
        playerController.isBusy = true;
        yield return new WaitForSeconds(0.25f); 
        DamageFlashStart();
        yield return new WaitForSeconds(0.25f); 
        playerController.isBusy = false;
        normalAudio.TransitionTo(0.1f);
    }

    IEnumerator DeathAnimation(){ 
        Debug.Log("Kill player!");
        playerController.isBusy = true;
        dying = true;
        deathPosition = transform.position;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        deathAudio.TransitionTo(1.0f);
        Initiate.Fade(SceneManager.GetActiveScene().name,Color.black,1f);
    }

}
