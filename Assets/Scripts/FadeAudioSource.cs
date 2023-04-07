using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class FadeAudioSource {
    public static IEnumerator StartFade(AudioSource audioSource)
    {

        float startVolume = audioSource.volume;
        float adjustedVolume = startVolume;
 
        while (adjustedVolume > 0) {
            adjustedVolume -= startVolume * Time.deltaTime / 2;
            audioSource.volume = adjustedVolume;
            Debug.Log (adjustedVolume);
            yield return null;
        }
 
        audioSource.Stop ();
        audioSource.volume = startVolume;
    }
}
