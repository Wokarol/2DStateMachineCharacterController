using UnityEngine;

namespace Wokarol.Physics
{
    public interface ISurfaceChecker
    {
        SurfaceCheckerHit Sample(Vector2 position, float distance, LayerMask mask);
    }
}