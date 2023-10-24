using CopperEngine.Core;
using CopperEngine.Logs;

namespace CopperEngine.NetCode.Transport.Telepathy;

public class TelepathyClient : IClient
{
    internal readonly Base.Client Client;

    public TelepathyClient(int maxMessageSize)
    {
        Client = new Base.Client(maxMessageSize);
        Log.Info($"Starting a new {GetType().Name} with a max message size of {maxMessageSize}");
    }

    public TelepathyClient() : this(16 * 1024) { }
    public Action<ArraySegment<byte>> OnData { get => Client.OnData; set => Client.OnData = value; }
    public Action OnConnect { get => Client.OnConnected; set => Client.OnConnected = value; }
    public Action OnDisconnect { get => Client.OnDisconnected; set => Client.OnDisconnected = value; }
    public void Connect(string targetIp = "127.0.0.1", int targetPort = 7777) => Client.Connect(targetIp, targetPort);
    public bool SendData(ArraySegment<byte> data) => Client.Send(data);
    public void Disconnect() => Client.Disconnect();
    public void NetCodeUpdate() => Client.Tick(100);
}