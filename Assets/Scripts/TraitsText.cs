using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitsText : MonoBehaviour
{
    private bool control;
    public int traitsNumb;
    private Text traitsText;
    public static Character character;

    private IEnumerator fillImage;
    // Start is called before the first frame update
    void Start() {
        if(character == null) {
            character = GameObject.FindObjectOfType<Character>();
        }
        traitsText = this.GetComponent<Text>();

        fillImage = UpdateText(0.15f);
        StartCoroutine(fillImage);
    }

    // Update is called once per frame
    void Update() {
        TraitsUpdate();
    }

    public void TraitsUpdate() {
        switch(traitsNumb) {
            case 0:
            traitsText.text = character.healt.ToString();
            break;
            case 1:
            traitsText.text = character.armor.ToString();
            break;
            case 2:
            traitsText.text = character.speed.ToString();
            break;
            case 3:
            traitsText.text = character.power.ToString();
            break;
            case 4:
            control = false;
            for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] != null) {
                    Merge_Images temp = Items.eguipItems[i].GetComponent<Merge_Images>();
                    if(temp.Object_Traits(0) > 0) {
                        traitsText.text = "+" + temp.Object_Traits(0).ToString();
                        control = true;
                    }
                }
            }
            if(!control) {
                traitsText.text = " ";
            }
            break;
            case 5:
            control = false;
            for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] != null) {
                    Merge_Images temp = Items.eguipItems[i].GetComponent<Merge_Images>();
                    if(temp.Object_Traits(1) > 0) {
                        traitsText.text = "+" + temp.Object_Traits(1).ToString();
                        control = true;
                    }
                }
            }
            if(!control) {
                traitsText.text = " ";
            }
            break;
            case 6:
            control = false;
            for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] != null) {
                    Merge_Images temp = Items.eguipItems[i].GetComponent<Merge_Images>();
                    if(temp.Object_Traits(2) > 0) {
                        traitsText.text = "+" + temp.Object_Traits(2).ToString();
                        control = true;
                    }
                }
            }
            if(!control) {
                traitsText.text = " ";
            }
            break;
            case 7:
            control = false;
            for(int i = 0; i < Items.eguipItems.Length; i++) {
                if(Items.eguipItems[i] != null) {
                    Merge_Images temp = Items.eguipItems[i].GetComponent<Merge_Images>();
                    if(temp.Object_Traits(3) > 0) {
                        traitsText.text = "+" + temp.Object_Traits(3).ToString();
                        control = true;
                    }
                }
            }
            if(!control) {
                traitsText.text = " ";
            }
            break;
            default:
            Debug.Log("Error");
            break;
        }
    }
    private IEnumerator UpdateText(float waitTime) {
        yield return new WaitForSeconds(Time.deltaTime);

        fillImage = UpdateText(Time.deltaTime);
        StartCoroutine(fillImage);

    }

}




