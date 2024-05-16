using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponentInChildren<CharacterControl>().visible.SetActive(true);
        }
        if(other.gameObject.tag == "Enemy") {
            if(other.gameObject.GetComponent<Enemy>().healthBar != null) {
                other.gameObject.GetComponent<Enemy>().healthBar.layer = 8;
            }
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = 8;
            if(other.gameObject.GetComponentInChildren<EnemyWeapon>()) {
                other.gameObject.GetComponentInChildren<EnemyWeapon>().gameObject.layer = 8;
                if(other.gameObject.GetComponentInChildren<ParticleSystem>()) {
                    other.gameObject.GetComponentInChildren<ParticleSystem>().gameObject.layer = 8;
                }
            }
        }

    }
    
    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponentInChildren<CharacterControl>().visible.SetActive(false);
        }
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
}
