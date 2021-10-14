using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    public GameObject panel, pausePopup, fadeScene;
    public Animator fadeAnim;
    public GameObject complete, lose;

    [SerializeField] private RectTransform pauseText;
    [SerializeField] private RectTransform music;
    [SerializeField] private RectTransform sound;
    [SerializeField] private RectTransform replay;
    [SerializeField] private RectTransform level;

    private Vector3 vectorShow = new Vector3(1, 1, 1);
    private Vector3 vectorHide = new Vector3(0, 0, 0);
    private int t;
    private void Awake()
    {
        Instance = this;
        t = PlayerPrefs.GetInt("LevelChoose");
        SuwariAwayGame.GamePlay += EndGame;
    }
    private void Start()
    {
        StartCoroutine(StartGame());
    }
    private void Update()
    {
        SuwariAwayGame.GamePlay.Invoke();
    }
    private void OnDestroy()
    {
        SuwariAwayGame.GamePlay -= EndGame;
    }
    public void Show()
    {
        Sequence sq = DOTween.Sequence();

        sq.Append(pauseText.DOScale(vectorShow, 0.1f))
            .Append(music.DOScale(vectorShow, 0.1f))
            .Append(sound.DOScale(vectorShow, 0.1f))
            .Append(replay.DOScale(vectorShow, 0.1f))
            .Append(level.DOScale(vectorShow, 0.1f)).SetUpdate(true).OnComplete(() =>
            {
                sq.Kill();
            });
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlayEffect("Click");
        panel.SetActive(true);
        pausePopup.SetActive(true);
        Time.timeScale = 0f;
        Show();
    }
    public void ReturnGame()
    {
        AudioManager.Instance.PlayEffect("Click");
        Sequence sq = DOTween.Sequence();

        sq.Append(level.DOScale(vectorHide, 0.1f))
            .Append(replay.DOScale(vectorHide, 0.1f))
            .Append(sound.DOScale(vectorHide, 0.1f))
            .Append(music.DOScale(vectorHide, 0.1f))
            .Append(pauseText.DOScale(vectorHide, 0.1f)).SetUpdate(true).OnComplete(() =>
            {
                sq.Kill();
                pausePopup.SetActive(false);
                panel.SetActive(false);
                Time.timeScale = 1f;
                
            });
        
    }
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        fadeScene.SetActive(false);
    }

    public void EndGame()
    {
        StartCoroutine(SetEndGame());
    }
    private IEnumerator SetEndGame()
    {
        if(TimeManager.Instance.timeValue == 0)
        {
            if(CharacterControl.Instance.goldEggs == 0)
            {
                yield return new WaitForSeconds(1f);
                panel.SetActive(true);
                lose.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                panel.SetActive(true);
                complete.SetActive(true);
                
            }
        }
        else
        {
            if (CharacterControl.Instance.goldEggs == PlayerPrefs.GetInt("LevelGoldEgg" + t.ToString())
                && CharacterControl.Instance.greenEggs == PlayerPrefs.GetInt("LevelGreenEgg" + t.ToString()))
            {
                yield return new WaitForSeconds(1f);
                panel.SetActive(true);
                complete.SetActive(true);
                
            }
            else if (CharacterControl.Instance.isDestroy)
            {
                yield return new WaitForSeconds(2f);
                panel.SetActive(true);
                lose.SetActive(true);
            }
        }
    }
}
