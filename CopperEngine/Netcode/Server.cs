using System.Text;
using System.Text.Json;
using CopperEngine.Core;
using CopperEngine.NetCode.Messages;
using CopperEngine.NetCode.Transport;

namespace CopperEngine.NetCode;

public class Server : Peer
{
    private readonly IServer transportServer;
    private readonly int port = 0;

    internal Server(IServer transportServer, int port = 7777)
    {
        this.transportServer = transportServer;
        this.port = port;

        this.transportServer.OnData += (client, bytes) =>
        {
            var message = JsonSerializer.Deserialize<Message>(Encoding.ASCII.GetString(bytes)); 
            MessageHandler.HandleMessage(message!.Id, message); 
        };
    }
    public override void Start() => transportServer.Start(port);
    public override void Update() => transportServer.NetCodeUpdate();
    public override void GameExit() => transportServer.Stop();

    public void SendMessage(Message message)
    {
        var json = JsonSerializer.Serialize(message);
        var bytes = Encoding.ASCII.GetBytes(json);

        foreach (var client in transportServer.ConnectedClients)
        {
            transportServer.SendData(client, bytes);
        }
    }
}