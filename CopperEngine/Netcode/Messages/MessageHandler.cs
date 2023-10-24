using System.Reflection;
using CopperEngine.Logs;

namespace CopperEngine.NetCode.Messages;

internal static class MessageHandler
{
    private static Dictionary<byte, Action<Message>> messageHandlers = new();

    // tbh took this from riptide cause i was lazy and reflection like this is confusing as hell
    private static MethodInfo[] FindMessageHandlers()
    {
        var thisAssemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a
                .GetReferencedAssemblies()
                .Any(n => n.FullName == thisAssemblyName)) // Get only assemblies that reference this assembly
            .SelectMany(a => a.GetTypes())
            .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)) // Include instance methods in the search so we can show the developer an error instead of silently not adding instance methods to the dictionary
            .Where(m => m.GetCustomAttributes(typeof(MessageHandlerAttribute), false).Length > 0)
            .ToArray();
    }
    
    internal static void CreateMessageHandlersDictionary()
    {
        var methods = FindMessageHandlers();
        messageHandlers = new Dictionary<byte, Action<Message>>(methods.Length);
        
        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<MessageHandlerAttribute>()!;
            
            if (!method.IsStatic)
            {
                Log.Error($"Message handler is not static. Declaring type - {method.DeclaringType}. Method Name - {method.Name}");
                return;
            }
            
            if(messageHandlers.ContainsKey(attribute.MessageId))
            {
                Log.Info($"Message handlers already has a handler for message of id {attribute.MessageId}");
                return;
            }

            messageHandlers.Add(attribute.MessageId, message => { method.Invoke(null, new object?[] { message }); });
        }
    }

    internal static void HandleMessage(byte id, Message message)
    {
        if (messageHandlers.TryGetValue(id, out var value))
        {
            Log.Info($"Handling new message of id {id}");
            value.Invoke(message);
        }
        else
        {
            Log.Error($"Could not find a message handler of id {id}");
        }
        
    }
}