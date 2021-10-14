using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class GameControl : MonoBehaviour
{
    Camera cam;
    public CharacterControl character;
    public Trajectory trajectory;
    [SerializeField] float pushForce = 4f;
    bool isDragging = false;
    bool startJump = false;
    Vector2 startPoins;
    Vector2 endPoins;
    Vector2 direction;
    Vector2 force;
    float distance;
   

    public static int done = 0;

    private void Start()
    {
        cam = Camera.main;

        character = FindObjectOfType<CharacterControl>();

    }
    private void Update()
    {
        Play();

        if(!CharacterControl.Instance.isDestroy)
        {
            if (CharacterControl.Instance.characterCurrent < CharacterControl.Instance.transform.position.y)
            {
                CharacterControl.Instance.CharacterJumpUp();
            }
            else if (CharacterControl.Instance.characterCurrent > CharacterControl.Instance.transform.position.y)
            {
                CharacterControl.Instance.CharacterJumpDown();
            }
            else
            {
                CharacterControl.Instance.CharacterIdle();
            }
        }
    }
    public void Play()
    {
      
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI() && CharacterControl.canJump == true)
        {
            CharacterControl.Instance.characterCurrent = CharacterControl.Instance.transform.position.y;
            OnDragStart();
            isDragging = true;
            startJump = true;
        }
        if (Input.GetMouseButtonUp(0) && !IsMouseOverUI() && startJump == true)
        {
            OnDragEnd();
            startJump = false;
        }
        if (isDragging && !IsMouseOverUI())
        {
            CharacterControl.canJump = false;
            OnDrag();
        }
        
        
    }

    void OnDragStart() // an tay
    {
        Time.timeScale = 0.1f;
        character.DeactivateRb();
        startPoins = cam.ScreenToWorldPoint(Input.mousePosition);
        trajectory.Show();


    }
    void OnDrag() // GIU
    {
        if (!IsMouseOverUI() && Time.timeScale != 0)
        {
            endPoins = cam.ScreenToWorldPoint(Input.mousePosition);

            //left
            if (endPoins.x <= startPoins.x)
            {
                CharacterControl.charecterScale.x = 0.2f;
            }
            //right
            else
            {
                CharacterControl.charecterScale.x = -0.2f;
            }

            distance = Vector2.Distance(startPoins, endPoins);
            direction = (startPoins - endPoins).normalized;
            force = (direction * distance * pushForce) / 1.5f;

            Debug.DrawLine(startPoins, endPoins);

            trajectory.UpdateDots(character.pos, force);
         
        }

        
    }
    void OnDragEnd() // tha tay
    {
        AudioManager.Instance.PlayEffect("Jump");
        Time.timeScale = 1f;
        character.ActivateRb();
        character.Push(force);

        trajectory.Hide();
        isDragging = false;

    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject(0);
    }
}
