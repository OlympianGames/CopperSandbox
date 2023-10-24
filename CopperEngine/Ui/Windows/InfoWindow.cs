using CopperEngine.Core;
using CopperEngine.Info;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using ImGuiNET;
using Raylib_CsLo;

namespace CopperEngine.Ui.Windows;

internal class InfoWindow : CopperWindow
{
    private DateTime lastTime;
    private int framesRendered;
    private int fps;
    private int peakFps;

    private readonly List<int> previousFps = new();
    
    protected override string WindowName { get; set; } = "Info";

    public override void Start()
    {
        SceneManager.SceneChanged += ClearPeakFps;
    }

    public override void Update()
    {
        framesRendered++;

        if (Raylib.IsWindowResized())
        {
            peakFps = 0;
            previousFps.Clear();
        }

        if ((DateTime.Now - lastTime).TotalSeconds >= 1)
        {
            fps = framesRendered;
            framesRendered = 0;
            lastTime = DateTime.Now;
            previousFps.Add(fps);
        }

        if (fps > peakFps)
            peakFps = fps;
    }

    protected override void WindowUpdate()
    {
        if (ImGui.CollapsingHeader("Fps Info"))
        {
            ImGui.Text($"Rl FPS - {Raylib.GetFPS()}");
            ImGui.Text($"CS FPS - {fps}");
            ImGui.Text($"Peak FPS - {peakFps}");
            ImGui.Text($"Frame Rendered - {framesRendered}");
            ImGui.Text($"Frame Time - {Time.DeltaTime}");
            ImGui.Text($"{previousFps.Count}s Average FPS - {AverageFps()}");
        }

        if (ImGui.CollapsingHeader("Scene Info"))
        {
            ImGui.Text($"Current Scene - {SceneManager.CurrentScene.DisplayName} ({SceneManager.CurrentScene.Id})");
        }

        if (ImGui.CollapsingHeader("Physics Info"))
        {
            ImGui.Text($"Current Scene Rigidbody Count - {SceneManager.CurrentScene.PhysicsWorld.RigidBodies.Count}");
            ImGui.Text($"Total Rigidbody Count - {TotalRigidbodies()}");
        }

        if (ImGui.CollapsingHeader("System Info"))
        {
            ImGui.Text($"CPU - {SystemInfo.Cpu}");
            ImGui.Text($"OS - {SystemInfo.Os}");
            ImGui.Text($"Memory Size - {SystemInfo.MemorySize} GB");
            ImGui.Text($"Threads - {SystemInfo.Threads}");
        }

        if (ImGui.CollapsingHeader("Camera Info"))
        {
            ImGui.SeparatorText("Editor Camera");
            CameraInfo("Editor", ref CopperEngine.Core.CopperEngine.EditorCamera);
                
            ImGui.SeparatorText("Game Camera");
            CameraInfo("Game", ref CopperEngine.Core.CopperEngine.GameCamera);
            
            ImGui.SeparatorText("Move Settings");
            if (ImGui.Button("Move game camera to editor camera"))
            {
                CopperEngine.Core.CopperEngine.GameCamera.position = CopperEngine.Core.CopperEngine.EditorCamera.position;
                CopperEngine.Core.CopperEngine.GameCamera.target = CopperEngine.Core.CopperEngine.EditorCamera.target;
            }
        }
    }

    private void CameraInfo(string prefix, ref Camera3D camera)
    {
        ImGui.DragFloat3($"{prefix} Position", ref camera.position);
        ImGui.DragFloat3($"{prefix} Target", ref camera.target);
        ImGui.DragFloat3($"{prefix} Up", ref camera.up);
        ImGui.DragFloat($"{prefix} Fov", ref camera.fovy);
        ImGui.LabelText($"{prefix} Projection", camera.GetProjection());
    }

    private int TotalRigidbodies()
    {
        return SceneManager.GetLoadedScenes().Sum(scene => scene.PhysicsWorld.RigidBodies.Count);
    }
    
    private int AverageFps()
    {
        if (previousFps.Count is 0)
            return 0;
        
        var value = previousFps.Sum();
        return value / previousFps.Count;
    }

    private void ClearPeakFps()
    {
        previousFps.Clear();
        peakFps = 0;
    }
}