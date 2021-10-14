using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CompletePopup : MonoBehaviour
{
    public static CompletePopup Instance;

    public GameObject fadeScene;
    public Animator fadeAnim;
    public GameObject panel;
    public Text time, goldEggText, greenEggText;
    [SerializeField] private RectTransform completeText;
    [SerializeField] private RectTransform clock;
    [SerializeField] private RectTransform goldEgg;
    [SerializeField] private RectTransform greenEgg;
    [SerializeField] private RectTransform homeButton;
    [SerializeField] private RectTransform nextButton;
    [SerializeField] private RectTransform faceButton;
    public DATALEVEL dataLevel;
    public ParticleSystem pa;

    private Vector3 vectorShow = new Vector3(1, 1, 1);

    private void Awake()
    {
        Instance = this;
        SetData();
    }
    private void Start()
    {
        SetUI();
        Show();
    }
    private void Show()
    {
        Time.timeScale = 0f;
        Sequence sq = DOTween.Sequence();

        sq.Append(completeText.DOScale(vectorShow, 0.5f))
            .Append(clock.DOScale(vectorShow, 0.5f))
            .Append(goldEgg.DOScale(vectorShow, 0.5f))
            .Append(greenEgg.DOScale(vectorShow, 0.5f)).SetUpdate(true).OnComplete(() =>
            {
                pa.Play();
                homeButton.DOScale(vectorShow, 1f).SetUpdate(true).OnComplete(() =>
                {
                    homeButton.DOKill();
                });
                if (PlayerPrefs.GetInt("LevelChoose") + 1 < dataLevel.listLevel.Count)
                {
                    nextButton.DOScale(vectorShow, 1f).SetUpdate(true).OnComplete(() =>
                    {
                        nextButton.DOKill();
                    });
                }
                else
                {
                    faceButton.gameObject.SetActive(true);
                    faceButton.DOScale(vectorShow, 1f).SetUpdate(true).OnComplete(() =>
                    {
                        faceButton.DOKill();
                    });
                }
                

                sq.Kill();
            });
    }
    private void SetUI()
    {
        goldEggText.text = "x " + CharacterControl.Instance.goldEggs.ToString();
        greenEggText.text = "x " + CharacterControl.Instance.greenEggs.ToString();
        time.text = TimeManager.Instance.DisplayTime(TimeManager.Instance.timeUse);


    }
    private void SetData()
    {
        if (TimeManager.Instance.timeUse <= PlayerPrefs.GetFloat("LevelTimeUse" + PlayerPrefs.GetInt("LevelChoose")) &&
            CharacterControl.Instance.greenEggs >= PlayerPrefs.GetInt("LevelGreenEggDone" + PlayerPrefs.GetInt("LevelChoose")) &&
            CharacterControl.Instance.goldEggs >= PlayerPrefs.GetInt("LevelGoldEggDone" + PlayerPrefs.GetInt("LevelChoose")))
        {
            PlayerPrefs.SetFloat("LevelTimeUse" + PlayerPrefs.GetInt("LevelChoose").ToString(), TimeManager.Instance.timeUse);
            PlayerPrefs.SetInt("LevelGreenEggDone" + PlayerPrefs.GetInt("LevelChoose").ToString(), CharacterControl.Instance.greenEggs);
            PlayerPrefs.SetInt("LevelGoldEggDone" + PlayerPrefs.GetInt("LevelChoose").ToString(), CharacterControl.Instance.goldEggs);
            PlayerPrefs.SetInt("LevelPass" + PlayerPrefs.GetInt("LevelChoose"), 1);
        }
        int t = PlayerPrefs.GetInt("MaxLevel");
        if (PlayerPrefs.GetInt("LevelChoose") + 1 == t)
        {
            PlayerPrefs.SetInt("MaxLevel", t + 1);
        }
    }
    public void Home()
    {
        AudioManager.Instance.PlayEffect("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        AudioManager.Instance.PlayEffect("Click");
        int t = PlayerPrefs.GetInt("LevelChoose") + 1;
        PlayerPrefs.SetInt("LevelChoose", t);
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }

}
