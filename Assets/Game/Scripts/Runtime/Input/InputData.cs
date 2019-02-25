using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PlayerInput
{
    public abstract class InputData : MonoBehaviour
    {
        public float Horizontal { get; protected set; } = 0;
        public bool Jump { get; protected set; } = false;
        public bool Sliding { get; protected set; } = false;

    }
    public class Key
    {
        public bool Down { get; set; }
        public bool Up { get; set; }
        public bool Hold { get; set; }
    } 
}
