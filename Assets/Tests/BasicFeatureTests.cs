// RotationMath, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

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
            void test(Vector2 input)
            {
                float expected = Vector2.SignedAngle(Vector2.right, input);
                Assert.AreEqual(expected, RotationMath.Degree(input), Tolerance);
            }

            test(Vector2.right);
            test(Vector2.left);
            test(new Vector2(1f, 1f));
            test(new Vector2(-1f, 1f));
            test(new Vector2(0.2f, 0f));
            test(new Vector2(0.4f, -0.3f));
            test(new Vector2(-20, -10));
        }

        [Test]
        public void ForwardYawPitch()
        {
            void test(float yaw, float pitch)
            {
                Vector3 expected = Quaternion.Euler(pitch, yaw, 0) * Vector3.forward;
                VectorAssert.AreEqual(expected, RotationMath.Forward(yaw, pitch), Tolerance);
            }

            test(0, 0);
            test(0, 45);
            test(30, 30);
            test(-30, -90);
            test(-45, 60);
            test(50, -60);
            test(120, 170);
        }

        [Test]
        public void DegreeYawPitch()
        {
            void test(float yaw, float pitch)
            {
                Vector3 vec = Quaternion.Euler(pitch, yaw, 0) * Vector3.forward;
                Assert.AreEqual(yaw, RotationMath.Yaw(vec), Tolerance);
                Assert.AreEqual(pitch, RotationMath.Pitch(vec), Tolerance);
            }

            test(0, 0);
            test(0, 45);
            test(30, 30);
            test(-30, -80);
            test(-45, 60);
            test(50, -60);
            test(120, 80);
            test(-120, -80);
        }

        [Test]
        public void RightQuaternion()
        {
            void test(Quaternion input)
            {
                Vector3 expected = input * Vector3.right;
                VectorAssert.AreEqual(expected, RotationMath.Right(input), Tolerance);
            }

            test(Quaternion.identity);
            test(Quaternion.Euler(90, 0, 0));
            test(Quaternion.Euler(45, 60, 90));
            test(Quaternion.Euler(45, -30, -10));
            test(Quaternion.Euler(-30, 60, 70));
            test(Quaternion.Euler(-30, -185, 0));
        }

        [Test]
        public void UpQuaternion()
        {
            void test(Quaternion input)
            {
                Vector3 expected = input * Vector3.up;
                VectorAssert.AreEqual(expected, RotationMath.Up(input), Tolerance);
            }

            test(Quaternion.identity);
            test(Quaternion.Euler(90, 0, 0));
            test(Quaternion.Euler(45, 60, 90));
            test(Quaternion.Euler(45, -30, -10));
            test(Quaternion.Euler(-30, 60, 70));
            test(Quaternion.Euler(-30, -185, 0));
        }

        [Test]
        public void ForwardQuaternion()
        {
            void test(Quaternion input)
            {
                Vector3 expected = input * Vector3.forward;
                VectorAssert.AreEqual(expected, RotationMath.Forward(input), Tolerance);
            }

            test(Quaternion.identity);
            test(Quaternion.Euler(90, 0, 0));
            test(Quaternion.Euler(45, 60, 90));
            test(Quaternion.Euler(45, -30, -10));
            test(Quaternion.Euler(-30, 60, 70));
            test(Quaternion.Euler(-30, -185, 0));
        }

        [Test]
        public void Projectile()
        {
            float angle;

            Assert.AreEqual(true, RotationMath.Projectile(40f, 20f, -9.8f, out angle));
            Assert.AreEqual(39.260829f, angle, Tolerance);

            Assert.AreEqual(true, RotationMath.Projectile(20f, 20f, -9.8f, out angle));
            Assert.AreEqual(14.670290f, angle, Tolerance);

            Assert.AreEqual(false, RotationMath.Projectile(50f, 20f, -9.8f, out angle));
        }

        [Test]
        public void Projectile3D()
        {
            float angle;

            Assert.AreEqual(true, RotationMath.Projectile(Vector3.zero, new Vector3(40, -10, 0), 20f, -9.8f, out angle));
            Assert.AreEqual(60.437389f, angle, Tolerance);

            Assert.AreEqual(true, RotationMath.Projectile(Vector3.zero, new Vector3(10, 10, 0), 20f, -9.8f, out angle));
            Assert.AreEqual(81.657814f, angle, Tolerance);

            Assert.AreEqual(true, RotationMath.Projectile(Vector3.zero, new Vector3(20, -100, 20), 20f, -9.8f, out angle));
            Assert.AreEqual(78.237945f, angle, Tolerance);

            Assert.AreEqual(false, RotationMath.Projectile(Vector3.zero, new Vector3(50, 0, 0), 20f, -9.8f, out angle));
        }
    }
}