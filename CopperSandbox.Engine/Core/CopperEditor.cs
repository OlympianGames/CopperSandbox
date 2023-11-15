using System.Numerics;
using CopperSandbox.Engine.Info;
using CopperSandbox.Engine.Rendering;
using CopperSandbox.Engine.Ui;
using CopperSandbox.Engine.Utility;
using ImGuiNET;
using ImPlotNET;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Core;

public static class CopperEditor
{
    public static RenderTexture RenderTexture { get; private set; }

    public static Vector2 GameViewSize;
    private static Vector2 gameViewSize = new();
    private static Vector2 oldGameViewSize = new();
    public static bool GameViewFocused = false;
    private static bool gameViewOpen = true;
    
    private static bool imGuiDemoOpen = false;
    private static bool imPlotDemoOpen = false;

    private static bool initialized;
    
    internal static void Initialize()
    {
        if (initialized)
            return;
        initialized = true;

        CopperEngine.EngineUpdate += EditorToggleCheck;
        CopperEngine.EngineUpdate += Update;
        CopperEngine.EngineFixedUpdate += FixedUpdate;
        CopperEngine.EngineUiUpdate += UiUpdate;
    }
    private static void Update()
    {
        GameViewSize = gameViewSize;
    }

    private static void UiUpdate()
    {
        if(imGuiDemoOpen)
            ImGui.ShowDemoWindow(ref imGuiDemoOpen);
        
        if(imPlotDemoOpen)
            ImPlot.ShowDemoWindow(ref imPlotDemoOpen);
        
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Windows"))
            {
                ImGui.MenuItem("Game View", null, ref gameViewOpen);
                ImGui.MenuItem("ImGui Demo", null, ref imGuiDemoOpen);
                ImGui.MenuItem("ImPlot Demo", null, ref imPlotDemoOpen);
                ImGui.EndMenu();
            }
            
            ImGui.EndMainMenuBar();
        }

        if (!gameViewOpen) 
            return;
        
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2(Window.GetScreenWidth(), Window.GetScreenHeight()));
        if (ImGui.Begin("Game View", ref gameViewOpen, ImGuiWindowFlags.NoScrollbar))
        {
            oldGameViewSize = gameViewSize;
            gameViewSize = ImGui.GetWindowSize();
            GameViewFocused = ImGui.IsWindowFocused();

            if (oldGameViewSize != gameViewSize)
            {
                TextureUtil.Unload(RenderTexture);
                RenderTexture = TextureUtil.LoadRenderTexture((int)gameViewSize.X, (int)gameViewSize.Y);
            }
                
            RlImGui.ImageRenderTextureFit(RenderTexture);
        }
        ImGui.PopStyleVar();
    }

    private static void FixedUpdate()
    {
        
    }

    private static void EditorToggleCheck()
    {
        if (Input.IsKeyPressed(KeyboardKey.KEY_F1))
        {
            switch (CopperEngine.EditorActive)
            {
                case true:
                    CopperEngine.EditorActive = false;
                    TextureUtil.Unload(RenderTexture);
                    break;
                case false:
                    CopperEngine.EditorActive = true;
                    RenderTexture = TextureUtil.LoadRenderTexture((int)gameViewSize.X, (int)gameViewSize.Y);
                    break;
            }
        }
    }
}