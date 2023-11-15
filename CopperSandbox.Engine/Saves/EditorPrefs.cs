using System.Text.Json;
using CopperSandbox.Engine.Logs;
using CopperSandbox.Engine.Utility;

namespace CopperSandbox.Engine.Saves;

public static class EditorPrefs
{
    public static string GetTargetPath()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        return @$"{appData}\CopperStudios\CopperSandbox\EditorPrefs";
    }

    public static void CreateTargetPath()
    {
        if (!Directory.Exists(GetTargetPath()))
            Directory.CreateDirectory(GetTargetPath());
    }

    public static void SavePref(string name, object data)
    {
        CreateTargetPath();
        
        var targetPath = @$"{GetTargetPath()}\{name}.json";
        var fileData = JsonSerializer.SerializeToUtf8Bytes(data);

        var logData = fileData.ToFancyString();
        
        Log.Info($"File data - {logData}");
        File.WriteAllBytes(targetPath, fileData);
        Log.Info($"Saving new editor pref to {targetPath}");
    }

    public static object? LoadPref(string name)
    {
        var targetPath = @$"{GetTargetPath()}\{name}.json";
        Log.Info($"Loading new editor pref from {targetPath}");

        if (!Directory.Exists(targetPath))
            return null;

        var loadedData = File.ReadAllBytes(targetPath);
        
        Log.Info($"Loading new editor pref from {targetPath} | Loaded data - {loadedData}");
        return JsonSerializer.Deserialize<object>(loadedData);
    }
}