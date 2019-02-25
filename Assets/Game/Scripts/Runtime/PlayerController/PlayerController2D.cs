using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PlayerInput;

namespace Wokarol.PlayerController
{
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] InputData input = null;
        private Vector3 targetScale = Vector3.one;
        private Quaternion targetRotation;

        private void FixedUpdate() {
            targetScale = new Vector3(
                input.Jump ? 0.5f : 1f,
                (input.Sliding ? 0.2f : 1f) * (input.Jump ? 1.2f : 1f)
            );
            targetRotation = Quaternion.Euler(0, 0, 10 * -input.Horizontal);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 125f * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, 10f * Time.deltaTime);
        }
    }
}
