using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public Text timerText;
    public Image fillTime;
    [HideInInspector] public float timeValue;
    [HideInInspector] public float timeUse;

    private int t;
    private void Awake()
    {
        Instance = this;
        t = PlayerPrefs.GetInt("LevelChoose");
    }
    private void Start()
    {
        timeValue = PlayerPrefs.GetFloat("LevelTime" + t.ToString());
        timeUse = 0;
    }
    private void Update()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            timeUse += Time.deltaTime;

        }
        else
        {
            timeValue = 0;
            timeUse = PlayerPrefs.GetFloat("LevelTime" + t.ToString());
        }

        if (Time.timeScale < 1 && Time.timeScale > 0)
        {
            timeValue -= Time.deltaTime * 10;
            timeUse += Time.deltaTime * 10;
        }
        fillTime.fillAmount = Mathf.Lerp(fillTime.fillAmount, (float)timeValue / PlayerPrefs.GetFloat("LevelTime" + t.ToString()), 3f * Time.deltaTime);
        timerText.text = DisplayTime(timeValue);
    }
    public string DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return string.Format("{00:00} : {01:00}", minutes, seconds);
    }

}
