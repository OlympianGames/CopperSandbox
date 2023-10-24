using System.Numerics;
using CopperEngine.Components.Common;
using CopperEngine.Data;
using CopperEngine.Physics;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using Engine = CopperEngine.Core.CopperEngine;

namespace CopperSandbox.Physics;

public static class Program
{
    public static void Main()
    {
        var engineSettings = new EngineSettings
        {
            EngineConfig = EngineConfig.EditorEnabled | EngineConfig.EditorAtStart| EngineConfig.DebugVisuals,
            WindowTitle =  "Copper's Physics Sandbox"
        };
        Engine.Initialize(engineSettings);
        
        var cubePhysics = new Scene("Cube Physics", "cube-physics");
        cubePhysics.AddComponent(new EnvironmentComponent());

        var offsetPosition = Vector3.Zero;
        for (var i = 0; i < 30; i++)
        {
            var cube = new PhysicsCube()
            {
                StartPos = offsetPosition + (Random.Shared.AreaInSphere() * 5)
            };
            cubePhysics.AddComponent(cube);
        }
        
        var spherePhysics = new Scene("Sphere Physics", "sphere-physics");
        spherePhysics.AddComponent(new EnvironmentComponent());

        offsetPosition = Vector3.Zero;
        for (var i = 0; i < 30; i++)
        {
            var sphere = new PhysicsSphere
            {
                StartPos = offsetPosition + (Random.Shared.AreaInSphere() * 5)
            };
            spherePhysics.AddComponent(sphere);
        }

        var shapeFountain = new Scene("Shape Fountain", "shape-fountain");
        shapeFountain.AddComponent(new ShapeFountain());
        shapeFountain.AddComponent(new EnvironmentComponent());
        
        Engine.Update();
    }
}