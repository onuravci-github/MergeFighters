using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    class MoveCharacter
    {
        public void LerpMove(float target,Transform transform) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, target + 35, 0), 0.2f);
        }
    }
    public Animator characAnim;
    public Transform characterTransform;
    private float rotateY = 0;
    private MoveCharacter moveCharacter = new MoveCharacter();
    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 oldVelocity;

    public GameObject visible;

    // Deneme
    private Vector3 dashVelocity;
    private float gravity = -1f;

    public Transform groundCheck;
    private float groundDistance = 0.5f;
    public LayerMask groundMask;
    bool isGround;

    public bool ishold;
    public bool isFight;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void MoveUpdate(float x,float z) {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGround) {
            oldVelocity.y = -3f;
            velocity.y = -3f;
        }
        else {
            oldVelocity.y = gravity;
            velocity.y = gravity;
            //oldVelocity.y += gravity * Time.deltaTime;
            //velocity.y += gravity * Time.deltaTime;
        }

        velocity = new Vector3(x, velocity.y, z);

        if(x != 0 && z != 0) {
            rotateY = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            if(!characAnim.GetBool("Run") && !characAnim.GetBool("Hold")) {
                characAnim.SetBool("Run", true);
            }
            if(characAnim.GetBool("Prison") && characAnim.GetBool("Run")){
                characAnim.SetBool("Prison", false);
            }
        }
        else {
            if(!characAnim.GetBool("Hold")) {
                if(characAnim.GetBool("Run")) {
                    characAnim.SetBool("Run", false);
                }
            }
        }
        //if(!ishold) {
            moveCharacter.LerpMove(rotateY, characterTransform);
        //}
        
        //oldVelocity = Vector3.Lerp(controller.velocity, Vector3.zero, 0.1f);
        //oldVelocity = velocity;
        // Deneme
        if(x != 0 && z != 0) {
            dashVelocity = Vector3.Normalize(new Vector3(x, 0, z));
            //Debug.Log("Dash = " + dashVelocity);
        }
        /*else{
            velocity = Vector3.Lerp(controller.velocity, Vector3.zero, 0.1f);
        }*/
        controller.Move(velocity * this.GetComponent<Character>().speed/10f * Time.deltaTime);
    }




    /// <summary>
    /// Dash
    /// </summary>
    /// <param name="dashTime"></param>
    /// <returns></returns>
    private IEnumerator Dash(float dashTime) {
        characAnim.SetBool("Dash", true);
        float starttime = Time.time;
        while(Time.time < starttime + dashTime) {
            controller.Move((this.GetComponent<Character>().speed/2f)*dashVelocity * Time.deltaTime);
            yield return null;
        }
        characAnim.SetBool("Dash", false);
    }

    public void DashStart() {
        StartCoroutine(Dash(0.15f));
    }

    public void JumpStart() {
        StartCoroutine(Jump(0.1f));
    }
    /// <summary>
    /// Dash
    /// </summary>
    /// <param name="dashTime"></param>
    /// <returns></returns>
    private IEnumerator Jump(float waitTime) {
        characAnim.SetBool("Dash", true);
        yield return new WaitForSeconds(waitTime);
        CancelInvoke("CameraReset");
        Camera.main.farClipPlane = 75;
        float starttime = Time.time;
        characAnim.SetBool("Dash", false);
        while(Time.time < starttime + 0.75f) {
            if(Time.time < starttime + 0.65f) {
                controller.Move(dashVelocity / 10f + Vector3.up * 0.5f);
            }
            else {
                controller.Move(dashVelocity / 10f + Vector3.up * 0.1f);
            }
            
            yield return null;
        } 
        while(Time.time < starttime + 1.5f) {

            controller.Move(dashVelocity / 10f);
            yield return null;
        }

        Invoke("CameraReset",0.5f);
        characAnim.SetBool("Dash", false);
    }

    public void CameraReset(){
        Camera.main.farClipPlane = 55;  
    }


    public void SlowDown(){
        this.GetComponent<Character>().speed *= 0.25f;
        Invoke("SlowUp", 4f);
    }
    public void SlowUp(){
        this.GetComponent<Character>().speed *= 4f;
    }
}
