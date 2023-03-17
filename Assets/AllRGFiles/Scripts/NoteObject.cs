using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public bool cleared;
    public bool fadeOut;
    public float fadeSpeed = 2;

    public KeyCode keyToPress;

    public GameObject hitEffect, goodEffect, missEffect, perfectEffect;
    // Start is called before the first frame update
    void Start()
    {
        fadeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress)){
            if(canBePressed){
                // gameObject.SetActive(false);
                fadeOut = true;

                if(Mathf.Abs(transform.position.y) > 0.25){
                    // Debug.Log("Normal");
                    GameManager.instance.NoteHit();
                    cleared = true;
                    //gameObject.SetActive(false);
                    //Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > 0.05f){
                    // Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    cleared = true;
                    //gameObject.SetActive(false);
                    //Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else{
                    // Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    cleared = true;
                    //gameObject.SetActive(false);
                    //Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }


            }
        }

        if (fadeOut) {
            Color objectColor = this.GetComponent<Renderer>().material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.GetComponent<Renderer>().material.color = objectColor;

            if (objectColor.a <= 0) {
                fadeOut = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Activator"){
            canBePressed = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Activator"){
            canBePressed = false;
            if(!cleared){
                GameManager.instance.NoteMissed();
                //Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }
}
