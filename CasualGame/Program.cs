
using CopperEngine.Components.Common;
using CopperEngine.Core;
using CopperEngine.Data;
using CopperEngine.Scenes;
using Engine = CopperEngine.Core.CopperEngine;

namespace CasualGame;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings
        {
            EngineConfig = EngineConfig.DebugVisuals | EngineConfig.EditorEnabled,
            WindowTitle = "Casual Game"
        };
        Engine.Initialize(engineSettings);

        var testingScene = new Scene("Testing Scene", "testing-scene");
        testingScene.AddComponents(new CopperComponent[] { new PlayerMovement(), new EnvironmentComponent() });
        
        
        Engine.Update();
    }
}