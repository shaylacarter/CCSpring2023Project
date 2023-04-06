using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{

    private bool fadeOut;
    public float fadeSpeed;
    public GameObject doorCollider;
    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut) {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;

            if (objectColor.a <= 0) {
                fadeOut = false;
            }
        }
        
    }

    public void Remove() {
        fadeOut = true;
        doorCollider.SetActive(false);
        particles.SetActive(false);
    }

    public void AttemptToStopAudio() {
        if (fadeOut) {
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null) {
                audioSource.volume = 0.0f;
            }
        }
    }
}
