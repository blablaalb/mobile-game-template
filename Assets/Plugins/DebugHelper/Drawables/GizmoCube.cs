using System;
using UnityEngine;

namespace DebugHelper.Drawables
{
    [Serializable]
    internal class GizmoCube : IDrawable
    {
        private readonly Color _color;

        public readonly Vector3 Origin;
        public readonly Vector3 Size;
        public readonly Quaternion Rotation;

        public GizmoCube(Vector3 origin, Vector3 size, Quaternion rotation, Color color)
        {
            _color = color;
            Origin = origin;
            Size = size;
            Rotation = rotation;
        }

        public void Draw()
        {
            Color previousColor = Gizmos.color;
            Matrix4x4 previousMatrix4x4 = Gizmos.matrix;
            Matrix4x4 m4x4 = Matrix4x4.TRS(Origin, Rotation, Vector3.one);
            Gizmos.color = _color;
            Gizmos.matrix = m4x4;
            Gizmos.DrawCube(Vector3.zero, Size);
            Gizmos.matrix = previousMatrix4x4;
            Gizmos.color = previousColor;
        }

        public bool Equals(IDrawable other)
        {
            GizmoCube otherGC = other as GizmoCube;
            return otherGC != null && otherGC.Origin.Equals(this.Origin) && otherGC.Size.Equals(this.Size);
        }

        public override bool Equals(object other)
        {
            GizmoCube otherGC = other as GizmoCube;
            return otherGC != null && otherGC.Origin.Equals(this.Origin) && otherGC.Size.Equals(this.Size);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}