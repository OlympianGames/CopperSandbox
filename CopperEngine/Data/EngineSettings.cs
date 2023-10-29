using Raylib_CsLo;

namespace CopperEngine.Data;

public class EngineSettings
{
    public Vector2Int WindowStartSize = new(800, 450);
    public string WindowTitle = "Unnamed Instance";

    public ConfigFlags WindowFlags = ConfigFlags.FLAG_MSAA_4X_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE;
    public EngineConfig EngineConfig = EngineConfig.None;

    public void AddEngineConfig(EngineConfig targetConfig) => EngineConfig |= targetConfig;
    public void RemoveEngineConfig(EngineConfig targetConfig) => EngineConfig &= ~targetConfig;
    public bool EngineConfigHas(EngineConfig targetConfig) => EngineConfig.HasFlag(targetConfig);
}