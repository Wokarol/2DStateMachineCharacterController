using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;

public class ConfinedMovementTest
{
    Transform transform;

    [SetUp]
    public void Setup() {
        transform = new GameObject().transform;
        Assert.AreEqual(1, Object.FindObjectsOfType<GameObject>().Length, "There is more than one transform");
    }

    [TearDown]
    public void TearDown() {
        Object.DestroyImmediate(transform.gameObject);
        Assert.AreEqual(0, Object.FindObjectsOfType<Transform>().Length, "There was some left over objects");
    }

    [Test]
    public void _00_Is_Instatied_Correctly() {
        Assert.AreEqual(Vector3.zero, transform.position, "Transform is not in centre");
    }

    [Test]
    public void _01_Can_Move_When_There_Are_No_Barriers() {
        SurfaceCheckerHit sch = new SurfaceCheckerHit(false, 20);
        transform.Move(Vector3.right * 2, 0, sch, sch, sch, sch);

        Assert.AreEqual(Vector3.right * 2, transform.position, "Transform was moved incorectly");
    }

    [Test]
    public void _02_Cant_Move_In_Blocked_Direction() {
        SurfaceCheckerHit noBarrier = new SurfaceCheckerHit(false, 20);
        SurfaceCheckerHit barrier = new SurfaceCheckerHit(true, 1);

        Vector3[] allDirections = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        foreach (var dir in allDirections) {

            SurfaceCheckerHit up = dir == Vector3.up ? barrier : noBarrier;
            SurfaceCheckerHit down = dir == Vector3.down ? barrier : noBarrier;
            SurfaceCheckerHit left = dir == Vector3.left ? barrier : noBarrier;
            SurfaceCheckerHit right = dir == Vector3.right ? barrier : noBarrier;

            foreach (var testDir in allDirections) {
                transform.position = Vector3.zero;
                transform.Move(testDir * 2, 0, up, down, left, right);
                Assert.AreEqual(testDir == dir ? testDir : testDir * 2, transform.position, $"Transform was moved incorectly in direction {testDir} with barrier in direction {dir}");
            }
        }
    }

    [Test]
    public void _03_Move_Takes_Skin_Width_Into_Account() {
        SurfaceCheckerHit noBarrier = new SurfaceCheckerHit(false, 20);
        SurfaceCheckerHit barrier = new SurfaceCheckerHit(true, 1.2f);

        Vector3[] allDirections = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        foreach (var dir in allDirections) {

            SurfaceCheckerHit up = dir == Vector3.up ? barrier : noBarrier;
            SurfaceCheckerHit down = dir == Vector3.down ? barrier : noBarrier;
            SurfaceCheckerHit left = dir == Vector3.left ? barrier : noBarrier;
            SurfaceCheckerHit right = dir == Vector3.right ? barrier : noBarrier;

            foreach (var testDir in allDirections) {
                transform.position = Vector3.zero;
                transform.Move(testDir * 2, 0.2f, up, down, left, right);
                Assert.AreEqual(testDir == dir ? testDir : testDir * 2, transform.position, $"Transform was moved incorectly in direction {testDir} with barrier in direction {dir}");
            }
        }
    }

    [Test]
    public void _04_Move_Goes_Out_Of_Obstacle() {
        SurfaceCheckerHit noBarrier = new SurfaceCheckerHit(false, 20);
        SurfaceCheckerHit barrier = new SurfaceCheckerHit(true, 0.1f);

        Vector3[] allDirections = new Vector3[] { Vector3.up, Vector3.down, Vector3.left, Vector3.right };
        foreach (var dir in allDirections) {

            SurfaceCheckerHit up = dir == Vector3.up ? barrier : noBarrier;
            SurfaceCheckerHit down = dir == Vector3.down ? barrier : noBarrier;
            SurfaceCheckerHit left = dir == Vector3.left ? barrier : noBarrier;
            SurfaceCheckerHit right = dir == Vector3.right ? barrier : noBarrier;

            transform.position = Vector3.zero;
            transform.Move(Vector3.zero, 0.2f, up, down, left, right);
            Assert.AreEqual(-dir * 0.1f, transform.position, $"Transform was moved incorectly with barrier in direction {dir}");

            transform.position = Vector3.zero;
            transform.Move(dir, 0.2f, up, down, left, right);
            Assert.AreEqual(-dir * 0.1f, transform.position, $"Transform was moved incorectly with barrier in direction {dir} and with input in same direction");
        }
    }

    [Test]
    public void _05_Move_Does_Not_Move_Out_Of_Obstacle_When_Squished() {
        SurfaceCheckerHit noBarrier = new SurfaceCheckerHit(false, 20);
        SurfaceCheckerHit barrier = new SurfaceCheckerHit(true, 0.1f);

        transform.position = Vector3.zero;
        transform.Move(Vector3.zero, 0.2f, noBarrier, noBarrier, barrier, barrier);
        Assert.AreEqual(Vector3.zero, transform.position, $"Transform was moved incorectly when squished from sides");

        transform.position = Vector3.zero;
        transform.Move(Vector3.zero, 0.2f, barrier, barrier, noBarrier, noBarrier);
        Assert.AreEqual(Vector3.zero, transform.position, $"Transform was moved incorectly when squished from top and down");
    }

    [Test]
    public void _06_Move_Goes_Out_Of_Pointy_Corners() {
        SurfaceCheckerHit noBarrier = new SurfaceCheckerHit(false, 20);
        SurfaceCheckerHit barrier = new SurfaceCheckerHit(true, 0f);

        Vector3[] allDirections = new Vector3[] {
            Vector3.up + Vector3.right,
            Vector3.up + Vector3.left,
            Vector3.down + Vector3.left,
            Vector3.down + Vector3.right};
        for (int i = 0; i < allDirections.Length; i++) {
            Vector3 dir = allDirections[i];
            SurfaceCheckerHit up = dir.y > 0 ? barrier : noBarrier;
            SurfaceCheckerHit down = dir.y < 0 ? barrier : noBarrier;
            SurfaceCheckerHit left = dir.x < 0 ? barrier : noBarrier;
            SurfaceCheckerHit right = dir.x > 0 ? barrier : noBarrier;

            transform.position = Vector3.zero;
            transform.Move(dir, 0.2f, up, down, left, right);
            Assert.AreEqual(-dir * 0.2f, transform.position, $"Transform was moved incorectly with barrier in direction {dir}");
        }
    }
}
