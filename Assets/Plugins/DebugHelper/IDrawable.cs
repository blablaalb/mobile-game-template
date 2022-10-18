using System;

namespace DebugHelper
{
    public interface IDrawable : IEquatable<IDrawable>
    {
        void Draw();
    }
}
