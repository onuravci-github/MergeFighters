using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool[] isCreate = new bool[25];
    public Transform[] createPos;


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 25; i++) {
            if(!isCreate[i] && Merge.merge_Collected[i] >= 0) {
                var temp = Instantiate(Resources.Load<GameObject>("Merge_Images/Merge_Img_" + (Merge.merge_Collected[i]).ToString()), createPos[i].position, Quaternion.identity, transform);
                temp.transform.localScale = Vector3.one * 0.65f;
                isCreate[i] = true;
            }
        }
    }
}
