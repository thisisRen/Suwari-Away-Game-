using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    [SerializeField] private GameObject character;
    [SerializeField] private RectTransform play;

    public GameObject fadeScene;
    public Animator fadeSceneAnim;

    [Header("Sky")] 
    public SpriteRenderer sky;
    public Sprite[] skys; //0-dark 1-light

    [Header("Leave")]
    public Image leave1, leave2;
    public Sprite[] leaves; //0-darkLeave1--- 1-darkLeave2 ---2-lightLeave1 --- 3-lightLeave2

    [Header("Tree")]
    public Image tree;
    public Sprite[] trees; //0-dark 1-light

    [Header("Mountain")]
    public Image mountain;
    public Sprite[] mountains; //0-dark 1-light

    [Header("Button")]
    public Image musicButton;
    public Image effectButton;

    public Sprite[] musicButtons; //0-on 1-off music 2 -on 3-off sound

    public Text textInPlayButton;

    public Text modeSky, modeNature;

    public float x1, x2;

    public float time1, time2;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(FadeShow());
        StartCoroutine(CharacterMove(x1,time1));
        character.GetComponent<Animator>().SetBool("doneJumpDown", false);
        if (PlayerPrefs.GetInt("SKY") == 0)
        {
            SkyLight();
        }
        else
        {
            SkyDark();
        }

        if (PlayerPrefs.GetInt("MODE") == 0)
        {
            ObjectLight();
            
        }
        else
        {
            ObjectDark();
    
        }
        MusicButton();
        EffectButton();
    }
    private void SkyLight()
    {
        sky.sprite = skys[1];
        tree.sprite = trees[1];
        modeSky.text = "Light";

        PlayerPrefs.SetInt("SKY", 0);
    }
    private void SkyDark()
    {
        sky.sprite = skys[0];
        tree.sprite = trees[0];
        modeSky.text = "Dawn";
        PlayerPrefs.SetInt("SKY", 1);
    }
    private void ObjectDark()
    {
        leave1.sprite = leaves[0];
        leave2.sprite = leaves[1];
        mountain.sprite = mountains[0];
        modeSky.color = Color.black;
        
        modeNature.text = "Dawn";
        modeNature.color = Color.black;

        PlayerPrefs.SetInt("MODE", 1);
    }
    private void ObjectLight()
    {
        leave1.sprite = leaves[2];
        leave2.sprite = leaves[3];
        mountain.sprite = mountains[1];
        modeSky.color = Color.white;
        modeNature.text = "Light";
        modeNature.color = Color.white;
        
        PlayerPrefs.SetInt("MODE", 0);
    }

    public void SetSky()
    {
        //dark -> light
        if (PlayerPrefs.GetInt("SKY") == 0)
        {
            SuwariAwayGame.ShowUI -= SkyLight;
            SuwariAwayGame.ShowUI += SkyDark;
            SuwariAwayGame.ShowUI.Invoke();
        }
        //light->dark
        else
        {
            SuwariAwayGame.ShowUI -= SkyDark;
            SuwariAwayGame.ShowUI += SkyLight;
            SuwariAwayGame.ShowUI.Invoke();
        }

    }
    public void SetNature()
    {
        //dark -> light
        if (PlayerPrefs.GetInt("MODE") == 1)
        {
            SuwariAwayGame.ShowUI -= ObjectDark;
            SuwariAwayGame.ShowUI += ObjectLight;
    
            SuwariAwayGame.ShowUI.Invoke();
        }
        //light->dark
        else
        {
            SuwariAwayGame.ShowUI -= ObjectLight;
            SuwariAwayGame.ShowUI += ObjectDark;
   
            SuwariAwayGame.ShowUI.Invoke();
        }
      
    }
    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("MUSIC") == 0)
        {
            musicButton.sprite = musicButtons[0];
        }
        else
        {
            musicButton.sprite = musicButtons[1];
        }
    }

    public void EffectButton()
    {
        if (PlayerPrefs.GetInt("EFFECT") == 0)
        {
            effectButton.sprite = musicButtons[2];
        }
        else
        {
            effectButton.sprite = musicButtons[3];
        }
    }

    private IEnumerator CharacterMove(float x, float time)
    {
        yield return new WaitForSeconds(time);
        character.GetComponent<Animator>().SetBool("jumpDown", true);
        character.GetComponent<Animator>().SetBool("jumpingDown", true);

        character.GetComponent<RectTransform>().DOAnchorPosY(x, 1f).OnComplete(() =>
        {
            character.GetComponent<Animator>().SetBool("jumpingDown", false);
            character.GetComponent<Animator>().SetBool("jumpDown", false);
            character.GetComponent<Animator>().SetBool("doneJumpDown", true);
            character.GetComponent<Animator>().DOKill();
        });
    }
    public void PlayButton()
    {
        AudioManager.Instance.PlayEffect("Click");
        Vector3 vectorHide = new Vector3(0, 0, 0);
        play.DOScale(vectorHide, 0.5f).OnComplete(() =>
        {
            play.DOKill();
            StartCoroutine(FadeHide());

        });
    }
    public void FacebookButton()
    {
        AudioManager.Instance.PlayEffect("Click");
    }
    private IEnumerator FadeShow()
    {
        yield return new WaitForSeconds(1f);
        fadeScene.SetActive(false);
    }
    private IEnumerator FadeHide()
    {
        fadeScene.SetActive(true);
        fadeSceneAnim.SetBool("changeScene", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
    

    
}
