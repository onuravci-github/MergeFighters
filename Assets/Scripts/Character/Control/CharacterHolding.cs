using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterHolding : MonoBehaviour
{
    public CharacterControl characterControl;
    public BoxCollider colliderBox;
    private bool isActive;
    public float speed;


    public GameObject controlObject;
    Vector3 position = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start() {
        characterControl = this.GetComponentInParent<CharacterControl>();
        speed = this.GetComponentInParent<Character>().speed;
    }

    // Update is called once per frame
    void Update() {
        if(position != Vector3.zero){
            controlObject.transform.localPosition = position;
        }
        if(characterControl.gameObject.transform.parent == null){
            this.GetComponentInParent<Animator>().SetBool("Prison", false);
        }
    }


    public void HoldCharacter() {
        colliderBox.enabled = true;
        this.GetComponentInParent<Animator>().SetBool("Hold", true);
        this.GetComponentInParent<Animator>().SetBool("Run", false);
        Invoke("CancelAnim", 0.5f);

    }
    public void CancelAnim() {
        if(!isActive) {
            this.GetComponentInParent<Animator>().SetBool("Hold", false);
            this.GetComponentInParent<Animator>().SetBool("Run", true);
            colliderBox.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && !isActive) {
            this.GetComponentInParent<CharacterController>().radius = 2;
            this.GetComponentInParent<CharacterController>().center = new Vector3(0,1.5f,0);
            isActive = true;
            other.gameObject.transform.parent = this.GetComponentInParent<AnimationFunctions>().gameObject.transform;
            controlObject = other.gameObject;
            position = other.gameObject.transform.localPosition;
            other.GetComponent<EnemyController>().holdPlayer = this.gameObject;
            other.GetComponent<EnemyController>().HoldEscape();
            other.GetComponentInChildren<EnemyCollectControl>().enemyAnim.SetBool("Prison",false);
            other.GetComponentInChildren<EnemyCollectControl>().MoveCharacterCancel();
            //this.GetComponentInParent<Character>().speed = Mathf.CeilToInt(speed * 0.5f);
            other.GetComponent<EnemyController>().CancelDestination();
            characterControl.GetComponentInChildren<HoldingButton>().HoldBreakImage();
            characterControl.GetComponentInChildren<HoldingButton>().oldTime = Time.time + 2.5f;
            var temp = Instantiate(characterControl.GetComponentInChildren<HoldingButton>().exclamation, other.transform.position + Vector3.up * 4.5f,Quaternion.identity , other.transform);
            temp.transform.localScale *= 0.5f; 
            Destroy(temp, 3f);
            //other.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    /*void OnTriggerExit(Collider other) {
        if(other.tag == "Enemy" && isActive) {
            isActive = false;
            other.gameObject.transform.parent = null;
            this.GetComponentInParent<Animator>().SetBool("Hold", false);
            this.GetComponentInParent<Character>().speed = Mathf.CeilToInt(speed * 2f);
        }
    }*/

    public void HoldBreak(GameObject other) {
        colliderBox.enabled = false;
        isActive = false;
        position = Vector3.zero;
        controlObject = null;
        other.GetComponentInChildren<EnemyCollectControl>().enemyHolding.waitTime = Time.time;
        other.gameObject.transform.parent = null;
        this.GetComponentInParent<CharacterController>().radius = 0.5f;
        this.GetComponentInParent<CharacterController>().center = new Vector3(0,0.75f,0);
        this.GetComponentInParent<CharacterControl>().isFight = false;
        
        other.GetComponentInChildren<EnemyCollectControl>().SlowDown();
        other.GetComponentInChildren<EnemyCollectControl>().opponentPos = null;
        other.GetComponent<EnemyController>().isFight = false;
        other.GetComponent<EnemyController>().ChangePosition();
        this.GetComponentInParent<Animator>().SetBool("Hold", false);
        this.GetComponentInParent<Animator>().SetBool("Run", true);
        //this.GetComponentInParent<Character>().speed = speed;
        characterControl.GetComponentInChildren<HoldingButton>().oldTime = Time.time + 5;
        characterControl.GetComponentInChildren<HoldingButton>().HoldingImage();
        var temp = Instantiate(characterControl.GetComponentInChildren<HoldingButton>().stars, other.transform.position + Vector3.up * 4.5f, Quaternion.identity, other.transform);
        temp.transform.localScale *= 0.5f;
        Destroy(temp, 3f);
        //other.GetComponent<BoxCollider>().isTrigger = false;
    }
    
}
