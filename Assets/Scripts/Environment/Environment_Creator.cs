using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Creator : MonoBehaviour
{
    
    private IEnumerator createCroutine;
    public bool isFinish;

    public int countX;
    public int countY = 0;
    // Start is called before the first frame update
    void Start() {
        createCroutine = CreateObject(0.15f);
        StartCoroutine(createCroutine);
    }

    private IEnumerator CreateObject(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if(countY < 18) {
            Instantiate(Resources.Load<GameObject>("Environment/Env_" + countY.ToString()), Vector3.zero, Quaternion.identity, transform);

            countY++;
            
            createCroutine = CreateObject(waitTime);
            StartCoroutine(createCroutine); }
        else {
            isFinish = true;
        }
    }
}
