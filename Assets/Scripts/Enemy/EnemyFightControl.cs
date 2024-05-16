using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightControl : MonoBehaviour
{
    public EnemyController controller;
    public Enemy enemy;

    public Transform opponentPos;
    public Animator enemyAnim;
    public Animator weapon;

    private IEnumerator createCroutine;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponentInParent<EnemyController>();
        enemy = this.GetComponentInParent<Enemy>();
        createCroutine = AnimUpdate(0.15f);
        StartCoroutine(createCroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(opponentPos != null) {
            if(Vector3.Distance(transform.position ,opponentPos.position) <= 2.5f) {
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
            if(Vector3.Distance(transform.position, opponentPos.position) > 2.5f) {
                transform.LookAt(opponentPos.position);
                controller.FightPosition(opponentPos.position);
                weapon.enabled = false;
                weapon.GetComponent<EnemyWeapon>().attackActive = false;
            }
            else if(Vector3.Distance(transform.position, opponentPos.position) <= 2.5f) {
                transform.LookAt(opponentPos.position);
                weapon.enabled = true;
            }
        }
        
        
    }
    public void InvokeRepeatingMoveCharacter() {
        InvokeRepeating("ChangeMoveCharacter", 0.25f, 0.25f);
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
            }
        }
        
        if(other.tag == "Enemy") {
            EnemyController enmControl = other.GetComponent<EnemyController>();
            // || enmControl.GetComponentInChildren<EnemyFightControl>().opponentPos == this.GetComponentInParent<Enemy>().gameObject.transform
            if(!enmControl.isFight && opponentPos == null) {
                controller.isFight = true;
                enmControl.isFight = true;
                opponentPos = enmControl.transform;
                transform.LookAt(opponentPos.position);
                enmControl.GetComponentInChildren<EnemyFightControl>().opponentPos = enemy.gameObject.transform;
                enmControl.GetComponentInChildren<EnemyFightControl>().InvokeRepeatingMoveCharacter();
                InvokeRepeatingMoveCharacter();
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
            
            
        }
        if(other.tag == "Enemy" && opponentPos != null && opponentPos.gameObject.tag == "Enemy") {
            EnemyController enmControl = other.GetComponent<EnemyController>();
            opponentPos = null;
            enmControl.isFight = false;
            controller.isFight = false;
            enemy.GetComponent<EnemyController>().agent.isStopped = false;
            CancelInvoke("ChangeMoveCharacter");
            enmControl.ChangePosition();
        }
    }



    private IEnumerator AnimUpdate(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if(Vector3.Distance(Character.hero.position, this.transform.position) >= 30) {
            enemyAnim.enabled = false;
        }
        else {
            enemyAnim.enabled = true;
        }
        createCroutine = AnimUpdate(waitTime);
        StartCoroutine(createCroutine);
    }
}
