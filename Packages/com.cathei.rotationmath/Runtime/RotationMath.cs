using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cathei.Mathematics
{
    public static class RotationMath
    {
        /// <summary>
        /// Calculate direction Vector2 on XY plane of given angle in degrees.
        /// This is optimized version of Quaternion.Euler(0, 0, degree) * Vector3.right.
        /// </summary>
        public static Vector2 Right(float degree)
        {
            return RightRadian(degree * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate direction Vector2 on XY plane of given angle in radians.
        /// </summary>
        private static Vector2 RightRadian(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Calculate degree of given 2D direction.
        /// This is optimized version of Vector2.SignedAngle(right, Vector3.right) in 0-360 range.
        /// </summary>
        public static float Degree(Vector2 right)
        {
            return Mathf.Atan2(right.y, right.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculate normalized direction Vector3 of given yaw (Y-axis rotation) and pitch (X-axis rotation), in degrees.
        /// This is optimized version of Quaternion.Euler(pitch, yaw, 0) * Vector3.forward.
        /// </summary>
        public static Vector3 Forward(float yaw, float pitch)
        {
            return ForwardRadian(yaw * Mathf.Deg2Rad, pitch * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate normalized direction Vector3 of given yaw (Y-axis rotation) and pitch (X-axis rotation), in radians.
        /// </summary>
        private static Vector3 ForwardRadian(float yaw, float pitch)
        {
            float rot = Mathf.Cos(pitch);
            return new Vector3(Mathf.Cos(yaw) * rot, Mathf.Sin(pitch), Mathf.Sin(yaw) * rot);
        }

        /// <summary>
        /// Calculate yaw (Y axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Yaw(Vector3 forward)
        {
            return Mathf.Atan2(forward.z, forward.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculate pitch (X axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Pitch(Vector3 forward)
        {
            return Mathf.Asin(forward.y) * Mathf.Rad2Deg;
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
    }
}