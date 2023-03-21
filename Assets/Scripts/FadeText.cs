using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{

    public bool fadeIn;
    public bool fadeOut;
    public float fadeSpeed;
    public float alpha;


    // Start is called before the first frame update
    void Start()
    {
        alpha = 0.0f;
        SetAlpha(0.0f);
        fadeIn = true;
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
            }
        } else if (fadeOut) {
            float fadeAmount = alpha - (fadeSpeed * Time.deltaTime);
            alpha = fadeAmount;
            SetAlpha(fadeAmount);

            if (alpha <= 0.0f) {
                fadeOut = false;
                SetChildrenInactive();
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

    public void SetChildrenInactive() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void FadeOut() {
        fadeOut = true;
        fadeIn = false;
    }

    public void FadeIn() {
        fadeIn = true;
        fadeOut = false;
    }
}
