using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Logs;
using ImGuiNET;

namespace CopperSandbox.Engine.Ui.Windows;

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