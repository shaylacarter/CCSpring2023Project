using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    public Color loadToColor = Color.black;
    public AudioSource audioSource;

    public void LoadScene(string levelName) {
        Time.timeScale = 1f;
        StartCoroutine(FadeAudioSource.StartFade(audioSource));
        Initiate.Fade(levelName,loadToColor,0.5f);
    }
}
