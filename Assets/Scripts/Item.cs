using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    public GameObject itemBox;
    public TMPro.TMP_Text itemText;
    public string dialog;
    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerInRange)
        {
            if (itemBox.activeInHierarchy)
            {
                itemBox.SetActive(false);
            }
            else
            {
                itemBox.SetActive(true);
                itemText.text = dialog;
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
            itemBox.SetActive(false);
        }
    }
}
