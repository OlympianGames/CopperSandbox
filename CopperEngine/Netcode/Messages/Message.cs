namespace CopperEngine.NetCode.Messages;

public class Message
{
    public object Data { get; set; }
    public byte Id { get; set; }

    public Message(object data, byte id)
    {
        Data = data;
        Id = id;
    }
}