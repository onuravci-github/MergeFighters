using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneMap : MonoBehaviour
{
    private Transform character;
   
    private float zoomValue = 1;
    private Vector3 startPos;

    public RectTransform dangerMapTransform;
    public Transform zoneTransform;
    public Transform resetPos;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        this.transform.position = startPos;
        dangerMapTransform.localPosition = new Vector2(zoneTransform.position.x,zoneTransform.position.z);  
        dangerMapTransform.sizeDelta = Vector2.one*zoneTransform.localScale.x*2f;
        //this.transform.localPosition = new Vector3(-character.position.x * zoomValue, -character.position.z * zoomValue, 0);
    }

    public void MapZoomPlus() {
        if(zoomValue >= 1) {
            zoomValue++;
            this.transform.localScale = Vector3.one * 1f/zoomValue;
        }
        else if(zoomValue < 1){
            zoomValue += 0.25f;
            this.transform.localScale = Vector3.one * 1f/zoomValue;
        }
    }
    public void MapZoomDec() {
        if(zoomValue > 1) {
            zoomValue--;
            this.transform.localScale = Vector3.one * 1f/zoomValue;
        }
        else if(zoomValue > 0.5f){
            zoomValue -= 0.25f;
            this.transform.localScale = Vector3.one * 1f/zoomValue;
        }
    }
}
