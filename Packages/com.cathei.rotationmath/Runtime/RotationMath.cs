using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cathei.Mathematics
{
    public static class RotationMath
    {
        public const float Rad2Deg = 180 / MathF.PI;
        public const float Deg2Rad = MathF.PI / 180;

        /// <summary>
        /// Wrap given degree in (-180, 180) range.
        /// </summary>
        public static float Normalize(float degree)
        {
            int offset = FloorToInt((degree + 180) / 360);
            return degree - offset * 360;
        }

        /// <summary>
        /// Clamp given degree with lower bound and upper bound.
        /// The parameters will be treated as angle and normalized in range of -180 to 180.
        /// This method will return closest bound if degree is outside of (min, max) range.
        /// </summary>
        public static float Clamp(float degree, float min, float max)
        {
            // wrap the values
            degree = Normalize(degree);
            min = Normalize(min);
            max = Normalize(max);

            float tDegree = degree, tMax = max;

            if (max < min)
            {
                tDegree += 360;
                tMax += 360;
            }

            if (min <= tDegree && tDegree <= tMax)
                return degree;

            float gap = (360 - (tMax - min)) / 2f;

            if (tDegree < min)
                return tDegree < min - gap ? max : min;
            return tDegree > tMax + gap ? min : max;
        }

        /// <summary>
        /// Calculate projectile ejection angle for given distance, speed and gravity.
        /// Returns false if it's not possible to reach.
        /// </summary>
        public static bool Projectile(float dist, float speed, float gravity, out float angle)
        {
            angle = MathF.Asin(dist * -gravity / (speed * speed)) / 2f;
            angle *= Rad2Deg;

            return float.IsNaN(angle);
        }

        /// <summary>
        /// Calculate direction Vector2 on XY plane of given angle in degrees.
        /// This is optimized version of Quaternion.Euler(0, 0, degree) * Vector3.right.
        /// </summary>
        public static Vector2 Right(float degree)
        {
            float radian = degree * Deg2Rad;
            return new Vector2(MathF.Cos(radian), MathF.Sin(radian));
        }

        /// <summary>
        /// Calculate degree of given 2D direction.
        /// This is optimized version of Vector2.SignedAngle(right, Vector3.right).
        /// </summary>
        public static float Degree(Vector2 right)
        {
            return MathF.Atan2(right.y, right.x) * Rad2Deg;
        }

        /// <summary>
        /// Calculate normalized direction Vector3 of given yaw (Y-axis rotation) and pitch (X-axis rotation), in degrees.
        /// This is optimized version of Quaternion.Euler(pitch, yaw, 0) * Vector3.forward.
        /// </summary>
        public static Vector3 Forward(float yaw, float pitch)
        {
            yaw *= Deg2Rad;
            pitch *= Deg2Rad;

            float rot = MathF.Cos(pitch);
            return new Vector3(MathF.Cos(yaw) * rot, MathF.Sin(pitch), MathF.Sin(yaw) * rot);
        }

        /// <summary>
        /// Calculate yaw (Y axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Yaw(Vector3 forward)
        {
            return MathF.Atan2(forward.z, forward.x) * Rad2Deg;
        }

        /// <summary>
        /// Calculate pitch (X axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Pitch(Vector3 forward)
        {
            return MathF.Asin(forward.y) * Rad2Deg;
        }

        /// <summary>
        /// Get right direction of given Quaternion.
        /// This is optimized version of rotation * Vector3.right.
        /// </summary>
        public static Vector3 Right(Quaternion rotation)
        {
            float y2 = rotation.y * 2f;
            float z2 = rotation.z * 2f;
            float yy2 = rotation.y * y2;
            float zz2 = rotation.z * z2;
            float xy2 = rotation.x * y2;
            float xz2 = rotation.x * z2;
            float yw2 = rotation.w * y2;
            float zw2 = rotation.w * z2;

            return new Vector3(1.0f - (yy2 + zz2), xy2 + zw2, xz2 - yw2);
        }

        /// <summary>
        /// Get up direction of given Quaternion.
        /// This is optimized version of rotation * Vector3.up.
        /// </summary>
        public static Vector3 Up(Quaternion rotation)
        {
            float x2 = rotation.x * 2f;
            float z2 = rotation.z * 2f;
            float xx2 = rotation.x * x2;
            float zz2 = rotation.z * z2;
            float xy2 = rotation.y * x2;
            float yz2 = rotation.y * z2;
            float xw2 = rotation.w * x2;
            float zw2 = rotation.w * z2;

            return new Vector3(xy2 - zw2, 1.0f - (xx2 + zz2), yz2 + xw2);
        }

        /// <summary>
        /// Get forward direction of given Quaternion.
        /// This is optimized version of rotation * Vector3.forward.
        /// </summary>
        public static Vector3 Forward(Quaternion rotation)
        {
            float x2 = rotation.x * 2f;
            float y2 = rotation.y * 2f;
            float xx2 = rotation.x * x2;
            float yy2 = rotation.y * y2;
            float xz2 = rotation.z * x2;
            float yz2 = rotation.z * y2;
            float xw2 = rotation.w * x2;
            float yw2 = rotation.w * y2;

            return new Vector3(xz2 + yw2, yz2 - xw2, 1.0f - (xx2 + yy2));
        }

        /// <summary>
        /// Calculate projectile ejection angle for given positions, speed and gravity.
        /// Returns false if it's not possible to reach.
        /// </summary>
        public static bool Projectile(Vector3 from, Vector3 to, float speed, float gravity, out float angle)
        {
            var diff = to - from;
            float distSqr = diff.x * diff.x + diff.z * diff.z;
            return Projectile(distSqr, diff.y, speed, gravity, out angle);
        }

        private static int FloorToInt(float f)
        {
            return (int)MathF.Floor(f);
        }

        private static bool Projectile(float distSqr, float height, float speed, float gravity, out float angle)
        {
            float numerator = distSqr * -gravity / (speed * speed) + height;
            float denominator = MathF.Sqrt(distSqr + height * height);
            float phase = MathF.Atan2(MathF.Sqrt(distSqr), -height);

            angle = (MathF.Acos(numerator / denominator) - phase) / 2;
            angle *= Rad2Deg;

            return !float.IsNaN(angle);
        }
    }
}