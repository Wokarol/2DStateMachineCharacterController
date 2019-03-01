using UnityEngine;

namespace Wokarol.Physics
{
    public interface IRaycaster
    {
        RaycasterHit Sample(Vector2 position, float distance, LayerMask mask);
    }

    public struct RaycasterHit
    {

        public RaycasterHit(RaycastHit2D[] hits, bool hitted) {
            Hits = hits;
            Hitted = hitted;
        }

        public readonly RaycastHit2D[] Hits;
        public readonly bool Hitted;
    }
}