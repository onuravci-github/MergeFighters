using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float baseHealth;
    public GameObject healthBar;
    private Image healthFill;
    private Text healthText;

    private Character character;
    void Start() {
        character = this.GetComponent<Character>();
        baseHealth = character.healt;
        //if(SceneManager.GetSceneByBuildIndex() == 3)
        healthBar = Instantiate(Resources.Load<GameObject>("Health/PlayerHealthBar"), new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.Euler(45, 0, 0),transform);
        healthFill = healthBar.transform.GetChild(2).GetComponent<Image>();
        healthText = healthBar.GetComponentInChildren<Text>();
    }

    void Update() {
        if(character.healt > 0) {
            //healthBar.transform.position = transform.position + Vector3.up * 3;
            healthFill.fillAmount = character.healt / baseHealth;
            healthText.text = character.healt.ToString("F0");
        }
        else {
            Destroy(healthBar);
        }
    }
}
