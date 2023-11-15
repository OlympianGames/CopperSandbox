using System.Numerics;
using CopperSandbox.Engine.Core;
using ImGuiNET;
using imnodesNET;

namespace CopperSandbox.Testing;

public class ImNodesTesting : CopperWindow
{
    protected override string WindowName { get; set; } = "ImNodes Testing";

    protected override void WindowUpdate()
    {
        imnodes.BeginNodeEditor();

        
        imnodes.BeginNode(1);
        ImGui.Dummy(new Vector2(80.0f, 45.0f));
        imnodes.EndNode();

        imnodes.EndNodeEditor();
    }
}