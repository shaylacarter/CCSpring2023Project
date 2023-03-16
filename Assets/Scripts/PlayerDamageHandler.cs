using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDamageHandler : MonoBehaviour, IDamageable
{

    public Rigidbody2D rb;
    PlayerController playerController;

    //Handles player health and damage effects.
    public float health = 100;
    public HealthBarShrink healthBarScript;
    public SpriteRenderer spriteRenderer;
    Color originalColor;
    float Flashtime = .15f;
    public AudioMixerSnapshot normalAudio;
    public AudioMixerSnapshot damagedAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Nothing to do here.
    }

    public void OnHit(int damage, Vector2 knockback) {
        TakeDamage(damage);
        rb.AddRelativeForce(knockback, ForceMode2D.Impulse);
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
        health -= damageAmount;
        if (health < 0) {
            health = 0;
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
        playerController.isTakingDamage = true;
        yield return new WaitForSeconds(0.25f); 
        DamageFlashStart();
        yield return new WaitForSeconds(0.25f); 
        playerController.isTakingDamage = false;
        normalAudio.TransitionTo(0.1f);
    }

}
