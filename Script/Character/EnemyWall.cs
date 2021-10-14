using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    private float moveDir = 1f;
    public float moveSpeed;
    private Vector3 enemyScale;

    private void Start()
    {
        enemyScale = transform.localScale;
    }
    void Update()
    {
        transform.localScale = enemyScale;
        EnemyMove();
    }
    private void EnemyMove()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, 0, 0);

        if (Mathf.Abs(transform.position.x) >= 2.4f)
        {
            transform.position = new Vector2(2.4f * Mathf.Sign(transform.position.x), transform.position.y);
            moveDir = -moveDir;
            enemyScale.x = gameObject.transform.localScale.x * -1f;
        }
        
    }
    
}
