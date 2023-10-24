using CopperEngine.Core;
using CopperEngine.Logs;
using CopperEngine.NetCode.Messages;
using CopperEngine.NetCode.Transport;
using CopperEngine.Scenes;

namespace CopperEngine.NetCode;

public static class CopperNetCode
{
    public static Peer? Peer { get; internal set; }
    internal static readonly Scene NetCodeScene = new("NetCode Objects", "netcode-objects");
    private static bool initialized;

    
    public static void Initialize(IClient client, string ip = "127.0.0.1", int port = 7777)
    {
        Initialize(new Client(client, ip, port));
    }
    
    public static void Initialize(IServer server, int port = 7777)
    {
        Initialize(new Server(server, port));
    }

    private static void Initialize(Peer peer)
    {
        if (initialized)
            return;
        initialized = true;
        
        MessageHandler.CreateMessageHandlersDictionary();
        
        Peer = peer;
        Core.CopperEngine.EngineUpdate += Update;
        Core.CopperEngine.EngineGameExit += GameExit;
        NetCodeScene.AddComponent(peer);
    }

    private static void Update()
    {
        SceneManager.SceneComponentsUpdateRunner(NetCodeScene, SceneManager.UpdateType.Normal);   
    }

    private static void GameExit()
    {
        SceneManager.SceneComponentsGameQuit(NetCodeScene);
    }
}