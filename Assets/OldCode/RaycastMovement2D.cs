using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Physics
{
    public static class RaycastMovement2D
    {
        //public static void Move(this Transform transform, Vector2 delta, float skinWidth, RaycasterHit upHit, RaycasterHit downHit, RaycasterHit leftHit, RaycasterHit rightHit) {
        //    var processedDelta = new Vector2();
        //    var horizontalLimiter = delta.x > 0 ? rightHit : leftHit;
        //    var verticalLimiter = delta.y > 0 ? upHit : downHit;

        //    var secondHorizontalLimiter = delta.x <= 0 ? rightHit : leftHit;
        //    var secondVerticalLimiter = delta.y <= 0 ? upHit : downHit;

        //    processedDelta.x = horizontalLimiter.Hitted ? Mathf.Sign(delta.x) * Mathf.Min(Mathf.Max(0, horizontalLimiter.MaxDistance - skinWidth), Mathf.Abs(delta.x)) : delta.x;
        //    processedDelta.y = verticalLimiter.Hitted ?   Mathf.Sign(delta.y) * Mathf.Min(Mathf.Max(0, verticalLimiter.MaxDistance   - skinWidth), Mathf.Abs(delta.y)) : delta.y;

        //    if (!secondHorizontalLimiter.Hitted && horizontalLimiter.Hitted && horizontalLimiter.MaxDistance - skinWidth < 0)
        //        processedDelta.x -= Mathf.Sign(delta.x) * horizontalLimiter.MaxDistance - skinWidth;
        //    if (!secondVerticalLimiter.Hitted && verticalLimiter.Hitted && verticalLimiter.MaxDistance - skinWidth < 0)
        //        processedDelta.y -= Mathf.Sign(delta.y) * verticalLimiter.MaxDistance - skinWidth;

        //    Debug.Log($"x:{Mathf.Max(0, horizontalLimiter.MaxDistance - skinWidth).ToString("F6")} / y:{Mathf.Max(0, verticalLimiter.MaxDistance - skinWidth).ToString("F6")}");

        //    transform.position += (Vector3)processedDelta;
        //}

        public static void Move(this Transform transform, Vector2 delta, float skinWidth, RaycasterHit upHit, RaycasterHit downHit, RaycasterHit leftHit, RaycasterHit rightHit) {
            var processedDelta = new Vector2();

            // TODO: Refactor this

            if (delta.x > 0) {
                // Moving right
                if (rightHit.Hitted) {
                    processedDelta.x = Mathf.Min(rightHit.MaxDistance - skinWidth, delta.x);
                } else {
                    processedDelta.x = delta.x;
                }
            } else if (delta.x < 0) {
                // Moving left
                if (leftHit.Hitted) {
                    // TODO: --------
                } else {
                    processedDelta.x = delta.x;
                }
            }

            if (delta.y > 0) {
                // Moving up
                if (upHit.Hitted) {
                    // TODO: --------
                } else {
                    processedDelta.y = delta.y;
                }
            } else if (delta.y < 0) {
                // Moving down
                if (downHit.Hitted) {
                    // TODO: --------
                } else {
                    processedDelta.y = delta.y;
                }
            }

            transform.position += (Vector3)processedDelta;
        }
    }
}
