
using CopperEngine.Components.Common;
using CopperEngine.Core;
using CopperEngine.Data;
using CopperEngine.Scenes;
using Engine = CopperEngine.Core.CopperEngine;

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
        Engine.Initialize(engineSettings);

        var testingScene = new Scene("Testing Scene", "testing-scene");
        testingScene.AddComponents(new CopperComponent[] { new CameraController(), new EnvironmentComponent() });
        
        Engine.Update();
    }
}