using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [HideInInspector] public bool walkRight = false;
    [HideInInspector] public bool walkLeft = false;
    [HideInInspector] public bool walkUp = false;
    [HideInInspector] public bool walkDown = false;
    [HideInInspector] public bool dash = false;
    [HideInInspector] public bool shoot = false;
    [HideInInspector] public bool crouch = false;
    

    void Update()
    {
        //Setup
        float GP_leftStick_X = 0.0f;
        float GP_leftStick_Y = 0.0f;
        float GP_crouch = 0.0f;


        bool GP_shoot = false;
        bool GP_dash = false;
        

        if(Gamepad.all.Count > 0)
        {
            GP_leftStick_X = Gamepad.all[0].leftStick.ReadValue().x;
            GP_leftStick_Y = Gamepad.all[0].leftStick.ReadValue().y;
            GP_dash = Gamepad.all[0].xButton.wasPressedThisFrame;
            GP_shoot = Gamepad.all[0].bButton.wasPressedThisFrame;
            GP_crouch = Gamepad.all[0].leftStick.ReadValue().y;
        }

        //Right
        if(Keyboard.current.dKey.isPressed || GP_leftStick_X > 0.1f)
        {
            walkRight = true;
        }
        else
        {
            walkRight = false;
        }

        //Left
        if (Keyboard.current.aKey.isPressed || GP_leftStick_X < -0.1f)
        {
            walkLeft = true;
        }
        else
        {
            walkLeft = false;
        }

        //Up
        if (Keyboard.current.wKey.isPressed || GP_leftStick_Y > 0.1f)
        {
            walkUp = true;
        }
        else
        {
            walkUp = false;
        }

        //Down
        if (Keyboard.current.sKey.isPressed || GP_leftStick_Y < -0.1f)
        {
            walkDown = true;
        }
        else
        {
            walkDown = false;
        }

        //Dash
        if (Keyboard.current.shiftKey.wasPressedThisFrame || GP_dash)
        {
            dash = true;
        }
        else
        {
            dash = false;
        }
        
        //Shoot
        if(Mouse.current.leftButton.wasPressedThisFrame || GP_shoot)
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }

        //Crouch
        if (Keyboard.current.sKey.isPressed || GP_crouch < -0.1)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
    }
}
