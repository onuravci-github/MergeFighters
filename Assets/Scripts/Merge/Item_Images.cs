using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Images : MonoBehaviour
{

    public GameObject interectObj;
    public BoxCollider2D interactCollider;
    public int itemAreaNumb;

    public void ItemOff() {
        interactCollider.enabled = false;
    }
    public void ItemOn() {
        interactCollider.enabled = false;
    }

    /*
    public GameObject interactObj;
    
    public bool isActive;

    private int[] controlItems = new int[4] {-1,-1,-1,-1};

    
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Mouse0) && isActive) {
            bool control = false;
            if(Items.eguipItems != null) {
                Debug.Log("L");
                for(int i = 0; i < Items.eguipItems.Length; i++) {
                    if(Items.eguipItems[i] != null) {
                        controlItems[i] = Items.eguipItems[i].GetComponent<Merge_Images>().imageNumb / 3;
                        if(controlItems[i] == interactObj.GetComponent<Merge_Images>().imageNumb / 3) {
                            control = true;
                            
                            Debug.Log("P");
                        }
                    }
                }
            }
            
            if(!control) {
                Debug.Log("M");
                Items.eguipItems[itemAreaNumb] = interactObj;
                interactObj.transform.position = new Vector2(transform.position.x, transform.position.y);
                interactObj.GetComponent<Merge_Images>().placeNumb = itemAreaNumb;
                //interactObj.GetComponent<Merge_Images>().isItemPlace = false;
                //GameObject.FindObjectOfType<Merge>().mouseUp = true;
            }
            else if(interactObj.GetComponent<Merge_Images>().isItemPlace && interactObj.GetComponent<Merge_Images>().posChangeObj == null && interactObj.GetComponent<Merge_Images>().interactObj1 == null && !control) {
                interactObj.transform.position = new Vector2(transform.position.x, transform.position.y);
                Debug.Log("S");
            }
            else {
                //GameObject.FindObjectOfType<Merge>().mouseUp = true;
            }
        }
        
        /*if(Items.eguipItems[itemAreaNumb] != null) {
            Items.eguipItems[itemAreaNumb].transform.position = this.transform.position;
        }*/
    //}

    /*void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Merge_Img") {
            if(!collision.GetComponent<Merge_Images>().isItemPlace) {
                Debug.Log("N");
                isActive = true;
                interactObj = collision.gameObject;
                collision.GetComponent<Merge_Images>().isItemPlace = true;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Merge_Img") {
            Debug.Log("O");
            isActive = false;
            interactObj = null;
            collision.GetComponent<Merge_Images>().isItemPlace = false;
        }
    }
    void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Merge_Img") {
            if(!collision.GetComponent<Merge_Images>().isItemPlace) {
                Debug.Log("N");
                isActive = true;
                interactObj = collision.gameObject;
                collision.GetComponent<Merge_Images>().isItemPlace = true;
            }
        }
    }*/
}