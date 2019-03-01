using UnityEngine;

namespace Wokarol.Physics
{
    public interface IRaycaster
    {
        RaycasterHit Sample(Vector2 position, float distance, LayerMask mask);
    }

    public struct RaycasterHit
    {

        public readonly RaycastHit2D[] Hits;
        public readonly bool Hitted;
        public readonly float MaxDistance;

        public RaycasterHit(RaycastHit2D[] hits, bool hitted, float maxDistance) {
            Hits = hits;
            Hitted = hitted;
            MaxDistance = maxDistance;
        }
    }
}