using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public int currentScore;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public static GameManager instance;

    public float totalNotes;
    public float notesActive;

    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float MissedHits;

    public GameObject minigameManager;
    public GameObject doorToOpen;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentMultiplier = 1;
        totalNotes = FindObjectsOfType<NoteObject>().Length;
        notesActive = totalNotes;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            startPlaying = true;
            theBS.hasStarted = true;
            theMusic.Play();
        } else if (notesActive == 0) {
            startPlaying = false;
            minigameManager.GetComponent<MiniGameManager>().ToggleVisibility();
            if (MissedHits == 0) {
                doorToOpen.GetComponent<DoorControl>().Remove();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
        if(currentMultiplier - 1 < multiplierThresholds.Length){
            multiplierTracker++;

            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker){
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        notesActive--;
    }

    public void NoteMissed(){
        Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;
        MissedHits++;
        notesActive--;
    }

    public void NormalHit(){
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit(){
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }


    public void PerfectHit(){
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

}


