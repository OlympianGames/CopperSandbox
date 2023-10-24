using System.Numerics;

namespace CopperEngine.Utility;

public static class MathUtil
{
    public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
    {
        value.X = Math.Clamp(value.X, min.X, max.X);
        value.Y = Math.Clamp(value.Y, min.Y, max.Y);
        value.Z = Math.Clamp(value.Z, min.Z, max.Z);
        return value;
    }
}