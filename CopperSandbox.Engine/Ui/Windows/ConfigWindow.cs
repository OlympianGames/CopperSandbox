using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Data;
using ImGuiNET;

namespace CopperSandbox.Engine.Ui.Windows;

internal class ConfigWindow : CopperWindow
{
    protected override string WindowName { get; set; } = "Config";

    public override void Start()
    {
        WindowOpen = false;
    }

    protected override void WindowUpdate()
    {
        DebugVisualsButton();
    }

    private void DebugVisualsButton()
    {
        switch (CopperSandbox.Engine.Core.CopperEngine.DebugVisuals)
        {
            case true:
                if(ImGui.Button("Disable Debug Visuals"))
                    CopperSandbox.Engine.Core.CopperEngine.Settings.RemoveEngineConfig(EngineConfig.DebugVisuals);
                break;
            case false:
                if(ImGui.Button("Enable Debug Visuals"))
                    CopperSandbox.Engine.Core.CopperEngine.Settings.AddEngineConfig(EngineConfig.DebugVisuals);
                break;
        }
    }
}