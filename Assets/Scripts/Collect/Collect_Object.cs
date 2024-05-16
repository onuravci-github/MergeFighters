using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collect_Object : MonoBehaviour
{
    public static int count = 0;
    public int objectNumb;
    private Animator animator; 
    
    private IEnumerator createCroutine;
    // Start is called before the first frame update
    void Start() {
        animator = this.GetComponent<Animator>();
        count = 0;
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            Destroy(this.gameObject);
        }
        createCroutine = AnimUpdate(0.15f);
        StartCoroutine(createCroutine);

    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            if(count < 25) {
                Merge.merge_Collected[count] = objectNumb;
                count++;
                //this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            }
        }
        else if(other.tag == "Enemy") {
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Environment") {
            Destroy(this.gameObject);
        }

    }



    private IEnumerator AnimUpdate(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        if(Vector3.Distance(Character.hero.position, this.transform.position) >= 30) {
            animator.enabled = false;
        }
        else {
            animator.enabled = true;
        }
        createCroutine = AnimUpdate(waitTime);
        StartCoroutine(createCroutine);
    }
}
