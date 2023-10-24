using CopperEngine.Procedural;
using ImGuiNET;

namespace CopperSandbox.Procedural;

public class PerlinCubes : DynamicCubes
{
    private FastNoiseLite noise;
    private float frequency = 0.03f;

    protected override string WindowName { get; set; } = "Perlin Cubes";

    protected override float GetYPos(float x, float z)
    {
        return noise.GetNoise(x, z);
        // return base.GetYPos(x, z);
    }

    public override void Start()
    {
        base.Start();
        noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
    }

    protected override void WindowUpdate()
    {
        base.WindowUpdate();
        
        ImGui.DragFloat("Frequency", ref frequency, 0.01f, 0, 1);
        noise.SetFrequency(frequency);
    }
}