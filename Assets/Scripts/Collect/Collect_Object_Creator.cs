using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Object_Creator : MonoBehaviour
{
    public static List<GameObject> collect_Objects = new List<GameObject>();
    private IEnumerator createCroutine;
    private int timeCount = 0;
    public bool isFinish;

    public int countX;
    public int countY = 0;

    public int maxObject = 450;
    public int oneTimeLimit = 20;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++) {
            collect_Objects.Add(Resources.Load<GameObject>("Collect_Objects/Collect_Object_" + (i * 3).ToString()));
        }
        countX = Random.Range(oneTimeLimit, oneTimeLimit+5);
        createCroutine = CreateObject(Time.deltaTime);
        StartCoroutine(createCroutine);
    }

    private IEnumerator CreateObject(float waitTime) {
        yield return new WaitForSeconds(Time.deltaTime);

        if(timeCount < maxObject) {
            for(int i = 1; i < countX; i++) {
                Instantiate(collect_Objects[Random.Range(0, 4)], new Vector3(Random.Range(-200f + (400f / countX) * i, -180 + (400f / countX) * i), 1.25f, Random.Range(-200 + (200f / 10 * countY), -180 + (200f / 10 * countY))), Quaternion.identity, transform);
                timeCount++;
            }
            countY++;
            countX = Random.Range(oneTimeLimit, oneTimeLimit+5);
            
            //Instantiate(collect_Objects[Random.Range(0, 4)], new Vector3(Random.Range(-70f,70f),1, Random.Range(-70f, 70f)), Quaternion.identity, transform);
            createCroutine = CreateObject(Time.deltaTime);
            StartCoroutine(createCroutine);
        }
        else {
            isFinish = true;
        }
    }
    
    public static void Collect_Message(int value) {

    }

}
