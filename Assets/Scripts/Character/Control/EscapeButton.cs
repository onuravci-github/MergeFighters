using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EscapeButton : JoyButton
{
    private Character character;
    public Animator anim;
    private int count = 0;

    // Start is called before the first frame update
    void Start() {
        character = GetComponentInParent<Character>();
        anim.speed = 1 / waitTime;
    }

    // Update is called once per frame
    void Update() {
        if(character.gameObject.transform.parent != null) {
            this.transform.localScale = Vector3.one;
        }
        else {
            this.transform.localScale = Vector3.zero;
            count = 0;
            anim.GetComponent<Image>().fillAmount = 1;
        }
    }
    public override void OnPointerDown(PointerEventData eventData) {
        pressed = false;
        if(oldTime != 0) {
            if(Time.time >= oldTime + waitTime) {
                oldTime = Time.time;
                count++;
                anim.GetComponent<Image>().fillAmount = (3 - count) * 0.33f;
                if(count == 3) {
                    var temp = character.GetComponentInParent<Enemy>().GetComponentInChildren<Animator>();
                    temp.SetBool("Run", true);
                    temp.SetBool("Prison", false);
                    character.GetComponentInChildren<Animator>().SetBool("Prison", false);
                    temp.GetComponentInChildren<EnemyHolding>().HoldBreak(character.gameObject);
                    character.isHold = false;
                    count = 0;
                }
            }
        }
        else {
            oldTime = Time.time;
            count++;
            anim.GetComponent<Image>().fillAmount = (3 - count) * 0.33f;
        }





    }
    public override void OnPointerUp(PointerEventData eventData) {
        pressed = true;
    }

    public void Reset() {
        character.GetComponentInChildren<Animator>().SetBool("Prison", false);
        anim.GetComponent<Image>().fillAmount = 0;
        character.isHold = false;
        count = 0;
    }
}
