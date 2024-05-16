using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollectControl : MonoBehaviour
{
    public EnemyController controller;
    public EnemyHolding enemyHolding;
    public Enemy enemy;

    public Transform opponentPos;
    public Animator enemyAnim;
    public GameObject holdObject;
    private Animator animator;

    private IEnumerator createCroutine;
    
    // Start is called before the first frame update
    void Start() {
        animator = this.GetComponent<Animator>();
        controller = this.GetComponentInParent<EnemyController>();
        enemy = this.GetComponentInParent<Enemy>();
        createCroutine = AnimUpdate(0.15f);
        StartCoroutine(createCroutine);
    }

    // Update is called once per frame
    void Update() {
        if(controller.GetComponentInChildren<Character>() != null){

        }
        else if(opponentPos != null) {
            if(Vector3.Distance(transform.position, opponentPos.position) <= 1.5f) {
                enemyAnim.SetBool("Run", false);
                controller.CancelDestination();
                transform.LookAt(opponentPos.position);
            }
            else {
                enemyAnim.SetBool("Run", true);
            }
        }
        else {
            if(controller.agent.isStopped) {
                CancelInvoke("ChangeMoveCharacter");
                controller.isFight = false;
                controller.ChangePosition();
            }
            enemyAnim.SetBool("Run", true);
        }
    }

    public void ChangeMoveCharacter() {
        if(opponentPos != null) {
            if(Vector3.Distance(transform.position, opponentPos.position) > 1.5f) {
                transform.LookAt(opponentPos.position);
                controller.FightPosition(opponentPos.position);
                holdObject.SetActive(false);
                enemyAnim.SetBool("Prison",false);
                //holdObject.GetComponent<EnemyWeapon>().attackActive = false;
            }
            else if(Vector3.Distance(transform.position, opponentPos.position) <= 1.5f) {
                transform.LookAt(opponentPos.position);
                holdObject.SetActive(true);
                holdObject.GetComponent<EnemyHolding>().HoldCharacter();
            }
        }


    }
    public void InvokeRepeatingMoveCharacter() {
        InvokeRepeating("ChangeMoveCharacter", 0.1f, 0.1f);
    }
    public void MoveCharacterCancel(){
        CancelInvoke("ChangeMoveCharacter");
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            CharacterControl chControl = other.GetComponent<CharacterControl>();
            if(!chControl.isFight && opponentPos == null) {
                chControl.isFight = true;
                opponentPos = chControl.transform;
                transform.LookAt(opponentPos.position);
                controller.isFight = true;
                InvokeRepeatingMoveCharacter();
                controller.FightPosition(opponentPos.position);
                this.GetComponent<SphereCollider>().radius = 9;
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && opponentPos != null && opponentPos.gameObject.tag == "Player") {
            CharacterControl chControl = other.GetComponent<CharacterControl>();
            opponentPos = null;
            chControl.isFight = false;
            controller.isFight = false;
            enemy.GetComponent<EnemyController>().agent.isStopped = false;
            CancelInvoke("ChangeMoveCharacter");
            controller.ChangePosition();
            
            this.GetComponent<SphereCollider>().radius = 6.5f;
        }
        /*if(other.tag == "Enemy" && opponentPos != null && opponentPos.gameObject.tag == "Enemy") {
            EnemyController enmControl = other.GetComponent<EnemyController>();
            opponentPos = null;
            enmControl.isFight = false;
            enmControl.ChangePosition();
            controller.isFight = false;
            CancelInvoke("ChangeMoveCharacter");
        }*/
    }

    public void SlowDown(){
        this.GetComponentInParent<NavMeshAgent>().speed *= 0.25f;
        Invoke("SlowUp", 4f);
    }
    public void SlowUp(){
        this.GetComponentInParent<NavMeshAgent>().speed *= 4f;
    }





    
    private IEnumerator AnimUpdate(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if(Vector3.Distance(Character.hero.position, this.transform.position) >= 30) {
            animator.enabled = false;
        }
        else {
            animator.enabled = true;
        }
        createCroutine = AnimUpdate(waitTime);
        StartCoroutine(createCroutine);
    }

}
