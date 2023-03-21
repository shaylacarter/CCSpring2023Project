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

        if (hitEnemies.Length > 0 && _soundEffects.Length > 0) {
            _audioSource.PlayOneShot(_soundEffects[_currentSoundEffect], volume);
            _currentSoundEffect = (_currentSoundEffect + 1) % _soundEffects.Length;
        }
        //Now play the audio effect!

    }

    void OnDrawGizmosSelected() {
        if (MeleePoint == null) { return; }
        Gizmos.DrawWireSphere(MeleePoint.position, MeleeRange);
    }
}
