using System.Numerics;
using CopperEngine.Core;
using CopperEngine.Data;
using CopperEngine.Info;
using CopperEngine.Utility;
using ImGuiNET;
using Raylib_CsLo;

namespace CopperSandbox.Procedural;

public class DynamicCubes : CopperWindow
{
    protected Vector2Int cubeGridSize = new(50, 50);
    private List<Vector3> cubePositions = new();
    
    private float time = 0;
    private Model model;

    protected override string WindowName { get; set; } = "Dynamic Cubes";

    public override void Update()
    {
        time += Time.DeltaTime;
        cubePositions.Clear();
        var gridSize = cubeGridSize;
        
        for (var x = 0; x < gridSize.X; x++)
        {
            for (var z = 0; z < gridSize.Y; z++)
            {
                var currentPos = new Vector3(x - gridSize.X/2, 0, z - gridSize.Y/2);
                var y = GetYPos(currentPos.X + time, z - currentPos.Y + time) * 5;
                cubePositions.Add(currentPos.WithY(y));
            }
        }

        foreach (var cube in cubePositions)
        {
            // ModelUtil.DrawCube(cube, 1, 1, 1, ColorUtil.Red);
            ModelUtil.DrawModel(model, cube, 1, ColorUtil.White);
        }
    }

    protected virtual float GetYPos(float x, float z)
    {
        return (x + z) / 2;
    }

    public override void Start()
    {
        model = ModelUtil.LoadFromMesh(MeshUtil.GenCube(1, 1, 1));
        model.WithGridTexture(2, 2, ColorUtil.Red, ColorUtil.Maroon);
    }

    protected override void WindowUpdate()
    {
        ImGui.DragFloat("Time", ref time);
        Vector2 grid = cubeGridSize;
        ImGui.DragFloat2("Grid Size", ref grid);
        cubeGridSize = grid;
    }
}