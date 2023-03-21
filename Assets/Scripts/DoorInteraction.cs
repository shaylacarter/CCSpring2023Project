using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public GameObject miniGameManager;
    public bool minigameStarted = false;
    public bool minigameLoaded = false;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!minigameStarted && Input.GetKey(KeyCode.E) && playerInRange)
        {
            Debug.Log("Minigame Triggered");
            minigameStarted = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void AttemptToStartGame() {
        if (minigameStarted && !minigameLoaded) {
            miniGameManager.GetComponent<MiniGameManager>().ToggleVisibility();
            minigameLoaded = true;
        }
    }

}
