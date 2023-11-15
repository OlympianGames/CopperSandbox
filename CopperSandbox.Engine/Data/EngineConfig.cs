namespace CopperSandbox.Engine.Data;

[Flags]
public enum EngineConfig
{
    None = 0,
    EditorEnabled = 1,
    EditorAtStart = 2,
    DebugVisuals = 4
}