using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Physics
{
    public class RaycastArray : IRaycaster
    {
        RayData[] rays = new RayData[0];

        public RaycastArray(int raysPerSide, float width, Vector3 direction, Vector3 baseOffset = new Vector3()) {
            if (raysPerSide < 1) throw new System.ArgumentOutOfRangeException("Rays per size needs to be higher or equal to 1");
            rays = new RayData[(raysPerSide * 2) + 1];

            float spacing = (width * 0.5f) / raysPerSide;
            Vector3 perpVector = Quaternion.Euler(0, 0, 90) * direction;


            rays[0] = new RayData(baseOffset, direction);
            for (int i = 0; i < raysPerSide; i++) {
                rays[((i+1) * 2) - 1] = new RayData(baseOffset + perpVector * spacing * (i + 1), direction);
                rays[((i+1) * 2)] =     new RayData(baseOffset - perpVector * spacing * (i + 1), direction);
            }
        }

        public RaycasterHit Sample(Vector2 position, float distance, LayerMask mask) {
            // TODO: Implement Sample method
            var hits = new RaycastHit2D[rays.Length];
            bool hittedAnything = false;
            float maxDist = 0;

            for (int i = 0; i < rays.Length; i++) {
                RayData ray = rays[i];
                hits[i] = Physics2D.Raycast(position + ray.offset, ray.direction, distance, mask);
                hittedAnything = hittedAnything || hits[i].transform != null;
                maxDist = Mathf.Max(maxDist, hits[i].distance);
            }

            return new RaycasterHit(hits, hittedAnything, maxDist);
        }

        public void DebugDraw(Vector2 position, Color color, float distanceMultiplier = 1) {
            for (int i = 0; i < rays.Length; i++) {
                var ray = rays[i];
                if (ray.direction == Vector2.zero) continue;
                Debug.DrawLine(position + ray.offset, position + ray.offset + ray.direction * distanceMultiplier, color);
            }
        }

    } 
}
