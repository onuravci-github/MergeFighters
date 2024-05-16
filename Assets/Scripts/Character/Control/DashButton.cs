using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashButton : JoyButton
{
    private CharacterControl character;
    private GameObject dashParticle;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<CharacterControl>();
        dashParticle = Resources.Load<GameObject>("Animation/DashParticle");
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
                character.DashStart();
                anim.Play("WaitButton", -1, 0f);
                Instantiate(dashParticle, character.gameObject.transform.position,Quaternion.identity,character.transform);
            }
        }
        else {
            oldTime = Time.time;
            character.DashStart();
            anim.enabled = true;
            anim.Play("WaitButton", -1, 0f);
            Instantiate(dashParticle, character.gameObject.transform.position, Quaternion.identity, character.transform);
        }
        pressed = false;
            
    }
    public override void OnPointerUp(PointerEventData eventData) {
        pressed = true;
    }
}
