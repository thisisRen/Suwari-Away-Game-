using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EggControl : MonoBehaviour
{
    private Vector2 vectorGreen = new Vector2(1.75f, 3.63f);
    private Vector2 vectorGold = new Vector2(1.78f, 4.12f);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (gameObject.GetComponent<Collider2D>().tag == "Green Egg")
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<Transform>().DOMove(vectorGreen, 1f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    gameObject.GetComponent<Transform>().DOKill();
                });
            }

            else if (gameObject.GetComponent<Collider2D>().tag == "Gold Egg")
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
                gameObject.GetComponent<Transform>().DOMove(vectorGold, 1f).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    gameObject.GetComponent<Transform>().DOKill();
                });
            }
        }
        

    }
}
