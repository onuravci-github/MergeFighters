using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerZone : MonoBehaviour
{
    // x1 x2 y1 y2
    public static float[] areaLimit = new float[] { -200, 200, -200, 200 };
    private int[] oldAreaLimit = new int[] { 0, 0, 0, 0 };

    private int count = 60;
    private int timeCount = 20;
    private int waitCount = 5;
    private int replayCount = 0;
    public Text timeText;

    public GameObject skull;
    public Transform dangerZone;

    public Vector3 finalPoint;

    // Start is called before the first frame update
    void Start() {
        finalPoint = new Vector3(Random.Range(-100, 100), 4, Random.Range(-100, 100));
        LimitUpdate();
        //dangerZone.
    }
    public void DangerZoneStart() {
        Invoke("DangerStart", 5f);
    }
    public void DangerStart() {
        TimeCroutine = TimeDec(1f);
        StartCoroutine(TimeCroutine);

        //dangerZone.position = finalPoint;
        ZoneCroutine = Zone(1f / (60 / 20));
        StartCoroutine(ZoneCroutine);
    }

    void LimitUpdate() {
        if(dangerZone != null) {
            areaLimit[0] = finalPoint.x - dangerZone.localScale.x / ((replayCount + 1) * 1.1f);
            areaLimit[1] = finalPoint.x + dangerZone.localScale.x / ((replayCount + 1) * 1.1f);
            areaLimit[2] = finalPoint.z - dangerZone.localScale.z / ((replayCount + 1) * 1.1f);
            areaLimit[3] = finalPoint.z + dangerZone.localScale.z / ((replayCount + 1) * 1.1f);
        }
        /*Debug.Log("0 = " + finalPoint);
        Debug.Log("1 = " + areaLimit[0]);
        Debug.Log("2 = " + areaLimit[1]);
        Debug.Log("3 = " + areaLimit[2]);
        Debug.Log("4 = " + areaLimit[3]);*/

    }
    //250 -250 250 -250    
    
    private IEnumerator TimeCroutine;
    private IEnumerator ZoneCroutine;
    private IEnumerator TimeDec(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        timeCount--;

        if(timeCount > 0) {
            skull.SetActive(true);
            //timeCount--;
            timeText.text = " ";
            TimeCroutine = TimeDec(waitTime);
            StartCoroutine(TimeCroutine);

        }
        else if(waitCount > 0) {
            timeText.text = waitCount.ToString();
            waitCount--;
            skull.SetActive(false);
            TimeCroutine = TimeDec(waitTime);
            StartCoroutine(TimeCroutine);
        }
        else {
            timeCount = 20;
            waitCount = 5;
            skull.SetActive(true);
            timeText.text = " ";
            //timeText.text = timeCount.ToString();
            TimeCroutine = TimeDec(waitTime);
            StartCoroutine(TimeCroutine);
        }
    }

    private IEnumerator Zone(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if(count > 0) {
            count--;
            dangerZone.position -= (dangerZone.position - finalPoint) / (60);
            dangerZone.localScale -= Vector3.one*(waitTime*2.75f);
            ZoneCroutine = Zone(waitTime);
            StartCoroutine(ZoneCroutine);

        }
        else {
            if(replayCount <= 3) {
                yield return new WaitForSeconds(4.66f);
                count = 60;
                finalPoint = finalPoint = new Vector3(Random.Range(finalPoint.x + 20, finalPoint.x - 20), 4, Random.Range(finalPoint.x + 20, finalPoint.x - 20));
                LimitUpdate();
                replayCount++;
                ZoneCroutine = Zone(waitTime);
                StartCoroutine(ZoneCroutine);
            }
            
        }
    }
}
