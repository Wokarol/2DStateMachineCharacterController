using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;

namespace Wokarol
{
    public class RaycasterDebug : MonoBehaviour
    {
        public DebugBlock DebugBlock { get; } = new DebugBlock("Raycasting");

        RaycastArray downArray;
        RaycastArray rightArray;
        RaycastArray upArray;
        RaycastArray leftArray;

        private void Start() {
            downArray = new RaycastArray(2, 1f, Vector3.down, Vector3.down * 0.5f);
            rightArray = new RaycastArray(2, 1f, Vector3.right, Vector3.right * 0.5f);
            upArray = new RaycastArray(1, 1f, Vector3.up, Vector3.up * 0.5f);
            leftArray = new RaycastArray(2, 1f, Vector3.left, Vector3.left * 0.5f);

            DebugBlock.Define("Down");
            DebugBlock.Define("Up");
            DebugBlock.Define("Left");
            DebugBlock.Define("Right");
        }

        private void Update() {
            UpdateBlockForRaycaster(downArray, "Down");
            UpdateBlockForRaycaster(upArray, "Up");
            UpdateBlockForRaycaster(leftArray, "Left");
            UpdateBlockForRaycaster(rightArray, "Right");

            downArray?.DebugDraw(transform.position,new Color(93/255f, 173/255f, 27/255f), 0.1f);
            rightArray?.DebugDraw(transform.position,new Color(93/255f, 173/255f, 27/255f), 0.1f);
            upArray?.DebugDraw(transform.position,new Color(93/255f, 173/255f, 27/255f), 0.1f);
            leftArray?.DebugDraw(transform.position,new Color(93/255f, 173/255f, 27/255f), 0.1f);

            void UpdateBlockForRaycaster(IRaycaster raycaster, string name) {
                var result = raycaster.Sample(transform.position, 0.1f, int.MaxValue);
                DebugBlock.Change(name, result.Hitted ? "<b><color=#00ffa9>True</color></b>" : "<b><color=#ff6a00>False</color></b>");
            }
        }
    } 
}
