namespace CopperEngine.NetCode.Transport;

public interface IClient : IPeer
{
    public Action<ArraySegment<byte>> OnData { get; set; }
    public Action OnConnect { get; set; }
    public Action OnDisconnect { get; set; }
    public void Connect(string targetIp, int targetPort);
    public bool SendData(ArraySegment<byte> data);
    public void NetCodeUpdate();
    public void Disconnect();
}