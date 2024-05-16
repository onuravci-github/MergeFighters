using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Animator roof;
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            roof.SetBool("Visible",true);
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            roof.SetBool("Visible", false);
        }
    }
}
