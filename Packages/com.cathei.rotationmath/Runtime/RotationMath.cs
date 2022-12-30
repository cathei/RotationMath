using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cathei.Mathematics
{
    public static class RotationMath
    {
        /// <summary>
        /// Calculate direction Vector2 on XY plane of given angle in degrees.
        /// </summary>
        public static Vector2 Direction(float degree)
        {
            return DirectionRadian(degree * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate direction Vector2 on XY plane of given angle in radians.
        /// </summary>
        private static Vector2 DirectionRadian(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        /// <summary>
        /// Calculate degree of given 2D direction.
        /// </summary>
        public static float Degree(Vector2 direction)
        {
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculate normalized direction Vector3 of given yaw (Y-axis rotation) and pitch (X-axis rotation), in degrees.
        /// </summary>
        public static Vector3 Direction(float yaw, float pitch)
        {
            return DirectionRadian(yaw * Mathf.Deg2Rad, pitch * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Calculate normalized direction Vector3 of given yaw (Y-axis rotation) and pitch (X-axis rotation), in radians.
        /// </summary>
        private static Vector3 DirectionRadian(float yaw, float pitch)
        {
            var rot = Mathf.Cos(pitch);
            return new Vector3(Mathf.Cos(yaw) * rot, Mathf.Sin(pitch), Mathf.Sin(yaw) * rot);
        }

        /// <summary>
        /// Calculate yaw (Y axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Yaw(Vector3 direction)
        {
            return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Calculate pitch (X axis rotation) of given normalized direction, in degrees.
        /// </summary>
        public static float Pitch(Vector3 direction)
        {
            return Mathf.Asin(direction.y) * Mathf.Rad2Deg;
        }
    }
}