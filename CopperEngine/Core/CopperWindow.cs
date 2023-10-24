using ImGuiNET;

namespace CopperEngine.Core;

public class CopperWindow : CopperComponent
{
    protected virtual string WindowName { get; set; } = "Unnamed";
    protected bool WindowOpen = true;
    protected virtual ImGuiWindowFlags WindowFlags { get; set; } = ImGuiWindowFlags.None;
    
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
            WindowUpdate();
            ImGui.End();
        }
    }

    protected virtual void WindowUpdate()
    {
        
    }
}