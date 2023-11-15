using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Utility;
using Jitter2;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Components.Common;

public class EnvironmentComponent : CopperComponent
{
    private Model model;
    private RigidBody RigidBody;

    public override void Start()
    {
        World physicsWorld = ComponentOwner;

        var mesh = MeshUtil.GenPlane(100, 100, 100, 100);
        model = ModelUtil.LoadFromMesh(mesh);
        model.WithGridTexture(100, 100);

        RigidBody = physicsWorld.CreateRigidBody();
        RigidBody.Position = Vector3.Zero.WithY(-0.5f).ToJVector();
        RigidBody.IsStatic = true;
        RigidBody.AffectedByGravity = false;
        RigidBody.AddShape(new BoxShape(100, 1, 100));
    }

    public override void Update()
    {
        ModelUtil.DrawModel(model, Vector3.Zero, 1, ColorUtil.White);
    }
}