using System.Reflection;
using CopperEngine.Core;
using CopperEngine.Logs;

namespace CopperEngine.Scenes;

public static class SceneManager
{
    private static readonly Scene EmptyScene = new("Empty Scene - Do not use.", "empty-do_not_use");
    internal static Scene CurrentScene { get; private set; } = EmptyScene;
    internal static Dictionary<string, Scene>? Scenes = new();
    public static Action? SceneChanged;

    private static void ScenesCheck()
    {
        if (Scenes is not null) 
            return;
        Scenes = new Dictionary<string, Scene>();
    }
    
    public static List<Scene> GetLoadedScenes()
    {
        ScenesCheck();
        var sceneKeys = Scenes?.Values.ToList();
        sceneKeys?.Remove(EmptyScene);
        sceneKeys?.Remove(CopperEngine.Core.CopperEngine.EngineScene);
        return sceneKeys!;
    }

    public static bool HasScene(string id)
    {
        ScenesCheck();
        return Scenes!.TryGetValue(id, out var scene);
    }
    
    public static string GetSceneName(string id)
    {
        ScenesCheck();
        return Scenes![id].DisplayName;
    }
    
    public static string GetSceneName(Scene id)
    {
        ScenesCheck();
        return Scenes![id].DisplayName;
    }
    
    public static Scene GetScene(string key)
    {
        ScenesCheck();
        return Scenes![key];
    }

    public static void AddScene(Scene scene)
    {
        ScenesCheck();
        if (Scenes!.TryGetValue(scene, out _))
        {
            Log.Warning($"Trying to add a new scene that is already added ({scene.Id})");
            return;
        }
        
        Log.Info($"Adding new scene - {scene.DisplayName} ({scene.Id})");
        Scenes.Add(scene, scene);
    }

    public static void LoadScene(string id)
    {
        if (!HasScene(id))
        {
            Log.Error($"Trying to load scene that does not exist ({id})");
            return;
        }
        
        Log.Info($"Loading new scene - {GetSceneName(id)} ({id})");
        CurrentScene = Scenes?[id]!;
        SceneChanged?.Invoke();
    }
    
    internal enum UpdateType
    {
        Normal,
        Ui,
        Fixed
    }

    internal static void SceneComponentsUpdate()
    {
        SceneComponentsUpdateRunner(CurrentScene, UpdateType.Normal);
    }

    internal static void SceneComponentsUiUpdate()
    {
        SceneComponentsUpdateRunner(CurrentScene, UpdateType.Ui);
    }

    internal static void SceneComponentsFixedUpdate()
    {
        SceneComponentsUpdateRunner(CurrentScene, UpdateType.Fixed);
    }

    internal static void SceneComponentsUpdateRunner(List<CopperComponent> components, UpdateType updateType)
    {
        try
        {
            foreach (var component in components.ToList())
            {
                switch (updateType)
                {
                    case UpdateType.Normal:
                        component.Update();
                        break;
                    case UpdateType.Ui:
                        component.UiUpdate();
                        break;
                    case UpdateType.Fixed:
                        component.FixedUpdate();
                        break;
                    default:
                        Log.Error($"No function to run for {updateType}");
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    internal static void SceneComponentsGameQuit(List<CopperComponent> components)
    {
        try
        {
            foreach (var component in components.ToList())
            {
                component.GameExit();
            }
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    private static readonly Type ComponentType = typeof(CopperComponent);
    
    [Obsolete("Reflection is fucking weird")]
    private static void RunComponentFunction(CopperComponent component, string name)
    {
        var method = ComponentType.GetMethod(name, BindingFlags.Instance);
        method?.Invoke(component, null);
    }
}