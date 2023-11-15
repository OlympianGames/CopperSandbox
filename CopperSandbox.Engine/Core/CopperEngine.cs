using System.Numerics;
using CopperSandbox.Engine.Components.Editor;
using CopperSandbox.Engine.Data;
using CopperSandbox.Engine.Logs;
using CopperSandbox.Engine.Physics;
using CopperSandbox.Engine.Rendering;
using CopperSandbox.Engine.Scenes;
using CopperSandbox.Engine.Ui;
using CopperSandbox.Engine.Ui.Windows;
using CopperSandbox.Engine.Utility;
using ImGuiNET;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Core;

public static class CopperEngine
{
    public static bool EditorActive { get; internal set; }
    public static bool EditorEnabled
    {
        get => Settings!.EngineConfigHas(EngineConfig.EditorEnabled);
        set
        {
            switch (value)
            {
                case true:
                    Settings!.AddEngineConfig(EngineConfig.EditorEnabled);
                    break;
                case false:
                    Settings!.RemoveEngineConfig(EngineConfig.EditorEnabled);
                    break;
            }
        }
    }

    public static bool DebugVisuals
    {
        get => Settings!.EngineConfigHas(EngineConfig.DebugVisuals);
        set
        {
            switch (value)
            {
                case true:
                    Settings!.AddEngineConfig(EngineConfig.DebugVisuals);
                    break;
                case false:
                    Settings!.RemoveEngineConfig(EngineConfig.DebugVisuals);
                    break;
            }
        }
    }

    public static Camera3D Camera
    {
        get
        {
            return (EditorEnabled && EditorActive) switch
            {
                true => EditorCamera,
                false => GameCamera
            };
        }
        set
        {
            switch (EditorEnabled && EditorActive)
            {
                case true:
                    EditorCamera = value;
                    break;
                case false:
                    GameCamera = value;
                    break;
            }
        }
    }

    internal static Camera3D GameCamera = new()
    {
        position = new Vector3(1.0f, 10.0f, 10.0f),
        target = new Vector3(0.0f, 0.0f, 0.0f),
        up = new Vector3(0.0f, 1.0f, 0.0f),
        fovy = 60.0f,
        projection_ = CameraProjection.CAMERA_PERSPECTIVE
    };

    internal static Camera3D EditorCamera = new()
    {
        position = new Vector3(1.0f, 10.0f, 10.0f),
        target = new Vector3(0.0f, 0.0f, 0.0f),
        up = new Vector3(0.0f, 1.0f, 0.0f),
        fovy = 60.0f,
        projection_ = CameraProjection.CAMERA_PERSPECTIVE
    };
    
    internal static DateTime StartTime = DateTime.Now;

    internal static EngineSettings? Settings;

    internal static readonly Scene EngineScene = new("Engine Assets", "engine-assets");

    internal static Action? EngineUpdate;
    internal static Action? EngineUiUpdate;
    internal static Action? EngineFixedUpdate;
    internal static Action? EngineGameExit;

    public static void Initialize(EngineSettings settings)
    {
        Settings = settings;
        CopperLogger.Initialize();
        CopperEditor.Initialize();
        
        Window.SetConfigFlags(Settings.WindowFlags);
        Window.Init(Settings.WindowStartSize.X, Settings.WindowStartSize.Y, Settings.WindowTitle);
        RlImGui.Setup();
        Task.Run(FixedUpdate);
        

        if (settings.EngineConfig.HasFlag(EngineConfig.EditorAtStart))
            EditorActive = true;
        
        AddComponent(new EditorCameraController());
        AddComponent(new EditorCameraDrawer());
        AddComponent(new InfoWindow());
        AddComponent(new SceneSwitcherWindow());
        AddComponent(new ConsoleWindow());
        AddComponent(new ConfigWindow());
    }

    private static void AddComponent(CopperComponent component)
    {
        EngineScene.AddComponent(component);
    }

    public static void Update()
    {
        while (!Window.ShouldClose())
        {
            Graphics.BeginDrawing();
            if (EditorEnabled && EditorActive)
                    Graphics.BeginTextureMode(CopperEditor.RenderTexture);
            Graphics.ClearBackground(ColorUtil.SkyBlue);

            Graphics.BeginCamera(Camera);

            if (EditorEnabled && EditorActive)
            {
                EngineUpdate?.Invoke();
                SceneManager.SceneComponentsUpdateRunner(EngineScene, SceneManager.UpdateType.Normal);
            }

            SceneManager.SceneComponentsUpdate();

            Graphics.End3D();

            if (EditorEnabled && EditorActive)
            {
                Graphics.EndTextureMode();

                RlImGui.Begin();
                ImGui.DockSpaceOverViewport();

                EngineUiUpdate?.Invoke();
                SceneManager.SceneComponentsUpdateRunner(EngineScene, SceneManager.UpdateType.Ui);

                SceneManager.SceneComponentsUiUpdate();

                RlImGui.End();
            }

            Graphics.EndDrawing();
        }

        foreach (var scene in SceneManager.Scenes?.Values!)
        {
            SceneManager.SceneComponentsGameQuit(scene);
        }
        
        EngineGameExit?.Invoke();
        RlImGui.Shutdown();
        Window.Close();
        CopperLogger.Shutdown();
    }

    private static async Task FixedUpdate()
    {
        while (!Window.ShouldClose())
        {
            PhysicsManager.UpdatePhysics();
            if (EditorEnabled && EditorActive)
            {
                EngineFixedUpdate?.Invoke();
                SceneManager.SceneComponentsUpdateRunner(EngineScene, SceneManager.UpdateType.Fixed);
            }
            SceneManager.SceneComponentsFixedUpdate();
            
            await Task.Delay(TimeSpan.FromSeconds(0.02f));
        }
    }
}