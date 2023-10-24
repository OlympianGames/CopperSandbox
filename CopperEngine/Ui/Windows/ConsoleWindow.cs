using CopperEngine.Core;
using CopperEngine.Logs;
using ImGuiNET;

namespace CopperEngine.Ui.Windows;

internal class ConsoleWindow : CopperWindow
{
    protected override string WindowName { get; set; } = "Console";

    public override void Start()
    {
        WindowOpen = false;
    }
    protected override void WindowUpdate()
    {
        foreach (var logLine in CopperLogger.LogLines)
        {
            ImGui.Text(logLine);
        }
    }
}