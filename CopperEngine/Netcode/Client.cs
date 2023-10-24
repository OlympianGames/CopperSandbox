using System.Text;
using System.Text.Json;
using CopperEngine.Core;
using CopperEngine.Logs;
using CopperEngine.NetCode.Messages;
using CopperEngine.NetCode.Transport;

namespace CopperEngine.NetCode;

public class Client : Peer
{
    private readonly IClient transportClient;
    private readonly string ip;
    private readonly int port;
    
    internal Client(IClient transportClient, string ip = "127.0.0.1", int port = 7777)
    {
        this.transportClient = transportClient;
        this.ip = ip;
        this.port = port;
        
        this.transportClient.OnData += (bytes) =>
        {
            var message = JsonSerializer.Deserialize<Message>(Encoding.ASCII.GetString(bytes)); 
            MessageHandler.HandleMessage(message!.Id, message); 
            Log.Info($"Handling new message of id {message.Id}");
        };
    }

    public override void Start() => transportClient.Connect(ip, port);
    public override void Update() => transportClient.NetCodeUpdate();
    public override void GameExit() => transportClient.Disconnect();
    
    public void SendMessage(Message message)
    {
        var json = JsonSerializer.Serialize(message);
        var bytes = Encoding.ASCII.GetBytes(json);

        transportClient.SendData(bytes);
    }
}