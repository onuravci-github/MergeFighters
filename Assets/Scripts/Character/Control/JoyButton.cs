using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public bool pressed;
    public float waitTime;
    public float oldTime = 0;
    public virtual void OnPointerDown(PointerEventData eventData) {
        pressed = false;
        Debug.Log("A");



    }
    public virtual void OnPointerUp(PointerEventData eventData) {
        pressed = true;
        Debug.Log("B");
    }
}
