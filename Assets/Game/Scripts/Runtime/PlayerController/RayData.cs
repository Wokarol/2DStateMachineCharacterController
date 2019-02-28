using UnityEngine;

namespace Wokarol.Physics
{
    public struct RayData
    {
        public Vector3 offset;
        public Vector3 direction;

        public RayData(Vector3 offset, Vector3 direction) {
            this.offset = offset;
            this.direction = direction;
        }
    }
}