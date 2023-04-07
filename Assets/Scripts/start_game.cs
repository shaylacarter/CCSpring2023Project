using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_game : MonoBehaviour
{
    public string LevelName;
    public Color loadToColor = Color.black;
    public AudioSource audioSource;
    public float duration = 500;
    public float targetVolume = 0;

    void Start()
    {
        Debug.Log("Start called");
        audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    public void LoadLevel() {
        StartCoroutine(FadeAudioSource.StartFade(audioSource));
        Initiate.Fade(LevelName,loadToColor,0.5f);
        //SceneManager.LoadScene(LevelName);
    }
}
