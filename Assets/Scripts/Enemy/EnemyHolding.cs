using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolding : MonoBehaviour
{
    public EnemyController enemyController;
    public BoxCollider colliderBox;
    public bool isActive;
    public int speed;

    public bool holdFinish;

    public float waitTime = 0;

    // Start is called before the first frame update
    void Start() {
        enemyController = this.GetComponentInParent<EnemyController>();
        speed = this.GetComponentInParent<Enemy>().speed;
    }

    public void HoldCharacter() {
        if(Time.time >= waitTime + 7 && this.GetComponentInParent<Character>() == null) {
            waitTime = Time.time;
            colliderBox.enabled = true;
            this.GetComponentInParent<Animator>().SetBool("Prison", true);
            this.GetComponentInParent<Animator>().SetBool("Run", false);
            //Invoke("CancelAnim", 2f);
        }
        

    }
    public void CancelAnim() {
        if(isActive) {
            isActive = false;
            this.GetComponentInParent<Animator>().SetBool("Prison", false);
            this.GetComponentInParent<Animator>().SetBool("Run", true);
            colliderBox.enabled = false;
            if(enemyController.GetComponentInChildren<EscapeButton>() != null) {
                enemyController.GetComponentInChildren<EscapeButton>().Reset();
                BreakHold(enemyController.GetComponentInChildren<Character>().gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !isActive  && this.GetComponentInParent<Character>() == null) {
            isActive = true;
            other.GetComponent<Character>().isHold = true;
            other.gameObject.transform.parent = enemyController.gameObject.transform;
            other.GetComponentInChildren<Animator>().SetBool("Prison", true);
            CancelInvoke("CancelAnim");
            Invoke("CancelAnim", 2f);
            this.GetComponentInParent<EnemyController>().holdMove = true;
            this.GetComponentInParent<EnemyController>().ChangePosition();
        }
        /*if(other.tag == "Enemy" && !isActive && other.gameObject != enemyController.gameObject) {
            isActive = true;Debug.Log("5");
            other.gameObject.transform.parent = enemyController.gameObject.transform;
        }*/
    }
    public void BreakHold(GameObject other){
        colliderBox.enabled = false;
        isActive = false;
        other.gameObject.transform.parent = null;
        other.GetComponent<CharacterControl>().SlowDown();
        this.GetComponentInParent<EnemyController>().holdMove = false;
    }
    
    public void HoldBreak(GameObject other) {
        colliderBox.enabled = false;
        isActive = false;
        this.GetComponentInParent<Animator>().SetBool("Prison", false);
        this.GetComponentInParent<Animator>().SetBool("Run", true);
        colliderBox.enabled = false;
        other.gameObject.transform.parent = null;
        other.GetComponent<CharacterControl>().SlowDown();
        this.GetComponentInParent<EnemyController>().holdMove = false;
    }
}
