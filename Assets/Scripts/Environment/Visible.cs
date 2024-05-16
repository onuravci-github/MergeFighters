using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visible : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy") {
            if(other.gameObject.GetComponent<Enemy>().healthBar != null) {
                other.gameObject.GetComponent<Enemy>().healthBar.layer = 0;
            }
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = 0;
            if(other.gameObject.GetComponentInChildren<EnemyWeapon>()) {
                other.gameObject.GetComponentInChildren<EnemyWeapon>().gameObject.layer = 0;
                if(other.gameObject.GetComponentInChildren<ParticleSystem>()) {
                    other.gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = 0;
                }
            }
            
        }

    }
    void OnTriggerExit(Collider other) {
        
        if(other.gameObject.tag == "Enemy") {
            if(other.gameObject.GetComponent<Enemy>().healthBar != null) {
                other.gameObject.GetComponent<Enemy>().healthBar.layer = 8;
            }
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = 8;
            if(other.gameObject.GetComponentInChildren<EnemyWeapon>()) {
                other.gameObject.GetComponentInChildren<EnemyWeapon>().gameObject.layer = 8;
                other.gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = 8;
            }
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<BoxCollider>().enabled = true;
        }
    }
    void OnCollisionStay(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().healthBar.layer = 0;
            collision.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = 0;
            if(collision.gameObject.GetComponentInChildren<EnemyWeapon>()) {
                collision.gameObject.GetComponentInChildren<EnemyWeapon>().gameObject.layer = 0;
                if(collision.gameObject.GetComponentInChildren<ParticleSystem>()) {
                    collision.gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = 0;
                }
            }
            
        }
    }
}
