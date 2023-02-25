using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnBeatClickDetection : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    public float lastBeatTime;
    public float lastPlayerClickTime;
    public float nextBeatTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //If the user left-clicked
        if (Input.GetMouseButtonDown(0)) {
            
            //Get the time that the player clicked.
            lastPlayerClickTime = Time.time;
            
            //First, check if the player clicked within the range of the last beat.
            if ((lastPlayerClickTime - lastBeatTime < 0.15) || (nextBeatTime - lastPlayerClickTime < 0.15)) {
                text.SetText("Click: On Beat");
            } else {
                text.SetText("Click: Off Beat");
            }
        }

    }

    public void OnBeat() {
        float currentTime = Time.time;
        nextBeatTime = currentTime + (currentTime - lastBeatTime);
        lastBeatTime = currentTime;
    }
}
