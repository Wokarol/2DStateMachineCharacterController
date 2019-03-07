using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;
using Wokarol.PlayerInput;

namespace Wokarol.PlayerControllers
{
    public class FlatTopDownPlayerController : BoxController
    {
        [SerializeField] InputData input = null;
        [Space]
        [SerializeField] float speed = 10;

        private void Update() {
            Vector2 moveDelta = new Vector2(input.Horizontal * speed, ((input.Jump ? 1 : 0) + (input.Crouch ? -1 : 0)) * speed) * Time.deltaTime;
            GetSamples(moveDelta);

            transform.Move(moveDelta, skinWidth, upHit, downHit, leftHit, rightHit);
        }
    } 
}
