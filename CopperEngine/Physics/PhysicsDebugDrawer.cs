using CopperEngine.Utility;
using Jitter2;
using Jitter2.LinearMath;

namespace CopperEngine.Physics;

public class PhysicsDebugDrawer : IDebugDrawer
{
    public static readonly PhysicsDebugDrawer Instance = new();
    
    public void DrawSegment(in JVector pA, in JVector pB)
    {
        ModelUtil.DrawLine3D(pA.ToVector(), pB.ToVector(), ColorUtil.Green);
    }

    public void DrawTriangle(in JVector pA, in JVector pB, in JVector pC)
    {
        ModelUtil.DrawTriangle3D(pA.ToVector(), pB.ToVector(), pC.ToVector(), ColorUtil.Green);
    }

    public void DrawPoint(in JVector p)
    {
        ModelUtil.DrawPoint3D(p.ToVector(), ColorUtil.Green);
    }
}