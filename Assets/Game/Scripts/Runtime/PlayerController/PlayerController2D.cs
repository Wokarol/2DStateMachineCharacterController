using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PlayerInput;

namespace Wokarol.PlayerController
{
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] InputData input = null;

        private void FixedUpdate() {
            transform.rotation = Quaternion.Euler(0, 0, 10 * -input.Horizontal);
            transform.localScale = new Vector3(
                input.Jump ? 0.5f : 1f,
                (input.Sliding ? 0.2f : 1f) * (input.Jump ? 1.2f : 1f)
            );
        }
    }
}
