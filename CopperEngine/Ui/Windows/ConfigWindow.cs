using CopperEngine.Core;
using CopperEngine.Data;
using ImGuiNET;

namespace CopperEngine.Ui.Windows;

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
        switch (CopperEngine.Core.CopperEngine.DebugVisuals)
        {
            case true:
                if(ImGui.Button("Disable Debug Visuals"))
                    CopperEngine.Core.CopperEngine.Settings.RemoveEngineConfig(EngineConfig.DebugVisuals);
                break;
            case false:
                if(ImGui.Button("Enable Debug Visuals"))
                    CopperEngine.Core.CopperEngine.Settings.AddEngineConfig(EngineConfig.DebugVisuals);
                break;
        }
    }
}