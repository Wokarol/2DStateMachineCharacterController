using UnityEngine;

namespace Wokarol.Physics
{
    public struct SurfaceCheckerHit
    {
        public readonly bool Hitted;
        public readonly float ClosestDistance;

        public SurfaceCheckerHit(bool hitted, float closestDistance) {
            Hitted = hitted;
            ClosestDistance = closestDistance;
        }
    }
}