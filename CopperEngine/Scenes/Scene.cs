using CopperEngine.Core;
using CopperEngine.Logs;
using Jitter2;
using Jitter2.Dynamics;

namespace CopperEngine.Scenes;

public class Scene
{
    public readonly string DisplayName;
    public readonly string Id;
    public readonly World PhysicsWorld = new();
    internal readonly List<CopperComponent> Components = new();

    public Scene(string displayName, string id)
    {
        DisplayName = displayName;
        Id = id;
        SceneManager.AddScene(this);
    }

    public void AddComponents(IEnumerable<CopperComponent> components)
    {
        components.ToList().ForEach(AddComponent);
    }
    
    public void AddComponent(CopperComponent component)
    {
        
        Log.Info($"Adding new <{component.GetType().FullName}> to <{DisplayName}> scene");
        component.ComponentOwner = this;
        Components.Add(component);
        component.Start();
    }
    
    public void Load()
    {
        SceneManager.LoadScene(this);
    }
    
    public static implicit operator string(Scene scene) => scene.Id;
    public static implicit operator List<CopperComponent>(Scene scene) => scene.Components;
    public static implicit operator World(Scene? scene) => scene.PhysicsWorld;

    public RigidBody CreateRigidBody() => PhysicsWorld.CreateRigidBody();


}