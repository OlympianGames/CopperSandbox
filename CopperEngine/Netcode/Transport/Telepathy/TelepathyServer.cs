using CopperEngine.Core;
using CopperEngine.Logs;

namespace CopperEngine.NetCode.Transport.Telepathy;

public class TelepathyServer : IServer
{
    internal readonly Base.Server Server;

    public TelepathyServer(int maxMessageSize)
    {
        Server = new Base.Server(maxMessageSize);
        Log.Info($"Starting a new {GetType().Name} with a max message size of {maxMessageSize}");

        OnConnect += i => { ConnectedClients.Add(i); };
        OnDisconnect += i => { ConnectedClients.Remove(i); };
    }

    public TelepathyServer() : this(16 * 1024) { }
    public Action<int, ArraySegment<byte>> OnData { get => Server.OnData; set => Server.OnData = value; }
    public Action<int> OnConnect { get => Server.OnConnected; set => Server.OnConnected = value; }
    public Action<int> OnDisconnect { get => Server.OnDisconnected; set => Server.OnDisconnected = value; }
    public List<int> ConnectedClients { get; } = new();
    public void Start(int targetPort = 7777) => Server.Start(targetPort);
    public bool SendData(int targetClient, ArraySegment<byte> data) => Server.Send(targetClient, data);
    public void NetCodeUpdate() => Server.Tick(100);
    public void Stop() => Server.Stop();
}