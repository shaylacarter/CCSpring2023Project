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

    //Used for playing music as the player attacks.
    [SerializeField] private AudioClip[] _soundEffects;
    [SerializeField] private AudioSource _audioSource;
    private int _currentSoundEffect;
    public float volume = 1.0f;

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
        anim.SetTrigger("Attack");
        //detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(MeleePoint.position, MeleeRange, enemyLayers);
        //dmg them
        foreach (Collider2D enemy in hitEnemies) {
            Debug.Log("Hit " + enemy.name);
            GameObject damageEffect = Instantiate(hitParticle, MeleePoint.position, Quaternion.identity) as GameObject;
        }

        //Now play the audio effect!
        if (hitEnemies.Length > 0 && _soundEffects.Length > 0) {
            _audioSource.PlayOneShot(_soundEffects[_currentSoundEffect], volume);
            _currentSoundEffect = (_currentSoundEffect + 1) % _soundEffects.Length;
        } else if (_soundEffects.Length > 0) {
            StartCoroutine(WhiffEffect());
        }

    }

    void OnDrawGizmosSelected() {
        if (MeleePoint == null) { return; }
        Gizmos.DrawWireSphere(MeleePoint.position, MeleeRange);
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
