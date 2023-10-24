using System.Numerics;
using CopperEngine.Core;
using CopperEngine.Physics;
using CopperEngine.Rendering;
using CopperEngine.Utility;
using ImGuiNET;

namespace CopperSandbox.Physics;

public class ShapeFountain : CopperWindow
{
    protected override string WindowName { get; set; } = "Shape Fountain";

    private readonly Random random = new();
    private Vector3 spawnPos = Vector3.Zero.WithY(10);
    private float spawnRadius = 5;
    private bool spawnObjects = true;
    private int maxRigidbodies = 1000;

    public override void Update()
    {
        Gizmo.DrawWireSphere(spawnPos, spawnRadius, ColorUtil.Red);
    }

    public override void FixedUpdate()
    {
        if(!spawnObjects)
            return;
        
        var randomValue = random.Next(0, 2);
        var pos = (random.AreaInSphere() * spawnRadius) + spawnPos;

        if (ComponentOwner.PhysicsWorld.RigidBodies.Count >= maxRigidbodies)
            return;

        switch (randomValue)
        {
            case 0:
                ComponentOwner.AddComponent(new PhysicsCube() { StartPos = pos } );
                break;
            case 1:
                ComponentOwner.AddComponent(new PhysicsSphere() { StartPos = pos } );
                break;
            default:
                ComponentOwner.AddComponent(new PhysicsCube() { StartPos = pos } );
                break;
        }
    }

    protected override void WindowUpdate()
    {
        ImGui.DragFloat3("Spawn Pos", ref spawnPos);
        ImGui.DragFloat("Spawn Radius", ref spawnRadius);
        ImGui.Checkbox("Spawn Objects", ref spawnObjects);
        ImGui.DragInt("Max Rigidbodies", ref maxRigidbodies, 1, 0, 10000);
    }
}