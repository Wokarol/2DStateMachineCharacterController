using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PlayerInput.Extensions
{
    public static class KeyEvaluator
    {
        public static void Evaluate(this Key key, KeyCode keyCode) {
            key.Down = Input.GetKeyDown(keyCode);
            key.Up = Input.GetKeyUp(keyCode);
            key.Hold = Input.GetKey(keyCode);
        }
        public static void Evaluate(this Key key, string keyCode) {
            key.Down = Input.GetKeyDown(keyCode);
            key.Up = Input.GetKeyUp(keyCode);
            key.Hold = Input.GetKey(keyCode);
        }
    } 
}
