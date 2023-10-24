using ImGuiNET;

namespace CopperSandbox.Procedural;

public class SineCubes : DynamicCubes
{
    private float speed = 0.5f;
    private float amplitude = 0.2f;
    private float offset = 0;

    protected override string WindowName { get; set; } = "Sine Cubes";

    protected override float GetYPos(float x, float z)
    {
        return (MathF.Sin((x + z+ offset) * speed) * amplitude);
    }

    protected override void WindowUpdate()
    {
        base.WindowUpdate();
        ImGui.DragFloat("Speed", ref speed, 0.1f, 0, 100);
        ImGui.DragFloat("Amplitude", ref amplitude, 0.1f, 0, 100);
        ImGui.DragFloat("Offset", ref offset);
    }
}