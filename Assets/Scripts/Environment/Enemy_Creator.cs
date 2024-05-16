using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Creator : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private IEnumerator createCroutine;
    public Text aliveNumb;
    private static KillEnemyPanel killEnemyPanel;
    //private static GameObject killEnemyPanel;
    public GameObject enemy;

    public int timeCount;
    public int countY = 1;

    public GameObject winPanel;
    bool winPanelActive;
    // Start is called before the first frame update
    void Start() {
        createCroutine = CreateObject(Time.deltaTime);
        StartCoroutine(createCroutine);

        killEnemyPanel = Instantiate(Resources.Load<GameObject>("Animation/KillEnemyPanel"),transform.GetChild(0)).GetComponent<KillEnemyPanel>();
        killEnemyPanel.gameObject.SetActive(false);
    }

    private IEnumerator CreateObject(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        if(timeCount < 40) {
            for(int i = 1; i < 10; i++) {
                enemies.Add(Instantiate(enemy, new Vector3(Random.Range(50 * i - 250 , 50 * i - 230), 0.5f, Random.Range(80 * countY - 280, 80 * countY - 260)), Quaternion.identity, transform));
                enemies[timeCount].GetComponent<Enemy>().enemyName = "BOT " + timeCount;
                timeCount++;
            }
            countY++;
            
            createCroutine = CreateObject(Time.deltaTime);
            StartCoroutine(createCroutine);
        }
        else {
            winPanelActive = true;
        }
    }


    void Update() {
        int count = 0;
        for(int i = 0; i < enemies.Count; i++) {
            if(enemies[i] != null) {
                count++;
            }
        }
        aliveNumb.text = "Remaining " + count;
        if(count == 0 && winPanelActive) {
            winPanel.SetActive(true);
        }
    }

    public static void KillEnemy(string name1,string name2,int itemNumb,int ch_1,int ch_2) {
        killEnemyPanel.gameObject.SetActive(false);
        killEnemyPanel.gameObject.SetActive(true);
        killEnemyPanel.PanelUpdate(name1, name2, itemNumb, ch_1,ch_2);
    }
}
