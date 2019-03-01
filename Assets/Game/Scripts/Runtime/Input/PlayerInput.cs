using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PlayerInput
{
    public class PlayerInput : InputData
    {
        private void Update() {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Jump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.JoystickButton0);
            Crouch = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.JoystickButton5) || Input.GetKey(KeyCode.JoystickButton4);
        }
    } 
}
