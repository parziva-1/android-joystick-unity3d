using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysticsTouch : MonoBehaviour {
    CharacterController characterController;
    protected Joystick joystick;
    protected JoyButton joybutton;
    protected bool jump;
    [Header("Characterproperties")]
    public float jumpSpeed = 8.0f;
    public float gravity = 9.8f;
    public float characterControllerSpeed;
    

    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<JoyButton>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.Move(new Vector3(joystick.Horizontal, -gravity, joystick.Vertical) * characterControllerSpeed * Time.deltaTime);
        //Salto
        if(!jump && joybutton.Pressed)
        {
            jump = true;
            characterController.Move(new Vector3(0, jumpSpeed, 0) *Time.deltaTime);
        }else if (jump && !joybutton.Pressed)
        {
            jump = false;   
        }
    }

}
