using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Wokarol.Physics
{
    public static class ConfinedMove
    {
        public static Vector2 Move(this Transform transform, Vector2 delta, float skinWidth, SurfaceCheckerHit upHit, SurfaceCheckerHit downHit, SurfaceCheckerHit leftHit, SurfaceCheckerHit rightHit) {
            var processedDelta = new Vector2();

            Vector2 dir = new Vector2(Mathf.Sign(delta.x), Mathf.Sign(delta.y));

            SurfaceCheckerHit horiontalLimiter = delta.x > 0 ? rightHit : leftHit;
            SurfaceCheckerHit verticalLimiter = delta.y > 0 ? upHit : downHit;

            processedDelta.x = dir.x * Mathf.Min(Mathf.Max(0, horiontalLimiter.ClosestDistance - skinWidth), Mathf.Abs(delta.x));
            processedDelta.y = dir.y * Mathf.Min(Mathf.Max(0, verticalLimiter.ClosestDistance  - skinWidth), Mathf.Abs(delta.y));

            if(upHit.ClosestDistance < skinWidth) {
                processedDelta.y += upHit.ClosestDistance - skinWidth;
            }
            if (downHit.ClosestDistance < skinWidth) {
                processedDelta.y += -(downHit.ClosestDistance - skinWidth);
            }
            if (rightHit.ClosestDistance < skinWidth) {
                processedDelta.x += rightHit.ClosestDistance - skinWidth;
            }
            if (leftHit.ClosestDistance < skinWidth) {
                processedDelta.x += -(leftHit.ClosestDistance - skinWidth);
            }
            transform.position += (Vector3)processedDelta;
            return processedDelta;
        }
} 
}
