using System;
using UnityEngine;

namespace DebugHelper.Drawables
{
    [Serializable]
    internal class GizmoSphere : IDrawable
    {
        private readonly Color _color;

        public readonly Vector3 Center;
        public readonly float Radius;
        public readonly Quaternion Rotation;

        public GizmoSphere(Vector3 center, float radius, Quaternion rotation, Color color)
        {
            _color = color;
            Center = center;
            Radius = radius;
            Rotation = rotation;
        }

        public void Draw()
        {
            Color previousColor = Gizmos.color;
            Matrix4x4 previousMatrix4x4 = Gizmos.matrix;
            Matrix4x4 m4x4 = Matrix4x4.TRS(Center, Rotation, Vector3.one);
            Gizmos.color = _color;
            Gizmos.matrix = m4x4;
            Gizmos.DrawSphere(Vector3.zero, Radius);
            Gizmos.matrix = previousMatrix4x4;
            Gizmos.color = previousColor;
        }

        public bool Equals(IDrawable other)
        {
            return other is GizmoSphere otherGC && otherGC.Center.Equals(this.Center) && otherGC.Radius.Equals(this.Radius);
        }

        public override bool Equals(object other)
        {
            return other is GizmoSphere otherGC && otherGC.Center.Equals(this.Center) && otherGC.Radius.Equals(this.Radius);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
