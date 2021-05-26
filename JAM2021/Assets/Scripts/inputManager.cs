using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [HideInInspector] public bool walkRight = false;
    [HideInInspector] public bool walkLeft = false;
    [HideInInspector] public bool jump = false;
    [HideInInspector] public bool dash = false;
    [HideInInspector] public bool shoot = false;
    [HideInInspector] public bool Cshoot = false;
    [HideInInspector] public bool Sshoot = false;
    [HideInInspector] public bool crouch = false;
    

    void Update()
    {
        //Setup
        float GP_leftStick_X = 0.0f;
        float GP_crouch = 0.0f;

        bool GP_jump = false;
        bool GP_shoot = false;
        bool GP_Sshoot = false;
        bool GP_Cshoot = false;
        bool GP_dash = false;
        

        if(Gamepad.all.Count > 0)
        {
            GP_leftStick_X = Gamepad.all[0].leftStick.ReadValue().x;
            GP_jump = Gamepad.all[0].aButton.wasPressedThisFrame;
            GP_dash = Gamepad.all[0].xButton.wasPressedThisFrame;
            GP_shoot = Gamepad.all[0].bButton.wasPressedThisFrame;
            GP_Sshoot = Gamepad.all[0].bButton.wasReleasedThisFrame;
            GP_Cshoot = Gamepad.all[0].bButton.isPressed;
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

        //Jump
        if (Keyboard.current.spaceKey.wasPressedThisFrame || GP_jump)
        {
            jump = true;
        }
        else
        {
            jump = false;
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

        //Sshoot
        if(Mouse.current.leftButton.wasReleasedThisFrame || GP_Sshoot)
        {
            Sshoot = true;
        }
        else
        {
            Sshoot = false;
        }

        //CShoot
        if (Mouse.current.leftButton.isPressed || GP_Cshoot)
        {
            Cshoot = true;
        }
        else
        {
            Cshoot = false;
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
