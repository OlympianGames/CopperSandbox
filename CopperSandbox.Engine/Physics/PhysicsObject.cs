using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Utility;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Physics;

public class PhysicsObject : CopperComponent
{
    protected virtual Mesh Mesh { get; set; }
    protected virtual Material Material { get; set; }
    protected RigidBody RigidBody;

    protected virtual List<Shape>? RigidbodyShapes { get; set; }
    
    public Vector3 StartPos = Vector3.Zero;
    private bool rigidbodyCreated = false;

    public Vector3 Position
    {
        get => rigidbodyCreated ? RigidBody.Position.ToVector() : Vector3.Zero;
        set
        {
            if (rigidbodyCreated)
                RigidBody.Position = value.ToJVector();
            else
                StartPos = value;
        }
    }
    
    public override void Start()
    {
        rigidbodyCreated = false;
        World physicsWorld = ComponentOwner;

        RigidBody = physicsWorld.CreateRigidBody();
        RigidBody.Position = Vector3.Zero.ToJVector();
        foreach (var shape in RigidbodyShapes!)
        {
            RigidBody.AddShape(shape);
        }
        RigidBody.IsStatic = false;
        RigidBody.AffectedByGravity = true;
        rigidbodyCreated = true;
        RigidBody.Position = StartPos.ToJVector();
    }

    public override void Update()
    {
        MeshUtil.Draw(Mesh, Material, RigidBody.GetTransformMatrix());
    }
}