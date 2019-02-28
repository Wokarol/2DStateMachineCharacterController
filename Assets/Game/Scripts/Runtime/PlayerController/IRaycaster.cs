using UnityEngine;

namespace Wokarol.Physics
{
    public interface IRaycaster
    {
        RaycasterHit Sample(Vector2 position);
    }

    public struct RaycasterHit
    {
        // TODO: Add some values here
    }
}