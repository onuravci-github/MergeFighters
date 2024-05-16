using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwordButton : JoyButton
{
    private Weapon weapon; 
    
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        weapon = FindObjectOfType<Weapon>();
        anim.speed = 1 / waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPointerDown(PointerEventData eventData) {
        if(oldTime != 0) {
            if(Time.time >= oldTime + waitTime) {
                oldTime = Time.time;
                weapon.Attack();
                anim.Play("WaitButton", -1, 0f);
            }
        }
        else {
            oldTime = Time.time;
            weapon.Attack();
            anim.enabled = true;
            anim.Play("WaitButton", -1, 0f);
        }
        pressed = false;
    }
    public override void OnPointerUp(PointerEventData eventData) {
        pressed = true;
    }
}
