using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UISH : MonoBehaviour
{
    [SerializeField] private RectTransform ui;
    public float time;
    private Vector3 vectorShow = new Vector3(1, 1, 1);
    private Vector3 vectorHide = new Vector3(0, 0, 0);
    void Start()
    {
        Show();
    }

    private void Show()
    {
        ui.DOScale(vectorShow, time).OnComplete(() =>
        {
            ui.DOKill();
            ui.localScale = this.ui.localScale;
        });
        
    }

    /*private void Hide()
    {
        ui.DOScale(vectorHide, time).OnComplete(() =>
        {
            ui.DOKill();
        });
    }*/
}
