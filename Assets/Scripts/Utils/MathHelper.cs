using UnityEngine;

namespace PER.Utils
{
    public static class MathHelper
    {
        // Source: https://github.com/Unity-Technologies/DOTSSample/blob/master/Assets/Scripts/Utils/Primitives/collision.cs
        // and https://monkeyproofsolutions.nl/wordpress/how-to-calculate-the-shortest-distance-between-a-point-and-a-line/
        public static Vector3 ClosestPointOnLineSegment(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            var v = lineEnd - lineStart;
            var t = Vector3.Dot(point - lineStart, v) / Vector3.Dot(v, v);
            t = Mathf.Max(t, 0.0f);
            t = Mathf.Min(t, 1.0f);
            return lineStart + v * t;
        }

        public static Vector3 LocatePointAt(Transform transform, float angle, float offset)
        {
            Vector3 point = transform.position + (Quaternion.AngleAxis(angle, Vector3.up) * transform.forward) * offset;
            return point;
        }

        /// <summary>
        /// Rotates given <paramref name="vector"/> around <paramref name="pivot"/> in given <paramref name="axis"/> with <paramref name ="angle"/>.
        /// </summary>
        /// <param name="vector">Vector to rotate</param>
        /// <param name="pivot">Pivot point</param>
        /// <param name="axis">Axis to rotate around/param>
        /// <param name="angle">Rotation angle</param>
        /// <returns>Rotated vector</returns>
        public static Vector3 RotateAround(this Vector3 vector, Vector3 pivot, Vector3 axis, float angle)
        {
            // Source: https://answers.unity.com/questions/489350/rotatearound-without-transform.html
            Vector3 pos = vector;
            Quaternion rot = Quaternion.AngleAxis(angle, axis); // get the desired rotation
            Vector3 dir = pos - pivot; // find current direction relative to center
            dir = rot * dir; // rotate the direction
            vector = pivot + dir; // define new position

            return vector;
        }

        /// <summary>
        /// Calculate the intersection point of two lines. Returns true if lines intersect, otherwise false.
        /// Note that in 3d, two lines do not intersect most of the time. So if the two lines are not in the same plane, use ClosestPointsOnTwoLines() instead.
        /// </summary>
        /// <param name="intersection">Intersection point</param>
        /// <param name="a1">First point for the line "a"</param>
        /// <param name="a2">Second point for the line "a"</param>
        /// <param name="b1">First point for the line "b"</param>
        /// <param name="b2">Second point for the line "b"</param>
        /// <returns>True if intersection point was found.</returns>
        public static bool LineLineIntersection(out Vector3 intersection, Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2)
        {
            // Source: https://wiki.unity3d.com/index.php/3d_Math_functions
            // I modified the parameters for the method.
            Vector3 lineVec1 = a2 - a1;
            Vector3 lineVec2 = b2 - b1;
            Vector3 lineVec3 = b1 - a1;
            Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
            Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

            float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

            //is coplanar, and not parrallel
            if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
            {
                float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
                intersection = a1 + (lineVec1 * s);
                return true;
            }
            else
            {
                intersection = Vector3.zero;
                return false;
            }
        }


    }




}