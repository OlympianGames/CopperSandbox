using System.Diagnostics;
using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Data;
using CopperSandbox.Engine.Info;
using CopperSandbox.Engine.Logs;
using CopperSandbox.Engine.Physics;
using CopperSandbox.Engine.Procedural;
using CopperSandbox.Engine.Utility;
using ImGuiNET;
using Jitter2.Collision.Shapes;
using Jitter2.Dynamics;
using Raylib_CsLo;

namespace CopperSandbox.Procedural;

public class CubeWater : CopperWindow
{
    private Vector2Int cubeGridSize = new(25, 25);
    // private List<RigidBody> physicsCubes = new();
    private RigidBody[,] physicCubes;
    
    private float time = 0;
    private readonly FastNoiseLite noise = new();
    private float frequency = 0.03f;
    
    private Mesh mesh;
    private Material material;

    private bool rigidbodiesCreated = false;
    private string rigidbodyCreationTime = "";

    public override void Update()
    {
        time += Time.DeltaTime;
        var gridSize = cubeGridSize;

        if (!rigidbodiesCreated)
            return;
        
        for (var x = 0; x < gridSize.X; x++)
        {
            for (var z = 0; z < gridSize.Y; z++)
            {
                var currentPos = new Vector3(x - gridSize.X/2, 0, z - gridSize.Y/2);
                var y = GetYPos(currentPos.X + time, z - currentPos.Y + time) * 5;
                var pos = currentPos.WithY(y);

                var cube = physicCubes[x, z];
                cube.Position = pos.ToJVector();
                
                MeshUtil.Draw(mesh, material, cube.GetTransformMatrix());
            }
        }
    }

    private float GetYPos(float x, float z)
    {
        return noise.GetNoise(x, z);
    }

    public override void Start()
    {
        rigidbodiesCreated = false;

        mesh = MeshUtil.GenCube(1, 1, 1);
        material = MaterialUtil.LoadDefault();
        material.WithGridTexture(2, 2, ColorUtil.Blue, ColorUtil.DarkBlue);

        var gridSize = cubeGridSize;
        physicCubes = new RigidBody[gridSize.X, gridSize.Y];
        
        var timer = new Stopwatch();
        timer.Start();

        for (var x = 0; x < gridSize.X; x++)
        {
            for (var z = 0; z < gridSize.Y; z++)
            {
                CreateRigidbody(x, z);
            }
        }
        
        timer.Stop();
        rigidbodyCreationTime = timer.Elapsed.ToString(@"m\:ss\.fff");
        Log.Info($"Time to create rigidbodies - {rigidbodyCreationTime}");

        rigidbodiesCreated = true;
    }

    private void CreateRigidbody(int x, int z)
    {
        var cube = ComponentOwner.CreateRigidBody();
        cube.AddShape(new BoxShape(1));
        cube.IsStatic = true;
        cube.AffectedByGravity = false;
        physicCubes[x, z] = cube;
    }
    
    protected override void WindowUpdate()
    {
        ImGui.DragFloat("Time", ref time);
        ImGui.DragFloat("Frequency", ref frequency, 0.01f, 0, 1);
        noise.SetFrequency(frequency);
        
        ImGui.Separator();
        
        ImGui.LabelText(rigidbodyCreationTime, "Rigidbody Creation Time");
        
        ImGui.Separator();

        if (ImGui.Button("Create Sphere Rigidbody"))
        {
            var pos = Vector3.Zero.WithY(25) + (Random.Shared.AreaInSphere() * 5);
            ComponentOwner.AddComponent(new PhysicsSphere{ StartPos = pos });
        }
    }
}