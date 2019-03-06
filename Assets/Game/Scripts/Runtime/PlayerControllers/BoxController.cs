using UnityEngine;
using Wokarol.Physics;
using Wokarol.PlayerInput;

namespace Wokarol.PlayerControllers
{
    public abstract class BoxController : MonoBehaviour
    {
        [SerializeField] int raysPerSide = 1;
        [SerializeField] int raysPerTop = 1;
        [SerializeField] protected float skinWidth = 0.05f;
        [SerializeField] float baseRayLenght = 0.02f;
        [Space]
        [SerializeField] LayerMask groundMask = int.MaxValue;
        [SerializeField] LayerMask cellingMask = int.MaxValue;
        [SerializeField] LayerMask wallsMask = int.MaxValue;
        //[Space]
        //[SerializeField] bool debugMode;

        protected virtual void OnValidate() {
            raysPerSide = Mathf.Max(1, raysPerSide);
            raysPerTop = Mathf.Max(1, raysPerTop);
        }

        protected RaycastArrayChecker leftChecker, rightChecker, upChecker, downChecker;

        protected virtual void Start() {
            float width = 1 - skinWidth * 2;
            leftChecker = new RaycastArrayChecker(raysPerSide, width, Vector3.left, Vector3.left * (0.5f - skinWidth));
            rightChecker = new RaycastArrayChecker(raysPerSide, width, Vector3.right, Vector3.right * (0.5f - skinWidth));
            downChecker = new RaycastArrayChecker(raysPerSide, width, Vector3.down, Vector3.down * (0.5f - skinWidth));
            upChecker = new RaycastArrayChecker(raysPerTop, width, Vector3.up, Vector3.up * (0.5f - skinWidth));

            //if (debugMode) {
            //    leftChecker.DebugMode();
            //    rightChecker.DebugMode();
            //    downChecker.DebugMode();
            //    upChecker.DebugMode();
            //}
        }

        protected void GetSamples(Vector2 moveDelta, out SurfaceCheckerHit rightHit, out SurfaceCheckerHit leftHit, out SurfaceCheckerHit upHit, out SurfaceCheckerHit downHit, Collider2D collider = null) {
            rightHit = rightChecker.Sample(transform.position, Mathf.Max(skinWidth + baseRayLenght, skinWidth + Mathf.Max(0, moveDelta.x)), wallsMask);
            leftHit = leftChecker.Sample(transform.position, Mathf.Max(skinWidth + baseRayLenght, skinWidth - Mathf.Min(0, moveDelta.x)), wallsMask);
            upHit = upChecker.Sample(transform.position, Mathf.Max(skinWidth + baseRayLenght, skinWidth + Mathf.Max(0, moveDelta.y)), cellingMask);
            downHit = downChecker.Sample(transform.position, Mathf.Max(skinWidth + baseRayLenght, skinWidth - Mathf.Min(0, moveDelta.y)), groundMask);
        }

        protected virtual void OnDrawGizmosSelected() {
            float width = 1 - skinWidth * 2;
            float rayLenght = skinWidth + baseRayLenght;
            RaycastArrayChecker.DrawPreview(transform.position, rayLenght, Color.white, raysPerSide, width, Vector3.left, Vector3.left * (0.5f - skinWidth));
            RaycastArrayChecker.DrawPreview(transform.position, rayLenght, Color.white, raysPerSide, width, Vector3.right, Vector3.right * (0.5f - skinWidth));
            RaycastArrayChecker.DrawPreview(transform.position, rayLenght, Color.white, raysPerSide, width, Vector3.down, Vector3.down * (0.5f - skinWidth));
            RaycastArrayChecker.DrawPreview(transform.position, rayLenght, Color.white, raysPerTop, width, Vector3.up, Vector3.up * (0.5f - skinWidth));
        }
    }
}