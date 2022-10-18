using System;

namespace DebugHelper
{
    public interface IPrintable : IEquatable<IPrintable>
    {
        string Print();
    }
}
