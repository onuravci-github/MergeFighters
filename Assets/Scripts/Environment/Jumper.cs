using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private bool isActive;
    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player" && !isActive){
            other.GetComponent<CharacterControl>().JumpStart();
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        
        if(other.tag == "Player" && isActive){
            isActive = false;
        }
    }
}
