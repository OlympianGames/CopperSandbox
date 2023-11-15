using CopperSandbox.Engine.Utility;
using Jitter2.Collision.Shapes;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Physics;

public class PhysicsCube : PhysicsObject
{
    private static readonly Mesh CubeMesh = MeshUtil.GenCube(2);
    private static readonly Material CubeMaterial = MaterialUtil.GenerateGridMaterial(ColorUtil.Red, ColorUtil.Maroon);
    
    protected override Mesh Mesh { get; set; } = CubeMesh;
    protected override Material Material { get; set; } = CubeMaterial;
    protected override List<Shape>? RigidbodyShapes { get; set; } = new() { new BoxShape(2) };
}