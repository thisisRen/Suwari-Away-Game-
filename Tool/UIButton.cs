
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    private Transform parent;
    private Vector3 localScaleOld;
    private void Start()
    {
        parent = this.transform.parent;
    }
    public UnityEvent ClickEvent = new UnityEvent();

    public void OnPointerUp(PointerEventData data)
    {
        transform.localScale = localScaleOld; // tha chuot thi thuc hien hanh dong
    }

    public void OnPointerDown(PointerEventData data)
    {
        localScaleOld = this.transform.localScale;
        transform.localScale = localScaleOld * 1.1f;    // click chuot vao IMAGE thi thuc hien hanh dong
    }

    public void OnPointerClick(PointerEventData data)
    {
        ClickEvent.Invoke();        // click chuot vao neu chuot van o vi tri IMAGE thi thuc hien hanh dong
    }

    public void SetUpEvent(UnityAction action)
    {
        ClickEvent.RemoveAllListeners();
        ClickEvent.AddListener(action);
    }

    public void ReturnParent()
    {
        this.transform.parent = parent;
    }
}
