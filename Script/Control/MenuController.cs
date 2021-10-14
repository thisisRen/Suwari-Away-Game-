using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject nameGame, character;

    public void Setting()
    {
        AudioManager.Instance.PlayEffect("Click");
        mainMenu.SetActive(false);
        nameGame.SetActive(false);
        character.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void Home()
    {
        AudioManager.Instance.PlayEffect("Click");
        settingMenu.SetActive(false);
        mainMenu.SetActive(true);
        nameGame.SetActive(true);
        character.SetActive(true);
        
    }
    public void TurnMusic()
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
        MainMenu.Instance.MusicButton();
        AudioManager.Instance.SetMusic();
    }

    public void TurnEffect()
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
        MainMenu.Instance.EffectButton();
        AudioManager.Instance.SetEffect();
    }
}
