using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    public Joystick joystick;
    //public JoyButton joyButton;

    public CharacterControl characterControl;

    private Vector2 handPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        characterControl = this.GetComponent<CharacterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.parent == null) {
            characterControl.MoveUpdate(joystick.Horizontal, joystick.Vertical);
        }
    }

}
