using CopperEngine.Components.Common;
using CopperEngine.Data;
using CopperEngine.NetCode.Transport.Telepathy;
using CopperEngine.Scenes;
using CopperEngine.Utility;
using Engine = CopperEngine.Core.CopperEngine;
using NetCode = CopperEngine.NetCode.CopperNetCode;

namespace CopperSandbox.Networking;

public static class Program
{
    public static void Main(string[] args)
    {
        var engineData = new EngineSettings()
        {
            EngineConfig = EngineConfig.DebugVisuals | EngineConfig.EditorEnabled | EngineConfig.EditorAtStart,
            WindowTitle = $"Copper's Networking Sandbox - {args[0].CapitalizeFirstLetter()}"
        };
        Engine.Initialize(engineData);

        var networkedCubesScene = new Scene("Networked Cubes", "networked-cubes");
        networkedCubesScene.AddComponent(new EnvironmentComponent());
        
        if (args.Contains("server"))
        {
            networkedCubesScene.AddComponent(new ServerNetworkedCubes());
            NetCode.Initialize(new TelepathyServer());
        }
        else if (args.Contains("client"))
        {
            networkedCubesScene.AddComponent(new ClientNetworkedCubes());
            NetCode.Initialize(new TelepathyClient());
        }
        else
            return;
        
        

        Engine.Update();
    }
}