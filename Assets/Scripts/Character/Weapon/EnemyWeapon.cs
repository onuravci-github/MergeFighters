using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWeapon : MonoBehaviour
{
    public int power;
    public bool attackActive = true;

    private BoxCollider colliderBox; 
    public GameObject textAttack;
    // Start is called before the first frame update
    void Start() {
        power = this.GetComponentInParent<Enemy>().power;
        colliderBox = this.GetComponent<BoxCollider>();
        
        textAttack = Resources.Load<GameObject>("Animation/Attack Text");

        colliderBox.enabled = false;
    }
    public void AttackActive() {
        attackActive = true;
        colliderBox.enabled = true;
    }
    // Update is called once per frame
    void Update() {

    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && attackActive) {
            attackActive = false;
            colliderBox.enabled = false;

            other.GetComponent<Character>().healt -= -other.GetComponent<Character>().armor + power;
            AttactText(-other.GetComponent<Character>().armor + power);
        }
        if(other.gameObject != this.GetComponentInParent<Enemy>().gameObject && other.tag == "Enemy" && attackActive) {
            attackActive = false;
            colliderBox.enabled = false;

            if(other.GetComponent<Enemy>().healt + other.GetComponent<Enemy>().armor - power < 0) {
                Enemy_Creator.KillEnemy(this.GetComponentInParent<Enemy>().enemyName,other.GetComponent<Enemy>().enemyName,0,0,0);
                this.GetComponentInParent<EnemyController>().isFight = false;
                this.GetComponentInParent<EnemyFightControl>().opponentPos = null;
            }
            other.GetComponent<Enemy>().healt -= -other.GetComponent<Enemy>().armor + power;
            AttactText(-other.GetComponent<Enemy>().armor + power);
        }
    }



    private void AttactText(int numb) {
        var text = Instantiate(textAttack, this.transform.position + Vector3.up * 3, Quaternion.identity);
        text.GetComponentInChildren<Text>().text = numb.ToString("F0");
        Destroy(text, 0.5f);
    }
}
