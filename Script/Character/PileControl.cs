using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileControl : MonoBehaviour
{
    public Transform pile2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collision.transform.position = pile2.position;
        }   
    }
}
