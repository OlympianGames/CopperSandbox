
using CopperEngine.Data;
using CopperEngine.Scenes;
using Engine = CopperEngine.Core.CopperEngine;

namespace CopperSandbox.Procedural;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings
        {
            EngineConfig = EngineConfig.EditorEnabled | EngineConfig.EditorAtStart | EngineConfig.DebugVisuals,
            WindowTitle =  "Copper's Procedural Sandbox"
        };
        Engine.Initialize(engineSettings);

        new Scene("Perlin Cubes", "perlin-cubes").AddComponent(new PerlinCubes());
        new Scene("Sine Cubes", "sine-cubes").AddComponent(new SineCubes());
        new Scene("Dynamic Cubes", "dynamic-cubes").AddComponent(new DynamicCubes());
        var cubeWater = new Scene("Cube Water", "cube-water");
        cubeWater.AddComponent(new CubeWater());
        
        Engine.Update();
    }
}