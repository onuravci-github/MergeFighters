using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    public void Collect_Scene() {
        Merge.merge_Collected = new int[25] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        SceneManager.LoadScene(1);
    }

    public static void Merge_Scene() {
        SceneManager.LoadScene(2);
    }

    public static void BattleRoyale_Scene() {
        for(int i = 0; i < Items.eguipItems.Length; i++) {
            if(Items.eguipItems[i] != null) {
                Items.itemsNumb[i] = Items.eguipItems[i].GetComponent<Merge_Images>().imageNumb;
            }
        }
        SceneManager.LoadScene(3);
    }
    public void MenuScene() {
        Items.itemsNumb = new int[4] { -1,-1,-1,-1};
        Items.eguipItems = new GameObject[4];
        SceneManager.LoadScene(0);
    }

    
    public void Deneme(){
        SceneManager.LoadScene(4);
    }
}
