using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelScene : MonoBehaviour
{
    public static UILevelScene Instance;
    public GameObject fadeScene;
    public Animator fadeSceneAnim;

    public GameObject background;
    public Sprite[] spriteBG;
    public DATALEVEL data;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        StartCoroutine(FadeShow());
        SetBackground();
        ShowUILevel.Instance.ShowLevel();
    }
    public void Home()
    {
        AudioManager.Instance.PlayEffect("Click");
        StartCoroutine(FadeHide());
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
        SceneManager.LoadScene(0);
    }
    private void SetBackground()
    {
        if (PlayerPrefs.GetInt("SKY") == 0 && PlayerPrefs.GetInt("MODE") == 0)
        {
            background.GetComponent<SpriteRenderer>().sprite = spriteBG[0];
        }
        else if (PlayerPrefs.GetInt("SKY") == 0 && PlayerPrefs.GetInt("MODE") == 1)
        {
            background.GetComponent<SpriteRenderer>().sprite = spriteBG[1];
        }
        else if (PlayerPrefs.GetInt("SKY") == 1 && PlayerPrefs.GetInt("MODE") == 0)
        {
            background.GetComponent<SpriteRenderer>().sprite = spriteBG[2];
        }
        else
        {
            background.GetComponent<SpriteRenderer>().sprite = spriteBG[3];
        }
    }
    
}
