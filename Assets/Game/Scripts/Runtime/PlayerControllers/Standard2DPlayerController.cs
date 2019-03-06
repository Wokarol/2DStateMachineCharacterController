using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;
using Wokarol.PlayerInput;

namespace Wokarol.PlayerControllers
{
    public class Standard2DPlayerController : BoxController
    {
        [Space]
        [SerializeField] InputData input = null;
        [Space]
        [SerializeField] float jumpHeight = 3f;
        [SerializeField] float timeToJumpApex = .4f;
        [SerializeField] float speed = 10;
        [SerializeField] float fallingGravityMultiplier = 1.5f;
        float gravity;
        float jumpVelocity;

        bool jumpLastPressed;

        float coyoteTimer = 0;
        const float coyoteTime = 0.07f;

        Vector2 velocity = new Vector2();

        protected override void OnValidate() {
            base.Start();
            RecalculateHiddenValues();
        }

        void RecalculateHiddenValues() {
            gravity = -(2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        }

        protected override void Start() {
            base.Start();
            RecalculateHiddenValues();
        }

        private void Update() {
            GetSamples(velocity, out var rightHit, out var leftHit, out var upHit, out var downHit);

            if (!input.Jump) jumpLastPressed = false;

            if (downHit.Hitted && downHit.ClosestDistance <= (skinWidth + 0.01f) && velocity.y < 0) {
                coyoteTimer = coyoteTime;
                velocity.y = 0;
            } else {
                velocity.y += (velocity.y < 0 ? fallingGravityMultiplier : 1) * gravity * Time.deltaTime;
                coyoteTimer -= Time.deltaTime;
            }

            if (input.Jump && !jumpLastPressed && coyoteTimer > 0) {

                if(!(downHit.Hitted && downHit.ClosestDistance <= (skinWidth + 0.01f))) {
                    Debug.Log("COYOTE TIME!!!");
                }

                velocity.y = jumpVelocity;
                jumpLastPressed = true;
            }

            if (upHit.Hitted && upHit.ClosestDistance <= (skinWidth + 0.05f)) {
                velocity.y = gravity * 0.06f;
            }

            velocity.x = input.Horizontal * speed;

            transform.Move(velocity * Time.deltaTime, skinWidth, upHit, downHit, leftHit, rightHit);

            Debug.DrawRay(transform.position, velocity * 0.1f, Color.red);
        }
    }
}
