using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon : MonoBehaviour
{
    public int power;
    public float attackSpeed = 1.0f;
    public bool attackActive = false;

    public GameObject textAttack;
    // Start is called before the first frame update
    void Start()
    {
        power = this.GetComponentInParent<Character>().power;
        textAttack = Resources.Load<GameObject>("Animation/Attack Text");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack() {
        if(!this.GetComponent<Animator>().GetBool("Attack")) {
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Animator>().SetBool("Attack", true);
            attackActive = true;
        }
        else {

        }
    }
    public void AttackActive() {
        this.GetComponent<Animator>().SetBool("Attack", false);
        attackActive = false;
    }


    void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && attackActive) {
            attackActive = false;
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy.healt + enemy.armor - power < 0) {
                Enemy_Creator.KillEnemy(this.GetComponentInParent<Character>().chName, other.GetComponent<Enemy>().enemyName, 0, 1, 0);
                this.GetComponentInParent<CharacterControl>().isFight = false;
            }
            enemy.healt -= -enemy.armor + power;
            
            AttactText(-enemy.armor + power);
        }
    }

    private void AttactText(int numb) {
        var text = Instantiate(textAttack, this.transform.position + Vector3.up * 3, Quaternion.identity);
        text.GetComponentInChildren<Text>().text = numb.ToString("F0");
        Destroy(text, 0.5f);
    }
}
