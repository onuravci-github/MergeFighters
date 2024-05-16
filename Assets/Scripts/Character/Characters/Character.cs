using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int healt;
    public int armor;
    public float speed;
    public int power;

    public string chName;
    public bool isHold;

    public static Transform hero;

    private GameObject thunder;

    void Awake() {
        hero = this.transform;
    }
    void Update() {
        if(this.transform.parent != null) {
            this.transform.localEulerAngles = new Vector3(0, 35 - this.transform.parent.localEulerAngles.y,0);
        }
    }
    
    void OnTriggerExit(Collider other) {
        if(other.tag == "Zone") {
            if(thunder != null) {
                Destroy(thunder);
            }
            thunder = Instantiate(Resources.Load<GameObject>("Animation/Thunder"), this.transform.position + Vector3.up * 6, Quaternion.identity,transform);
            CancelInvoke("DangerZoneAttack");
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
        var text = Instantiate(Resources.Load<GameObject>("Animation/Attack Text"), this.transform.position + Vector3.up * 3 - Vector3.right*2, Quaternion.identity);
        text.GetComponentInChildren<Text>().text = numb.ToString("F0");
        Destroy(text, 0.5f);
    }
}
