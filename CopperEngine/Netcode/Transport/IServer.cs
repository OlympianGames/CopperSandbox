namespace CopperEngine.NetCode.Transport;

public interface IServer : IPeer
{
    public Action<int, ArraySegment<byte>> OnData { get; set; }
    public Action<int> OnConnect { get; set; }
    public Action<int> OnDisconnect { get; set; }
    public List<int> ConnectedClients { get; }
    public void Start(int targetPort);
    public bool SendData(int targetClient, ArraySegment<byte> data);
    public void NetCodeUpdate();
    public void Stop();
}