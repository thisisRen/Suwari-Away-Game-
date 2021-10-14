using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public static LevelButton Instance;
    public Text levelText;
    public Image levelImage;
    public Sprite notPlay, canPlay;
    public Text time;
    public Text greenEgg;
    public Text goldEgg;
    public Image background;
    public DATALEVEL data;
    private Level level;
    private void Awake()
    {
        Instance = this;
    }
    public void SetData(Level _level)
    {
        level = _level;
        if (level.ID >= PlayerPrefs.GetInt("MaxLevel"))
        {
            levelText.text = "UNLOCK";
            levelImage.sprite = notPlay;
            time.gameObject.SetActive(false);
            greenEgg.gameObject.SetActive(false);
            goldEgg.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
        }

        else
        {
            levelText.text = "Level " + (level.ID + 1).ToString();
            levelImage.sprite = canPlay;
            time.gameObject.SetActive(true);
            greenEgg.gameObject.SetActive(true);
            goldEgg.gameObject.SetActive(true);


            if (PlayerPrefs.GetInt("LevelPass" + level.ID.ToString()) == 0)
            {
                time.text = DisplayTime(PlayerPrefs.GetFloat("LevelTime" + level.ID.ToString()));
                greenEgg.text = "0/" + PlayerPrefs.GetInt("LevelGreenEgg" + level.ID.ToString()).ToString();
                goldEgg.text = "0/" + PlayerPrefs.GetInt("LevelGoldEgg" + level.ID.ToString()).ToString();
                background.gameObject.SetActive(false);
                PlayerPrefs.SetFloat("LevelTimeUse" + level.ID.ToString(), level.second);
            }
            else
            {

                time.text = DisplayTime(PlayerPrefs.GetFloat("LevelTimeUse" + level.ID.ToString()));
                greenEgg.text = PlayerPrefs.GetInt("LevelGreenEggDone" + level.ID.ToString()).ToString() + "/" + PlayerPrefs.GetInt("LevelGreenEgg" + level.ID.ToString()).ToString();
                goldEgg.text = PlayerPrefs.GetInt("LevelGoldEggDone" + level.ID.ToString()).ToString() + "/" + PlayerPrefs.GetInt("LevelGoldEgg" + level.ID.ToString()).ToString();
                background.gameObject.SetActive(true);
                if (level.ID == 0 || level.ID % 4 == 0)
                {
                    background.sprite = UILevelScene.Instance.spriteBG[0];
                }
                else if (level.ID == 1 || level.ID % 5 == 0)
                {
                    background.sprite = UILevelScene.Instance.spriteBG[1];
                }
                else if (level.ID == 2 || level.ID % 6 == 0)
                {
                    background.sprite = UILevelScene.Instance.spriteBG[2];
                }
                else
                {
                    background.sprite = UILevelScene.Instance.spriteBG[3];
                }
            }
        }

        
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
    public void AvatarOnClicked()
    {
        AudioManager.Instance.PlayEffect("Click");
        if (level.ID < PlayerPrefs.GetInt("MaxLevel"))
        {
            PlayerPrefs.SetInt("LevelChoose", level.ID);
            SceneManager.LoadScene(2);
        }
    }
}
