using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Physics
{
    public class RaycastArrayChecker : ISurfaceChecker
    {
        //bool _isDebug;
        //Color _debugColor;

        public RayData[] Rays { get; private set; }

        public RaycastArrayChecker(int raysPerSide, float width, Vector3 direction, Vector3 baseOffset = new Vector3()) {
            Rays = GetRays(raysPerSide, width, direction, baseOffset);
        }

        //public RaycastArrayChecker DebugMode(Color debugColor) {
        //    _isDebug = true;
        //    _debugColor = debugColor;
        //    return this;
        //}
        //public RaycastArrayChecker DebugMode() {
        //    return DebugMode(Color.red);
        //}
        public static void DrawPreview(Vector2 position, float distance, Color color, int raysPerSide, float width, Vector3 direction, Vector3 baseOffset = new Vector3()) {
            RayData[] rays = GetRays(raysPerSide, width, direction, baseOffset);
            Gizmos.color = color;
            foreach (var ray in rays) {
                Gizmos.DrawRay(position + ray.Offset, ray.Direction * distance);
            }
        }

        public SurfaceCheckerHit Sample(Vector2 position, float distance, LayerMask mask) {
            var hits = new RaycastHit2D[Rays.Length];
            bool hittedAnything = false;
            float closestDistance = distance;

            bool lastQueriesHitTriggers = Physics2D.queriesHitTriggers;
            bool lastQueriesStartInColliders = Physics2D.queriesStartInColliders;

            Physics2D.queriesHitTriggers = false;
            Physics2D.queriesStartInColliders = false;

            for(int i = 0; i < Rays.Length; i++) {
                RayData ray = Rays[i];
                hits[i] = Physics2D.Raycast(position + ray.Offset, ray.Direction, distance, mask);

                if(hits[i].transform != null) {
                    hittedAnything = true;
                    closestDistance = Mathf.Min(closestDistance, hits[i].distance);
                }
                //Debug.DrawRay(position + ray.Offset, ray.Direction * distance, _debugColor);
            }

            Physics2D.queriesHitTriggers = lastQueriesHitTriggers;
            Physics2D.queriesStartInColliders = lastQueriesStartInColliders;

            return new SurfaceCheckerHit(hittedAnything, closestDistance);
        }

        private static RayData[] GetRays(int raysPerSide, float width, Vector3 direction, Vector3 baseOffset) {
            if (raysPerSide < 1) throw new System.ArgumentOutOfRangeException("Rays per side needs to be higher or equal to 1");
            RayData[] rays = new RayData[(raysPerSide * 2) + 1];

            float spacing = (width * 0.5f) / raysPerSide;
            Vector3 perpendicularVector = Quaternion.Euler(0, 0, 90) * direction;

            rays[0] = new RayData(baseOffset, direction);
            for (int i = 0; i < raysPerSide; i++) {
                int n = ((i + 1) * 2);
                rays[n] = new RayData(baseOffset + perpendicularVector * spacing * (i + 1), direction);
                rays[n - 1] = new RayData(baseOffset - perpendicularVector * spacing * (i + 1), direction);
            }

            return rays;
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
