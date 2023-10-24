using System.Numerics;
using CopperEngine.Core;
using CopperEngine.Info;
using CopperEngine.Logs;
using CopperEngine.Physics;
using CopperEngine.Utility;
using ImGuiNET;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_CsLo;


namespace CasualGame;

public class PlayerMovement : PhysicsObject
{
    private static readonly Mesh SphereMesh = MeshUtil.GenSphere(1, 16, 16);
    private static readonly Material SphereMaterial = MaterialUtil.GenerateGridMaterial(ColorUtil.Purple, ColorUtil.DarkPurple);
    
    protected override Mesh Mesh { get; set; } = SphereMesh;
    protected override Material Material { get; set; } = SphereMaterial;
    protected override List<Shape>? RigidbodyShapes { get; set; } = new() { new SphereShape(1) };
    
    private string WindowName { get; set; } = "Player Movement";
    private bool WindowOpen = true;
    private ImGuiWindowFlags WindowFlags { get; set; } = ImGuiWindowFlags.None;
    
    private FourAxisInput moveInput = new();
    private float moveForce = 6;

    public override void Start()
    {
        base.Start();
        
        RigidBody.Position = Vector3.Zero.WithY(5).ToJVector();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        AddMoveForce();
    }

    private void AddMoveForce()
    {
        var input = moveInput.GetInput();
        RigidBody.AddForce((new Vector3(input.X, 0, input.Y) * moveForce).ToJVector());
    }

    public override void UiUpdate()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Windows"))
            {
                ImGui.MenuItem(WindowName, null, ref WindowOpen);
                ImGui.EndMenu();
            }
            
            ImGui.EndMainMenuBar();
        }
        
        if (!WindowOpen)
            return;
        
        if (ImGui.Begin(WindowName, ref WindowOpen, WindowFlags))
        {
            ImGui.DragFloat("Move Force", ref moveForce);

            var rbPos = RigidBody.Position.ToVector();
            ImGui.DragFloat3("Position", ref rbPos);
            RigidBody.Position = rbPos.ToJVector();
            
            var rigidBodyAffectedByGravity = RigidBody.AffectedByGravity;
            ImGui.Checkbox("Gravity", ref rigidBodyAffectedByGravity);
            RigidBody.AffectedByGravity = rigidBodyAffectedByGravity;
            
            var rigidBodyIsStatic = RigidBody.IsStatic;
            ImGui.Checkbox("Static", ref rigidBodyIsStatic);
            RigidBody.IsStatic = rigidBodyIsStatic;
            
            ImGui.Separator();

            ImGui.DragFloat3("New Pos", ref newPos);

            if (ImGui.Button("Move"))
            {
                RigidBody.Position = newPos.ToJVector();
            }
            
            ImGui.End();
        }
    }

    private Vector3 newPos;
}