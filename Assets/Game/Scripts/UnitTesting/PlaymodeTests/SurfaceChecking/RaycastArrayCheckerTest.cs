using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.Physics;

public class RaycastArrayCheckerTest
{
    RaycastArrayChecker arrayChecker;
    SurfaceCheckerHit hit = new SurfaceCheckerHit();

    [SetUp]
    public void Setup() {
        arrayChecker = new RaycastArrayChecker(hit, 2, 1, Vector3.down);
        Assert.AreEqual(0, Object.FindObjectsOfType<Transform>().Length);
    }

    [TearDown]
    public void Teardown() {
        Assert.AreEqual(0, Object.FindObjectsOfType<Transform>().Length, "There was some left over objects");
    }

    [Test]
    public void _00_Is_Initiallized_Correctly() {
        Assert.AreEqual(0, Object.FindObjectsOfType<Transform>().Length);
        Assert.AreEqual(5, arrayChecker.Rays.Length, "Incorect number of arrays");

        Assert.AreEqual(Vector2.zero, arrayChecker.Rays[0].Offset, "Central array is not in centre");
        Assert.AreEqual(-0.25f, arrayChecker.Rays[1].Offset.x, 0.0000001f, "Firts left array is not setted up correctly");
        Assert.AreEqual( 0.25f, arrayChecker.Rays[2].Offset.x, 0.0000001f, "First right array is not setted up correctly");
        Assert.AreEqual(-0.50f, arrayChecker.Rays[3].Offset.x, 0.0000001f, "Second left array is not setted up correctly");
        Assert.AreEqual( 0.50f, arrayChecker.Rays[4].Offset.x, 0.0000001f, "Second right array is not setted up correctly");
    }

    [Test]
    public void _01_Ground_Checker_Hit_Is_Correctly_Defaulted() {
        arrayChecker.Sample(Vector3.zero, 2, int.MaxValue);

        Assert.AreEqual(false, hit.Hitted, $"{nameof(hit.Hitted)} is not set to false when no objects are hit");
        Assert.AreEqual(2, hit.ClosestDistance, float.Epsilon, $"{nameof(hit.ClosestDistance)} is not set to max distance when no objects are hit");
    }

    [Test]
    public void _02_Ground_Checker_Hits_Ground() {
        var ob = new GameObject();
        ob.transform.SetPositionAndRotation(Vector2.down, Quaternion.identity);
        var collider = ob.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);

        arrayChecker.Sample(Vector3.zero, 2, int.MaxValue);

        Assert.AreEqual(true, hit.Hitted, $"{nameof(hit.Hitted)} is not set to true when object is hit");
        Assert.AreEqual(0.5, hit.ClosestDistance, 0.05f, $"{nameof(hit.ClosestDistance)} is not set to correct value");

        Object.DestroyImmediate(ob);
    }
}
