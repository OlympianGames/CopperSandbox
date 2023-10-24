using CopperEngine.Logs;

namespace CopperEngine.NetCode.Transport.Telepathy.Base;

public static class Log
{
    public static void Info(object message) =>
        CopperLogger.WriteBaseLog("INFO] [NETCODE", message, ConsoleColor.DarkGray);
        
    public static void Warning(object message) =>
        CopperLogger.WriteBaseLog("WARNING] [NETCODE", message, ConsoleColor.DarkYellow);

    public static void Error(object message) =>
        CopperLogger.WriteBaseLog("ERROR] [NETCODE", message, ConsoleColor.DarkRed);
}