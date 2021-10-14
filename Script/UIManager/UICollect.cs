using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICollect : MonoBehaviour
{
    public static UICollect Instance;
    public GameObject collectPopup;
    public Text targetGold, targetGreen;
    public ParticleSystem collectParticle;
    public Text gold, green;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        int t = PlayerPrefs.GetInt("LevelChoose");
        targetGold.text = "/"+ PlayerPrefs.GetInt("LevelGoldEgg" + t.ToString()).ToString();
        targetGreen.text = "/" + PlayerPrefs.GetInt("LevelGreenEgg" + t.ToString()).ToString();
    }

    public void ShowWhenCollectGreen()
    {
        collectPopup.GetComponent<RectTransform>().DOAnchorPosX(-253, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            green.DOText(CharacterControl.Instance.greenEggs.ToString(), 0.2f, true, ScrambleMode.None, null);
            collectParticle.Play();
        });
    }
    public void ShowWhenCollectGold()
    {
        collectPopup.GetComponent<RectTransform>().DOAnchorPosX(-253, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            gold.DOText(CharacterControl.Instance.goldEggs.ToString(), 0.2f, true, ScrambleMode.None, null);
            collectParticle.Play();
            collectPopup.GetComponent<RectTransform>().DOKill();
        });
    }
    public IEnumerator HideCollectPopUp()
    {
        yield return new WaitForSeconds(2f);
        collectPopup.GetComponent<RectTransform>().DOAnchorPosX(500, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            collectPopup.GetComponent<RectTransform>().DOKill();
        });
    }
}
