using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour, IDamageable
{

    public Rigidbody2D rb;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(int damage, Vector2 knockback) {
        playerController.TakeDamage(damage);
        rb.AddRelativeForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(int damage) {

    }

    public void OnObjectDestroyed() {

    }

}
