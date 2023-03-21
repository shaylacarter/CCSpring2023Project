using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    public GameObject itemCanvas;
    public bool playerInRange;
    public bool cooldownActive;

    // Start is called before the first frame update
    void Start()
    {
        cooldownActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cooldownActive && Input.GetKey(KeyCode.E) && playerInRange)
        {
            StartCoroutine(InteractionCooldown());

            if (!itemCanvas.activeInHierarchy) {
                itemCanvas.SetActive(true);
                ActivateAll();
            } else {
                ClosePopup();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player left range");
            playerInRange = false;
            ClosePopup();
        }
    }

    private void ClosePopup() {
        itemCanvas.GetComponent<FadeText>().FadeOut();
    }

    public void ActivateAll() {
        Transform[] children = itemCanvas.GetComponentsInChildren<Transform>(true);
        foreach(Transform child in children) {
            child.gameObject.SetActive(true);
        }
        itemCanvas.GetComponent<FadeText>().FadeIn();

    }

    IEnumerator InteractionCooldown(){ 
        cooldownActive = true;
        yield return new WaitForSeconds(0.2f); 
        cooldownActive = false;
    }
}
