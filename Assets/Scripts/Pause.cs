 using System.Collections;
  using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

 
 public class Pause : MonoBehaviour {

    //Side notes: These could be useful if we wanted to remove the cursor while playing.
    //But we don't really need to worry about that right now.
    //Cursor.visible = false;
    //Screen.lockCursor = true;

    [SerializeField] GameObject pauseMenu;
    private AudioSource[] allAudioSources;
    private bool paused;

    public void Start() {
        OnResume();
    }

    public void Update() {
        if (Input.GetKeyDown("escape")) {
            if (paused) {
                OnResume();
            } else {
                OnPause();
            }
        }
    }

    public void OnPause() {
        paused = true;
        pauseMenu.SetActive(true);
        PauseAllAudio();
        Time.timeScale = 0f;
    }

    public void OnResume() {
        paused = false;
        pauseMenu.SetActive(false);
        ResumeAllAudio();
        Time.timeScale = 1f;
    }

    public void ExitToMainMenu() {
        StopAllAudio();
        Time.timeScale = 1f;
        //TODO: Here, we would use the special fade scene manager to go to the main menu.
    }

    void StopAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Stop();
        }
    }


    void PauseAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.Pause();
        }
    }

    void ResumeAllAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach( AudioSource audioS in allAudioSources) {
            audioS.UnPause();
        }
    }
}    