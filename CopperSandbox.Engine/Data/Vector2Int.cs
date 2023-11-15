using System.Numerics;

namespace CopperSandbox.Engine.Data;

public struct Vector2Int
{
    public int X;
    public int Y;

    public Vector2Int(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Vector2Int(int value) : this(value, value) { }
    public Vector2Int() : this(0) {}

    public static implicit operator Vector2(Vector2Int vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static implicit operator Vector2Int(Vector2 vector)
    {
        return new Vector2Int((int)vector.X, (int)vector.Y);
    }
}