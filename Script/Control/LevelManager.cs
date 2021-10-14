using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    float dx = Screen.width;
    float dy = Screen.height;

    private void Awake()
    {
        Instance = this;
        Scaler();
        int t = PlayerPrefs.GetInt("LevelChoose");
    }
    private void Scaler()
    {
        float currentAp = (float)dx / (float)dy;
        float targetAp = (float)1080 / (float)1920;

        float scaleFactor = currentAp / targetAp;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
    }
}
