using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Transform MeleePoint; //Point for melee action.
    public float MeleeRange = 0.5f; //Range of melee attack.
    public LayerMask enemyLayers; //Put enemies all in a layer or layers so we can detect them in that layer and smack em.
    public Animator anim;
    public GameObject hitParticle;
    public GameObject whiffParticle;

    //Used for tracking a player's mana.
    public PlayerManaHandler playerManaHandler;

    //Used for calculating if the player is attacking on-beat or off-beat.
    public float lastBeatTime;
    public float lastPlayerClickTime;
    public float nextBeatTime;
    public AudioSource attackAudioSource;
    private bool delayBeat;

    public float knockBackScale;

    void Start() {
        playerManaHandler = GetComponent<PlayerManaHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            Melee();
        }
    }

    void Melee() {
        //play attack animation
        //https://www.youtube.com/watch?v=sPiVz1k-fEs


        anim.SetTrigger("Attack");  //We always want to trigger the base attack animation.


        //Now check if the player is attacking on or off the beat.
        bool onBeat = false;
        lastPlayerClickTime = Time.time;
        if ((lastPlayerClickTime - lastBeatTime < 0.15) || (nextBeatTime - lastPlayerClickTime < 0.15)) {
            Debug.Log("Click: On Beat");
            onBeat = true;
        } else {
            Debug.Log("Click: Off Beat");
        }


        //detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(MeleePoint.position, MeleeRange, enemyLayers);
        //dmg them
        foreach (Collider2D enemy in hitEnemies) {
            Debug.Log("Hit " + enemy.name);
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockBackScale;
            enemy.gameObject.GetComponent<IDamageable>().OnHit(5,knockback);
        }

        //Now do the on-attack effect!
        if (hitEnemies.Length > 0 && onBeat) {
            //Spawn the particle that shows we hit.
            Instantiate(hitParticle, MeleePoint.position, Quaternion.identity);

            //Have the player's song join in until the next beat.
            attackAudioSource.volume = 1.0f;
            //If the player clicked just barely before the next beat, then have delaybeat set to true.
            if (nextBeatTime - lastPlayerClickTime < 0.15) {
                delayBeat = true;
            }

            //Update the player's mana.
            playerManaHandler.AddMana(10);
        } else {
            StartCoroutine(WhiffEffect());
        }

    }

    void OnDrawGizmosSelected() {
        if (MeleePoint == null) { return; }
        Gizmos.DrawWireSphere(MeleePoint.position, MeleeRange);
    }

    public void OnBeat() {
        float currentTime = Time.time;
        nextBeatTime = currentTime + (currentTime - lastBeatTime);
        lastBeatTime = currentTime;
        if (delayBeat) {
            delayBeat = false;
        } else {
            attackAudioSource.volume = 0.0f;
        }
    }

    IEnumerator WhiffEffect(){ 
        
        GameObject attackEffect = (GameObject)GameObject.Instantiate(whiffParticle);
        attackEffect.transform.parent = transform;

        float attackOffset = (transform.localScale.x < 0) ? 0.4f : -0.4f;
        //float attackOffset = (transform.localScale.x < 0) ? 0.0f : -0.0f;
        attackEffect.transform.position = new Vector3(transform.position.x + attackOffset, transform.position.y, transform.position.z);

        attackEffect.transform.localScale = new Vector3(0.35f, 0.35f, 1);

        yield return new WaitForSeconds(0.2f); 
        Destroy(attackEffect); 
    }
}
