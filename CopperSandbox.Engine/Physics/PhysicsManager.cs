using CopperSandbox.Engine.Logs;
using CopperSandbox.Engine.Scenes;
using Jitter2.Dynamics;

namespace CopperSandbox.Engine.Physics;

public static class PhysicsManager
{
    private static bool simulatePhysics = true;

    public static void UpdatePhysics()
    {
        try
        {
            if(simulatePhysics)
                SceneManager.CurrentScene.PhysicsWorld.Step(1/50f);
        }
        catch (Exception e)
        {
            Log.Error(e);
        }
    }

    public static List<RigidBody> RigidBodies => SceneManager.CurrentScene.PhysicsWorld.RigidBodies.ToList();
    public static void SetPhysicsState(bool state) => simulatePhysics = state;
    public static bool GetPhysicsState() => simulatePhysics;

    public static RigidBody NewRigidbody(Scene scene)
    {
         return scene.PhysicsWorld.CreateRigidBody();
    }
}