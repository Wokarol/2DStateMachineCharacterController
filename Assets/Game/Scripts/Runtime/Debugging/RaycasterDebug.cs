using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;
using Wokarol.PlayerInput;

namespace Wokarol
{
    public class RaycasterDebug : MonoBehaviour
    {
        [SerializeField] InputData input = null;

        #region DEBUG_BLOCK
        public DebugBlock DebugBlock { get; } = new DebugBlock("Raycasting"); 
        #endregion

        [SerializeField] float maxDist = 0.8f;
        [SerializeField] float skinWidth = 0.3f;
        [SerializeField] float raycastOffset = 0.025f;
        [SerializeField] float speed = 1f;

        RaycastArray downArray;
        RaycastArray rightArray;
        RaycastArray upArray;
        RaycastArray leftArray;

        private void Start() {
            downArray = new RaycastArray(2, 1f - (raycastOffset * 2), Vector3.down, Vector3.down * (0.5f - skinWidth));
            rightArray = new RaycastArray(2, 1f - (raycastOffset * 2), Vector3.right, Vector3.right * (0.5f - skinWidth));
            upArray = new RaycastArray(1, 1f - (raycastOffset * 2), Vector3.up, Vector3.up * (0.5f - skinWidth));
            leftArray = new RaycastArray(2, 1f - (raycastOffset * 2), Vector3.left, Vector3.left * (0.5f - skinWidth));

            #region DEBUG_BLOCK
            DebugBlock.Define("Down");
            DebugBlock.Define("Up");
            DebugBlock.Define("Left");
            DebugBlock.Define("Right");
            #endregion
        }

        private void Update() {

            Transform t = transform;
            Vector3 pos = t.position;
            float perFrameSpeed = Time.deltaTime * speed;
            t.Move((Vector2.right * (input.Horizontal * perFrameSpeed)) + (Vector2.up * (input.Jump ? perFrameSpeed : 0)) + (Vector2.down * (input.Crouch ? perFrameSpeed : 0)),
                skinWidth,
                upArray.Sample(pos, maxDist, int.MaxValue),
                downArray.Sample(pos, maxDist, int.MaxValue),
                leftArray.Sample(pos, maxDist, int.MaxValue),
                rightArray.Sample(pos, maxDist, int.MaxValue));

            #region DEBUG_BLOCK
            pos = t.position;
            UpdateBlockForRaycaster(downArray, "Down");
            UpdateBlockForRaycaster(upArray, "Up");
            UpdateBlockForRaycaster(leftArray, "Left");
            UpdateBlockForRaycaster(rightArray, "Right");

            Color color = new Color(0.365f, 0.678f, 0.106f);
            downArray?.DebugDraw(pos, color, maxDist);
            rightArray?.DebugDraw(pos, color, maxDist);
            upArray?.DebugDraw(pos, color, maxDist);
            leftArray?.DebugDraw(pos, color, maxDist);

            void UpdateBlockForRaycaster(IRaycaster raycaster, string name) {
                var result = raycaster.Sample(pos, maxDist, int.MaxValue);
                string hitted = result.Hitted ? "<b><color=#00ffa9>True</color></b>" : "<b><color=#ff6a00>False</color></b>";
                DebugBlock.Change(name, $"{hitted}\t{result.MaxDistance.ToString("F3")}");
            }
            #endregion
        }
    }
}
