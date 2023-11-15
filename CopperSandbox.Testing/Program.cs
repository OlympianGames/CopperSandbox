using CopperSandbox.Engine.Components.Common;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Data;
using CopperSandbox.Engine.Scenes;

namespace CopperSandbox.Testing;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings
        {
            EngineConfig = EngineConfig.EditorEnabled | EngineConfig.EditorAtStart | EngineConfig.DebugVisuals,
            WindowTitle =  "Copper's Testing Sandbox"
        };
        CopperEngine.Initialize(engineSettings);

        var testingScene = new Scene("Testing Scene", "testing-scene");
        testingScene.AddComponents(new CopperComponent[] { new CameraController(), new EnvironmentComponent() });
        
        CopperEngine.Update();
    }
}