using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    public bool visible = false;
    public GameObject minigameOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility() {
        if (visible) {
            minigameOverlay.SetActive(false);
        } else {
            minigameOverlay.SetActive(true);
        }

        visible = !visible;
    }
}
