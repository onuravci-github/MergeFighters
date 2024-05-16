using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeCount : MonoBehaviour
{
    private IEnumerator createCroutine;
    private float timeUpdate = 1f;
    public int timeCount = 15;
    public Text timeText;

    public GameObject splashScreen;

    private IEnumerator TimeDec(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if(timeCount > 0) {
            timeCount--;
            timeText.text = timeCount.ToString();
            createCroutine = TimeDec(timeUpdate);
            StartCoroutine(createCroutine);

        }
        else {
            if(splashScreen != null) {
                splashScreen.SetActive(true);
            }
            Invoke("NextScene", 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeText.text = timeCount.ToString();
        
    }

    public void TimeCountStart() {
        createCroutine = TimeDec(timeUpdate);
        StartCoroutine(createCroutine);
    }

    public void NextScene() {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            /*for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] != null) {
                    Items.itemsNumb[i] = Items.eguipItems[i].GetComponent<Merge_Images>().imageNumb;
                }
            }*/
            SceneControl.BattleRoyale_Scene();
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        //SceneManager.LoadScene();
    }
}
