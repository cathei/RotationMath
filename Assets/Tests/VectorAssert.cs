// RotationMath, Maxwell Keonwoo Kang <code.athei@gmail.com>, 2023

using Cathei.Mathematics;
using NUnit.Framework;
using UnityEngine;

namespace Cathei.Mathematics.Tests
{
    public static class VectorAssert
    {
        public static void AreEqual(Vector2 expected, Vector2 actual, float delta)
        {
            Assert.AreEqual(expected.x, actual.x, delta);
            Assert.AreEqual(expected.y, actual.y, delta);
        }

        public static void AreEqual(Vector3 expected, Vector3 actual, float delta)
        {
            Assert.AreEqual(expected.x, actual.x, delta);
            Assert.AreEqual(expected.y, actual.y, delta);
            Assert.AreEqual(expected.z, actual.z, delta);
        }
    }
}