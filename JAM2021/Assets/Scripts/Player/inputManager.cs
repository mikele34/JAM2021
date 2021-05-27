using UnityEngine;
using UnityEngine.InputSystem;

public class inputManager : MonoBehaviour
{
    [HideInInspector] public bool walkRight = false;
    [HideInInspector] public bool walkLeft = false;
    [HideInInspector] public bool walkUp = false;
    [HideInInspector] public bool walkDown = false;
    [HideInInspector] public bool attack = false;
    [HideInInspector] public bool run = false;
    

    void Update()
    {
        //Setup
        bool GP_leftdpad = false;
        bool GP_rightdpad = false;
        bool GP_updpad = false;
        bool GP_downdpad = false;


        bool GP_shoot = false;
        bool GP_run = false;
        

        if(Gamepad.all.Count > 0)
        {
            GP_leftdpad = Gamepad.all[0].dpad.left.isPressed;
            GP_rightdpad = Gamepad.all[0].dpad.right.isPressed;
            GP_updpad = Gamepad.all[0].dpad.up.isPressed;
            GP_downdpad = Gamepad.all[0].dpad.down.isPressed;
            
            GP_run = Gamepad.all[0].aButton.isPressed;
            GP_shoot = Gamepad.all[0].xButton.wasPressedThisFrame;
        }

        //Right
        if(Keyboard.current.dKey.isPressed || GP_rightdpad)
        {
            walkRight = true;
        }
        else
        {
            walkRight = false;
        }

        //Left
        if (Keyboard.current.aKey.isPressed || GP_leftdpad)
        {
            walkLeft = true;
        }
        else
        {
            walkLeft = false;
        }

        //Up
        if (Keyboard.current.wKey.isPressed || GP_updpad)
        {
            walkUp = true;
        }
        else
        {
            walkUp = false;
        }

        //Down
        if (Keyboard.current.sKey.isPressed || GP_downdpad)
        {
            walkDown = true;
        }
        else
        {
            walkDown = false;
        }

        //Run
        if (Keyboard.current.shiftKey.isPressed || GP_run)
        {
            run = true;
        }
        else
        {
            run = false;
        }

        //Attack
        if (Mouse.current.leftButton.wasPressedThisFrame || GP_shoot)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
    }
}
