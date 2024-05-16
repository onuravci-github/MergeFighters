using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProcess : MonoBehaviour
{
    public Image image;
    public Collect_Object_Creator creatorCollect;
    public Environment_Creator environment_Creator;
    public TimeCount timeCount;
    public DangerZone dangerZone;
    public GameObject sceneMessage;
    // Start is called before the first frame update
    void Start()
    {
        fillImage = CreateObject(Time.deltaTime);
        StartCoroutine(fillImage);
    }

    private IEnumerator fillImage;
    
    private IEnumerator CreateObject(float waitTime) {
        yield return new WaitForSeconds(Time.deltaTime);

        if(image.fillAmount < 0.4f) {
            image.fillAmount += 0.02f;
            fillImage = CreateObject(Time.deltaTime);
            StartCoroutine(fillImage);
        }
        else if(image.fillAmount < 1 && environment_Creator != null && !environment_Creator.isFinish) {
            fillImage = CreateObject(Time.deltaTime);
            StartCoroutine(fillImage);
        }
        else if(image.fillAmount < 1 && creatorCollect != null && !creatorCollect.isFinish) {
            fillImage = CreateObject(Time.deltaTime);
            StartCoroutine(fillImage);
        }
        else if(image.fillAmount < 1 ) {
            image.fillAmount += 0.02f;
            fillImage = CreateObject(Time.deltaTime);
            StartCoroutine(fillImage);
        }
        else {
            if(timeCount != null) {
                timeCount.TimeCountStart();
            }
            if(dangerZone != null) {
                dangerZone.DangerZoneStart();
            }
            //Time.timeScale = 4f;
            this.gameObject.SetActive(false);
            if(sceneMessage != null) {
                sceneMessage.SetActive(true);
            }
        }
    }

}
