using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillEnemyPanel : MonoBehaviour
{
    public Text ch1;
    public Text ch2;

    public Image ch1_img;
    public Image ch2_img;

    public Image item;

    public Sprite[] characters;
    public Sprite[] items;
    

    public void PanelUpdate(string name1, string name2, int itemNumb, int ch_1, int ch_2) {
        ch1.text = name1;
        ch2.text = name2;
        ch1_img.sprite = characters[ch_1];
        ch2_img.sprite = characters[ch_2];
        item.sprite = items[itemNumb];
    }
}
