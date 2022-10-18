using System.Collections.Generic;
using DebugHelper.Drawables;
using System.Collections;
using UnityEngine;
using System;

namespace DebugHelper.Drawables
{
    public struct DrawableAndTimePair : IEquatable<DrawableAndTimePair>
    {
        public readonly IDrawable Drawable;
        public readonly float Time;

        public DrawableAndTimePair(IDrawable drawable, float time)
        {
            Drawable = drawable;
            Time = time;
        }

        public bool Equals(DrawableAndTimePair other)
        {
            return other.Drawable.Equals(this.Drawable) && other.Time.Equals(this.Time);
        }

        public override bool Equals(object other)
        {
            return other is DrawableAndTimePair dtp && dtp.Drawable.Equals(this.Drawable) && dtp.Time.Equals(this.Time);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}