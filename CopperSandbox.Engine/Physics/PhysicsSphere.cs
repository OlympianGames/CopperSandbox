using CopperSandbox.Engine.Utility;
using Jitter2.Collision.Shapes;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Physics;

public class PhysicsSphere : PhysicsObject
{
    private static readonly Mesh SphereMesh = MeshUtil.GenSphere(1, 16, 16);
    private static readonly Material SphereMaterial = MaterialUtil.GenerateGridMaterial(ColorUtil.Purple, ColorUtil.DarkPurple);
    
    protected override Mesh Mesh { get; set; } = SphereMesh;
    protected override Material Material { get; set; } = SphereMaterial;
    protected override List<Shape>? RigidbodyShapes { get; set; } = new() { new SphereShape(1) };
}