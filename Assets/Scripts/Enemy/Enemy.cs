using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int healt;
    public int armor;
    public int speed;
    public int power;

    private float baseHealth;
    public GameObject healthBar;
    private Image healthFill;
    private Text healthText;

    public bool isVisible;
    public string enemyName;
    private GameObject thunder;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = healt;
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            healthBar = Instantiate(Resources.Load<GameObject>("Health/EnemyHealthBar"), new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), Quaternion.Euler(45, 0, 0),GetComponentInParent<Enemy_Creator>().transform);
            healthFill = healthBar.transform.GetChild(2).GetComponent<Image>();
            healthText = healthBar.GetComponentInChildren<Text>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            if(healt > 0) {
                healthBar.transform.position = transform.position + Vector3.up * 3;
                healthFill.fillAmount = healt / baseHealth;
                healthText.text = healt.ToString("F0");
            }
            else if(healt <= 0) {
                Destroy(healthBar);
                Instantiate(Resources.Load<GameObject>("Animation/Tomb"), transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "Zone") {
            CancelInvoke("DangerZoneAttack");
            if(thunder != null) {
                Destroy(thunder);
            }
            thunder = Instantiate(Resources.Load<GameObject>("Animation/Thunder"), this.transform.position + Vector3.up * 6, Quaternion.identity, transform);
            thunder.transform.localScale *= 0.5f;
            InvokeRepeating("DangerZoneAttack", 1f, 1f);
        }
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag == "Zone") {
            CancelInvoke("DangerZoneAttack");
            Destroy(thunder);
        }
    }
    void DangerZoneAttack() {
        AttactText(5);
        healt -= 5;
    }

    private void AttactText(int numb) {
        var text = Instantiate(Resources.Load<GameObject>("Animation/Attack Text"), this.transform.position + Vector3.up * 3, Quaternion.identity);
        text.GetComponentInChildren<Text>().text = numb.ToString("F0");
        Destroy(text, 0.5f);
    }

}
