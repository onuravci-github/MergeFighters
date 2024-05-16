using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    private bool isActive;
    private void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player" && !isActive){
            other.GetComponent<Character>().speed *= 0.5f;
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        
        if(other.tag == "Player" && isActive){
            other.GetComponent<Character>().speed *= 2f;
            isActive = false;
        }
    }
}
