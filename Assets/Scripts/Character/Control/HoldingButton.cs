using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoldingButton : JoyButton
{
    private Character character;
    private CharacterHolding holdEnemy;
    public Animator anim;

    public Image holdImage;
    public Sprite[] holdSprite;
    public GameObject exclamation;
    public GameObject stars;
    // Start is called before the first frame update
    void Start() {
        character = GetComponentInParent<Character>();
        holdEnemy = FindObjectOfType<CharacterHolding>();
        anim.speed = 1 / waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(character.gameObject.transform.parent != null) {
            this.transform.localScale = Vector3.zero;
        }
        else {
            this.transform.localScale = Vector3.one;
        }
    }
    public override void OnPointerDown(PointerEventData eventData) {
        pressed = false;
        if(oldTime != 0) {
            if(Time.time >= oldTime + waitTime) {
                oldTime = Time.time;
                holdEnemy.HoldCharacter();
                anim.Play("WaitButton", -1, 0f);
            }
            else {
                if(character.GetComponentInChildren<Enemy>() != null) {

                    character.GetComponentInChildren<EnemyController>().Escape();
                }
            }
        }
        else if(oldTime == 0) {
            oldTime = Time.time;
            holdEnemy.HoldCharacter();
            anim.enabled = true;
            anim.Play("WaitButton", -1, 0f);
        }
        
        
    }
    public override void OnPointerUp(PointerEventData eventData) {
        pressed = true;
    }

    public void HoldBreakImage() {
        holdImage.sprite = holdSprite[1];
    }
    public void HoldingImage() {
        holdImage.sprite = holdSprite[0];
    }
}
