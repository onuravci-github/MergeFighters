using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCount : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        createCroutine = CreateMergeItems(0.05f);
        StartCoroutine(createCroutine);
    }

    private IEnumerator createCroutine;
    
    private IEnumerator CreateMergeItems(float waitTime) {
        yield return new WaitForSeconds(Time.deltaTime);

        text.text = "FPS : " + (1 / Time.unscaledDeltaTime).ToString("F0");
        createCroutine = CreateMergeItems(0.05f);
        StartCoroutine(createCroutine);
    }

}