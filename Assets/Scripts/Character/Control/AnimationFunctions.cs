using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    public void BreakHand() {
        this.GetComponentInChildren<CharacterHolding>().colliderBox.enabled = false;
        //this.GetComponent<Animator>().SetBool("Hold", false);
    }

    public void BreakHandEnemy() {
        //this.GetComponentInChildren<EnemyHolding>().colliderBox.enabled = false;
        //this.GetComponent<Animator>().SetBool("Hold", false);
    }
}
