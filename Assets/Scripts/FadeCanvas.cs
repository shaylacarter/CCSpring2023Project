using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{

    public bool fadeIn;
    public bool fadeOut;
    public float fadeSpeed;
    public float alpha;
    public bool openDoor;

    public GameObject gameManager;
    public GameObject doorToOpen;

    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        SetAlpha(0.0f);
        fadeIn = true;
        openDoor = false;
        gameManager = GameObject.FindGameObjectWithTag("GameController").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn) {
            float fadeAmount = alpha + (fadeSpeed * Time.deltaTime);
            alpha = fadeAmount;
            SetAlpha(fadeAmount);

            if (alpha >= 1.0f) {
                fadeIn = false;
                gameManager.GetComponent<GameManager>().StartPlaying();
            }
        } else if (fadeOut) {
            float fadeAmount = alpha - (fadeSpeed * Time.deltaTime);
            alpha = fadeAmount;
            SetAlpha(fadeAmount);

            if (alpha <= 0.0f) {
                fadeOut = false;
                GetComponentInParent<MiniGameManager>().ToggleVisibility();
                doorToOpen.GetComponent<DoorControl>().Remove();
            }
        }
    }

    public void SetAlpha(float alpha) {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        foreach(SpriteRenderer child in children) {
            newColor = child.color;
            newColor.a = alpha;
            child.color = newColor;
        }
    }

    public void FadeOut(bool openDoor) {
        fadeOut = true;
        openDoor = true;
    }
}
