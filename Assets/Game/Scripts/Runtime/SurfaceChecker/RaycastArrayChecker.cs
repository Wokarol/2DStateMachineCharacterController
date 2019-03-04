using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Physics
{
    public class RaycastArrayChecker : ISurfaceChecker
    {
        public RayData[] Rays { get; private set; }

        public RaycastArrayChecker(int raysPerSide, float width, Vector3 direction, Vector3 baseOffset = new Vector3()) {
            if (raysPerSide < 1) throw new System.ArgumentOutOfRangeException("Rays per side needs to be higher or equal to 1");
            Rays = new RayData[(raysPerSide * 2) + 1];

            float spacing = (width * 0.5f) / raysPerSide;
            Vector3 perpendicularVector = Quaternion.Euler(0, 0, 90) * direction;

            Rays[0] = new RayData(baseOffset, direction);
            for(int i = 0; i < raysPerSide; i++) {
                int n = ((i + 1) * 2);
                Rays[n] = new RayData(baseOffset + perpendicularVector * spacing * (i + 1), direction);
                Rays[n - 1] = new RayData(baseOffset - perpendicularVector * spacing * (i + 1), direction);
            }
        }

        public SurfaceCheckerHit Sample(Vector2 position, float distance, LayerMask mask) {
            var hits = new RaycastHit2D[Rays.Length];
            bool hittedAnything = false;
            float closestDistance = distance;

            for(int i = 0; i < Rays.Length; i++) {
                RayData ray = Rays[i];
                hits[i] = Physics2D.Raycast(position + ray.Offset, ray.Direction, distance, mask);
                if(hits[i].transform != null) {
                    hittedAnything = true;
                    closestDistance = Mathf.Min(closestDistance, hits[i].distance);
                }
            }

            return new SurfaceCheckerHit(hittedAnything, closestDistance);
        }
    }

    public struct RayData
    {
        public readonly Vector2 Offset;
        public readonly Vector2 Direction;

        public RayData(Vector2 offset, Vector2 direction) {
            Offset = offset;
            Direction = direction;
        }
    }
}
