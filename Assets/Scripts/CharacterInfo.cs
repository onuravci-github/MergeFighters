using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    private static Character character;
    private Text infoText;
    public int infoNumb;
    // Start is called before the first frame update
    void Start() {
        if(character == null) {
            character = FindObjectOfType<Character>();
        }
        infoText = this.GetComponent<Text>();
        InfoUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        InfoUpdate();
    }
    public void InfoUpdate() {
        if(infoNumb == 0) {
            infoText.text = character.healt.ToString();
        }
        else if(infoNumb == 1) {
            infoText.text = character.armor.ToString();
        }
        else if(infoNumb == 2) {
            infoText.text = character.speed.ToString();
        }
        else if(infoNumb == 3) {
            infoText.text = character.power.ToString();
        }
    }
}
