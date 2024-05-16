using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private Transform character;

    private float zoomValue = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = new Vector3(-character.position.x * zoomValue, -character.position.z * zoomValue, 0);
    }

    public void MapZoomPlus() {
        if(zoomValue >= 1) {
            zoomValue++;
            this.transform.localScale = Vector3.one * zoomValue;
        }
        else if(zoomValue < 1){
            zoomValue += 0.25f;
            this.transform.localScale = Vector3.one * zoomValue;
        }
    }
    public void MapZoomDec() {
        if(zoomValue > 1) {
            zoomValue--;
            this.transform.localScale = Vector3.one * zoomValue;
        }
        else if(zoomValue > 0.5f){
            zoomValue -= 0.25f;
            this.transform.localScale = Vector3.one * zoomValue;
        }
    }
}
