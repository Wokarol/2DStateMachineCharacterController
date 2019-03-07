using UnityEngine;

namespace Wokarol.Physics
{
    public interface ISurfaceChecker
    {
        SurfaceCheckerHit Hit { get; }
        void Sample(Vector2 position, float distance, LayerMask mask);
    }
}