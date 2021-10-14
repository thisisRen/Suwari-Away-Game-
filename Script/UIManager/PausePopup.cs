using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    public GameObject fadeScene;
    public Animator fadeSceneAnim;
    public Image musicButton, soundButton;
    public Sprite[] spriteUI; //0-onM 1-offM
    private void Start()
    {
        SetUI();
    }
    public void Music()
    {
        AudioManager.Instance.PlayEffect("Click");
        if (PlayerPrefs.GetInt("MUSIC") == 1)
        {
            PlayerPrefs.SetInt("MUSIC", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MUSIC", 1);
        }
        AudioManager.Instance.SetMusic();
        SetUI();
    }
    public void Sound()
    {
        AudioManager.Instance.PlayEffect("Click");
        if (PlayerPrefs.GetInt("EFFECT") == 1)
        {
            PlayerPrefs.SetInt("EFFECT", 0);
        }
        else
        {
            PlayerPrefs.SetInt("EFFECT", 1);
        }
        AudioManager.Instance.SetEffect();
        SetUI();
    }
    public void Replay()
    {
        AudioManager.Instance.PlayEffect("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void LevelScene()
    {
        AudioManager.Instance.PlayEffect("Click");
        Time.timeScale = 1f;
        ShowUILevel.Instance.ShowLevel();
        SceneManager.LoadScene(1);
    }
    private void SetUI()
    {
        if (PlayerPrefs.GetInt("MUSIC") == 0)
        {
            musicButton.sprite = spriteUI[0];
        }
        else
        {
            musicButton.sprite = spriteUI[1];
        }

        if (PlayerPrefs.GetInt("EFFECT") == 0)
        {
            soundButton.sprite = spriteUI[2];
        }
        else
        {
            soundButton.sprite = spriteUI[3];
        }
    }
}
