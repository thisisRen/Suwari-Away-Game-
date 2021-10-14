using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public static CharacterControl Instance;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    [HideInInspector] public static Vector3 charecterScale;
    public Animator characterAnim;
    //public Animator smokeAnim;
    public static bool canJump;
    public ParticleSystem destroy;
    public ParticleSystem colli;

    [HideInInspector] public float characterCurrent;
    [HideInInspector] public bool isDestroy;
    [HideInInspector] public int greenEggs;
    [HideInInspector] public int goldEggs;
    [HideInInspector] public Vector3 pos
    {
        get { return transform.position; }
    }
    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        charecterScale = transform.localScale;
        isDestroy = false;
        greenEggs = 0;
        goldEggs = 0;
    }
   
    private void Update()
    {
        transform.localScale = charecterScale;
        
    }
    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    public void ActivateRb()
    {
        rb.isKinematic = false;
    }
    public void DeactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDestroy)
        {
            colli.transform.position = gameObject.transform.position;
            colli.Play();
            if (collision.collider.tag == "Left Wall")
            {
                charecterScale.x = this.gameObject.transform.localScale.x;
            }
            else if (collision.collider.tag == "Right Wall")
            {
                charecterScale.x = -this.gameObject.transform.localScale.x;
            }
        }

        if (collision.collider.tag == "Object Destroy")
        {
            AudioManager.Instance.PlayEffect("Destroy");
            destroy.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
            destroy.Play();
            isDestroy = true;
       
           
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isDestroy)
        {
            if (collision.collider.tag == "Left Wall" || collision.collider.tag == "Right Wall" || collision.collider.tag == "Stick")
            {
                colli.transform.position = gameObject.transform.position;
                colli.Play();
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -0.1f, float.MaxValue));
                CharacterSlide();
            }
            if (collision.collider.tag == "Wall")
            {
                CharacterIdle();
            }

            if (collision.collider.tag == "Wall" || collision.collider.tag == "Left Wall" || collision.collider.tag == "Right Wall" )
            {
                canJump = true;
                characterCurrent = transform.position.y;
            }
        }
        
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isDestroy)
        {
            if (collision.collider.tag == "Wall" || collision.collider.tag == "Left Wall" || collision.collider.tag == "Right Wall" || collision.collider.tag == "Stick")
            {
                canJump = false;
            }
        }
        
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Green Egg")
        {
            AudioManager.Instance.PlayEffect("Collect");
            greenEggs += 1;
            UICollect.Instance.ShowWhenCollectGreen();
            StartCoroutine(UICollect.Instance.HideCollectPopUp());
        }
        else if (collision.tag == "Gold Egg")
        {
            AudioManager.Instance.PlayEffect("Collect");
            goldEggs += 1;
            UICollect.Instance.ShowWhenCollectGold();
            StartCoroutine(UICollect.Instance.HideCollectPopUp());
        }
    }
    public void CharacterSlide()
    {
        characterAnim.SetBool("isSlide", true);
        characterAnim.SetBool("Idle", false);
        characterAnim.SetBool("isJumpUp", false);
        characterAnim.SetBool("isJumpDown", false);
    }
    public void CharacterIdle()
    {
        characterAnim.SetBool("isSlide", false);
        characterAnim.SetBool("Idle", true);
        characterAnim.SetBool("isJumpUp", false);
        characterAnim.SetBool("isJumpDown", false);
  
    }
    public void CharacterJumpUp()
    {
        characterAnim.SetBool("isSlide", false);
        characterAnim.SetBool("Idle", false);
        characterAnim.SetBool("isJumpUp", true);
        characterAnim.SetBool("isJumpDown", false);
      
    }
    public void CharacterJumpDown()
    {
        characterAnim.SetBool("isSlide", false);
        characterAnim.SetBool("Idle", false);
        characterAnim.SetBool("isJumpUp", false);
        characterAnim.SetBool("isJumpDown", true);

    }
}
