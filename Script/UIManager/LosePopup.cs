using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LosePopup : MonoBehaviour
{
    public static LosePopup Instance;


    [SerializeField] private RectTransform loseText;
    [SerializeField] private RectTransform homeButton;
    [SerializeField] private RectTransform replayButton;
   

    private Vector3 vectorShow = new Vector3(1, 1, 1);

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Show();
    }
    public void Show()
    {
        Time.timeScale = 0f;
        Sequence sq = DOTween.Sequence();

        sq.Append(loseText.DOScale(vectorShow, 0.5f)).SetUpdate(true).OnComplete(() =>
        {
                homeButton.DOScale(vectorShow, 0.7f).SetUpdate(true).OnComplete(() =>
                {
                    homeButton.DOKill();
                });

                replayButton.DOScale(vectorShow, 0.7f).SetUpdate(true).OnComplete(() =>
                {
                    replayButton.DOKill();
                });

                sq.Kill();
        });
    }
}
