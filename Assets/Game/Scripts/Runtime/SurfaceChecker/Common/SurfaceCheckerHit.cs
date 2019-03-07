using UnityEngine;

namespace Wokarol.Physics
{
    public class SurfaceCheckerHit
    {
        public bool Hitted;
        public float ClosestDistance;

        public SurfaceCheckerHit(bool hitted = false, float closestDistance = 0) {
            Hitted = hitted;
            ClosestDistance = closestDistance;
        }

        public void Set(bool hitted, float closestDistance) {
            Hitted = hitted;
            ClosestDistance = closestDistance;
        }
    }
}