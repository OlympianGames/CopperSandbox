using CopperSandbox.Engine.Components.Common;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Data;
using CopperSandbox.Engine.Scenes;
using Raylib_CsLo;

namespace CopperSandbox.Testing;

public static class Program
{
    public static void Main()
    {
        // var engineSettings = new EngineSettings
        // {
        //     EngineConfig = EngineConfig.EditorEnabled | EngineConfig.EditorAtStart | EngineConfig.DebugVisuals,
        //     WindowTitle =  "Copper's Testing Sandbox"
        // };
        // CopperEngine.Initialize(engineSettings);
        //
        // var testingScene = new Scene("Testing Scene", "testing-scene");
        // testingScene.AddComponents(new CopperComponent[] { new CameraController(), new EnvironmentComponent() });
        //
        // CopperEngine.Update();
        
        var engineSettings = new EngineSettings
        {
            WindowStartSize = new Vector2Int(600, 450),
            WindowTitle = "New Sandbox",
            WindowFlags = ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE,
            EngineConfig = EngineConfig.EditorEnabled | EngineConfig.EditorAtStart | EngineConfig.DebugVisuals
        };
        CopperEngine.Initialize(engineSettings);
        
        // user added stuff here - scenes, gameobjects, custom ui,
        var newScene = new Scene("Scene Display Name", "scene-id");
        
        newScene.AddComponent(new CameraController());
        newScene.AddComponent<CameraController>();
        
        CopperEngine.Update();
        
        SceneManager.LoadScene("scene-id");
    }
}