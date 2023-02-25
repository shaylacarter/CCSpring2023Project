using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    public Color loadToColor = Color.black;

    public void LoadScene(string levelName) {
        Initiate.Fade(levelName,loadToColor,0.5f);
    }
}
