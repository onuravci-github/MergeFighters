using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject holdPlayer;
    public Animator animEnemy;

    public bool isFight = false;
    public bool holdMove = false;
    void Start() {

        Vector3 temp = new Vector3(Random.Range(DangerZone.areaLimit[0], DangerZone.areaLimit[1]), 0.5f, Random.Range(DangerZone.areaLimit[2], DangerZone.areaLimit[3]));
        Debug.Log("Temp = " + temp);

        agent.SetDestination(temp);
        Invoke("ChangePosition", 1f);
    }

    public void ChangePosition() {
        if(!isFight || holdMove) {
            agent.isStopped = false;
            Vector3 temp = new Vector3(Random.Range(DangerZone.areaLimit[0], DangerZone.areaLimit[1]), 0.5f, Random.Range(DangerZone.areaLimit[2], DangerZone.areaLimit[3]));
            Debug.Log("Temp = " + temp);
            agent.SetDestination(temp);
            animEnemy.SetBool("Run", true);
            Invoke("ChangePosition", 5f);
        }
        
    }
    public void CancelDestination() {
        CancelInvoke("ChangePosition");
        agent.isStopped = true;
        animEnemy.SetBool("Run", false);
    }
    public void HoldEscape() {
        animEnemy.SetBool("Hold", true);
        //this.GetComponent<EnemyController>().is
        Invoke("Escape", 3f);
    }
    public void Escape() {
        holdPlayer.GetComponent<CharacterHolding>().HoldBreak(this.gameObject);
        animEnemy.SetBool("Hold", false);
        ChangePosition();
    }

    public void FightPosition(Vector3 position) {
        CancelInvoke("ChangePosition");

        agent.isStopped = false;
        //Vector3 temp = new Vector3(Random.Range(DangerZone.areaLimit[0], DangerZone.areaLimit[1]), 0.5f, Random.Range(DangerZone.areaLimit[2], DangerZone.areaLimit[3]));
        Debug.Log("Temp = " + position);
        agent.SetDestination(position);
    }
    
}
