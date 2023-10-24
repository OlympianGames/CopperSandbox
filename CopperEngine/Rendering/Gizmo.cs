using System.Numerics;
using CopperEngine.Utility;
using Raylib_CsLo;

namespace CopperEngine.Rendering;

public static class Gizmo
{
    private static readonly Vector3 MinCubeSize = Vector3.Zero;
    private static readonly Vector3 MaxCubeSize = new(float.MaxValue);

    private static bool DrawGizmos =>
        CopperEngine.Core.CopperEngine.EditorEnabled && CopperEngine.Core.CopperEngine.EditorActive && CopperEngine.Core.CopperEngine.DebugVisuals;
    
    public static void DrawWireCube(Vector3 position, Vector3 size, Color color)
    {
        if (!DrawGizmos)
            return;
        
        size = MathUtil.Clamp(size, MinCubeSize, MaxCubeSize);
        ModelUtil.DrawCubeWires(position, size, color);
    }

    public static void DrawWireSphere(Vector3 position, float radius, Color color)
    {
        if (!DrawGizmos)
            return;
        
        radius = Math.Clamp(radius, 0, float.MaxValue);
        // var size = Math.Clamp((int)(radius * 1.5f), 0, 96);
        var size = 16;
        ModelUtil.DrawSphereWires(position, radius, size, size, color);
    }
}