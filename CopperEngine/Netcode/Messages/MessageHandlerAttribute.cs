namespace CopperEngine.NetCode.Messages;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class MessageHandlerAttribute : Attribute
{
    internal readonly byte MessageId;

    public MessageHandlerAttribute(byte messageId)
    {
        MessageId = messageId;
    }
}