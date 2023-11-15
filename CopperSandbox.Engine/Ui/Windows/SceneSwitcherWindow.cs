using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Scenes;
using ImGuiNET;

namespace CopperSandbox.Engine.Ui.Windows;

internal class SceneSwitcherWindow : CopperWindow
{
    protected override string WindowName { get; set; } = "Scene Switcher";

    protected override void WindowUpdate()
    {
        foreach (var scene in SceneManager.GetLoadedScenes())
        {
            if (ImGui.Button(SceneManager.GetSceneName(scene), new Vector2(ImGui.GetWindowWidth(), 0)))
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}