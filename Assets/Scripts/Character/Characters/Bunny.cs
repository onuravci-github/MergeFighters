using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : Character
{
    public Bunny() {
        healt = 100;
        armor = 5;
        speed = 150;
        power = 20;
        chName = "Mouse";
    }

    void OnEnable() {
        if(Items.eguipItems != null) {
            for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] == null && Items.itemsNumb[i] != -1) {
                    Items.eguipItems[i] = Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (Items.itemsNumb[i]).ToString()));
                    Merge_Images temp = Items.eguipItems[i].GetComponent<Merge_Images>();
                    healt += temp.healthUp;
                    armor += temp.armorUp;
                    speed += temp.speedUp;
                    power += temp.powerUp;
                }
            }
        }
        
    }

}
