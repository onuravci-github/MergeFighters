using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge_Images : MonoBehaviour
{
    public int healthUp;
    public int armorUp;
    public int speedUp;
    public int powerUp;

    public int imageNumb;
    public GameObject interactObj1;
    public GameObject posChangeObj;
    public GameObject stayObject;

    public GameObject itemObject;

    public GameObject isActiveObj;
    public bool isActive = false;
    public bool isItemPlace = false;
    public int placeNumb = -1;

    public BoxCollider2D colliderBox;

    void Start() {
        //collider = this.GetComponent<BoxCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(isActive) {
            if(collision.gameObject.tag == "Merge_Img") {
                if(interactObj1 == null) {
                    if(collision.gameObject.GetComponent<Merge_Images>().imageNumb == imageNumb && (imageNumb + 1) % 3 != 0) {
                        collision.gameObject.GetComponent<Merge_Images>().isActiveObj.SetActive(true);
                        interactObj1 = collision.gameObject;
                    }
                }
            }
            if(interactObj1 == null) {
                if(collision.gameObject.tag == "Merge_Img" && collision.gameObject.GetComponent<Merge_Images>().imageNumb != imageNumb) {
                   posChangeObj = collision.gameObject;
                }
                else if(collision.gameObject.tag == "Merge_Pos") {
                    if(posChangeObj != null) {
                        if(posChangeObj.tag != "Merge_Img") {
                            posChangeObj = collision.gameObject;
                        }
                        else {
                            
                        }
                    }
                    else {
                        posChangeObj = collision.gameObject;
                    }
                }
            }
            if(collision.gameObject.tag == "Item_Img") {
                itemObject = collision.gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if(isActive) {
            if(collision.gameObject.tag == "Merge_Img") {
                collision.gameObject.GetComponent<Merge_Images>().isActiveObj.SetActive(false);
                if(interactObj1 == collision.gameObject) {
                    interactObj1 = null;
                    BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
                    collider.enabled = false;
                    collider.enabled = true;
                }
            }
            if(collision.gameObject.tag == "Merge_Img" || collision.gameObject.tag == "Merge_Pos" && interactObj1 == null) {
                if(posChangeObj == collision.gameObject && stayObject != collision.gameObject) {
                    posChangeObj = null;
                }
                
            }

            if(collision.gameObject.tag == "Item_Img" && collision.gameObject == itemObject) {
                itemObject = null;
            }
        }
        else {
            if(collision.gameObject.tag == "Merge_Img") {
                collision.gameObject.GetComponent<Merge_Images>().isActiveObj.SetActive(false);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision) {
        if(isActive) {
            stayObject = collision.gameObject;
            if(posChangeObj == null) {
                posChangeObj = stayObject;
            }
        }
    }

    public int Object_Traits(int numb) {
        if(numb == 0) {
            return healthUp;
        }
        if(numb == 1) {
            return armorUp;
        }
        if(numb == 2) {
            return speedUp;
        }
        if(numb == 3) {
            return powerUp;
        }
        else {
            return 0;
        }
    }
}
