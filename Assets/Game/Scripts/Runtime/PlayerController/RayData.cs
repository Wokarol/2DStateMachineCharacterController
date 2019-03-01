using UnityEngine;

namespace Wokarol.Physics
{
    public struct RayData
    {
        public Vector2 offset;
        public Vector2 direction;

        public RayData(Vector2 offset, Vector2 direction) {
            this.offset = offset;
            this.direction = direction;
        }
    }
}