using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;

namespace Wokarol
{
    public class RaycasterDebug : MonoBehaviour
    {
        RaycastArray downArray;
        RaycastArray rightArray;
        RaycastArray upArray;
        RaycastArray leftArray;

        private void Start() {
            downArray = new RaycastArray(2, 1f, Vector3.down, Vector3.down * 0.5f);
            rightArray = new RaycastArray(2, 1f, Vector3.right, Vector3.right * 0.5f);
            upArray = new RaycastArray(1, 1f, Vector3.up, Vector3.up * 0.5f);
            leftArray = new RaycastArray(2, 1f, Vector3.left, Vector3.left * 0.5f);
        }

        private void Update() {
            // TODO: Test sampling

            downArray?.DebugDraw(transform.position);
            rightArray?.DebugDraw(transform.position);
            upArray?.DebugDraw(transform.position);
            leftArray?.DebugDraw(transform.position);
        }
    } 
}
