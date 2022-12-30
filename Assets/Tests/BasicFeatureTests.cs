using Cathei.Mathematics;
using NUnit.Framework;
using UnityEngine;

namespace Cathei.Mathematics.Tests
{
    public class BasicFeatureTests
    {
        private static readonly float Tolerance = 0.0001f;

        [Test]
        public void Constant()
        {
            Assert.AreEqual(Mathf.Deg2Rad, RotationMath.Deg2Rad, Tolerance);
            Assert.AreEqual(Mathf.Rad2Deg, RotationMath.Rad2Deg, Tolerance);
        }

        [Test]
        public void Normalize()
        {
            Assert.AreEqual(-10f, RotationMath.Normalize(350f), Tolerance);
            Assert.AreEqual(0f, RotationMath.Normalize(720f), Tolerance);
            Assert.AreEqual(45f, RotationMath.Normalize(45f), Tolerance);
            Assert.AreEqual(-45f, RotationMath.Normalize(675f), Tolerance);
            Assert.AreEqual(-175f, RotationMath.Normalize(-535f), Tolerance);
        }

        [Test]
        public void Clamp()
        {
            Assert.AreEqual(0f, RotationMath.Clamp(-10f, 0f, 180f), Tolerance);
            Assert.AreEqual(10f, RotationMath.Clamp(-10f, 10f, 20f), Tolerance);
            Assert.AreEqual(45f, RotationMath.Clamp(45f, 10f, 60f), Tolerance);
            Assert.AreEqual(45f, RotationMath.Clamp(405f, 10f, 60f), Tolerance);
            Assert.AreEqual(-10f, RotationMath.Clamp(380f, 60f, 350f), Tolerance);
            Assert.AreEqual(40f, RotationMath.Clamp(380f, 40f, 350f), Tolerance);
        }

        [Test]
        public void Right2D()
        {
            VectorAssert.AreEqual(Vector2.right, RotationMath.Right(0f), Tolerance);
            VectorAssert.AreEqual(Vector2.up, RotationMath.Right(90f), Tolerance);
            VectorAssert.AreEqual(Vector2.left, RotationMath.Right(180f), Tolerance);
            VectorAssert.AreEqual(Vector2.down, RotationMath.Right(-90f), Tolerance);
            VectorAssert.AreEqual(new Vector2(1, 1).normalized, RotationMath.Right(45f), Tolerance);
            VectorAssert.AreEqual(new Vector2(1, -1).normalized, RotationMath.Right(-45f), Tolerance);
        }

        [Test]
        public void Angle2D()
        {
            Assert.AreEqual(0f, RotationMath.Degree(Vector2.right), Tolerance);
            Assert.AreEqual(45f, RotationMath.Degree(new Vector2(1, 1)), Tolerance);
            Assert.AreEqual(90f, RotationMath.Degree(Vector2.up), Tolerance);
            Assert.AreEqual(180f, RotationMath.Degree(Vector2.left), Tolerance);
            Assert.AreEqual(-45f, RotationMath.Degree(new Vector2(1, -1)), Tolerance);
        }
    }
}