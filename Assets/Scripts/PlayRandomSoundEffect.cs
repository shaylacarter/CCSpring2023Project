using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSoundEffect : MonoBehaviour
{

    public AudioClip[] soundEffects;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayRandomSound() {
        audioSource.clip = soundEffects[Random.Range(0, soundEffects.Length)];
        audioSource.Play();
    }
}
