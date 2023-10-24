using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ImGuiNET;
using ImGuizmoNET;
using imnodesNET;
using ImPlotNET;
using Raylib_CsLo;

namespace CopperEngine.Ui;

// TODO: Properly add ImGuizmo support
// BUG: ImNodes isn't rendering nodes
public static unsafe class RlImGui
{
    private static IntPtr imGuiContext = IntPtr.Zero;
    private static IntPtr imPlotContext = IntPtr.Zero;
    private static IntPtr imNodesContext = IntPtr.Zero;

    private static ImGuiMouseCursor currentMouseCursor = ImGuiMouseCursor.COUNT;
    private static Dictionary<ImGuiMouseCursor, MouseCursor>? mouseCursorMap;
    private static KeyboardKey[]? keyEnumMap;

    private static Texture fontTexture;

    public static void Setup(bool darkTheme = true)
    {
        mouseCursorMap = new Dictionary<ImGuiMouseCursor, MouseCursor>();
        keyEnumMap = Enum.GetValues(typeof(KeyboardKey)) as KeyboardKey[];

        fontTexture.id = 0;

        BeginInitImGui();

        if (darkTheme)
            ImGui.StyleColorsDark();
        else
            ImGui.StyleColorsLight();
        
        SetDarkThemeColors();

        EndInitImGui();
    }
    
    private static void SetDarkThemeColors()
    {
        var colors = ImGui.GetStyle().Colors;
        colors[(int)ImGuiCol.WindowBg] = new Vector4(0.1f, 0.105f, 0.11f, 1.0f);

        // Headers
        colors[(int)ImGuiCol.Header] = new Vector4( 0.2f, 0.205f, 0.21f, 1.0f );
        colors[(int)ImGuiCol.HeaderHovered] = new Vector4( 0.3f, 0.305f, 0.31f, 1.0f );
        colors[(int)ImGuiCol.HeaderActive] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );

        // Buttons
        colors[(int)ImGuiCol.Button] = new Vector4( 0.2f, 0.205f, 0.21f, 1.0f );
        colors[(int)ImGuiCol.ButtonHovered] = new Vector4( 0.3f, 0.305f, 0.31f, 1.0f );
        colors[(int)ImGuiCol.ButtonActive] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );

        // Frame BG
        colors[(int)ImGuiCol.FrameBg] = new Vector4( 0.2f, 0.205f, 0.21f, 1.0f );
        colors[(int)ImGuiCol.FrameBgHovered] = new Vector4( 0.3f, 0.305f, 0.31f, 1.0f );
        colors[(int)ImGuiCol.FrameBgActive] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );

        // Tabs
        colors[(int)ImGuiCol.Tab] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );
        colors[(int)ImGuiCol.TabHovered] = new Vector4( 0.38f, 0.3805f, 0.381f, 1.0f );
        colors[(int)ImGuiCol.TabActive] = new Vector4( 0.28f, 0.2805f, 0.281f, 1.0f );
        colors[(int)ImGuiCol.TabUnfocused] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );
        colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4( 0.2f, 0.205f, 0.21f, 1.0f );

        // Title
        colors[(int)ImGuiCol.TitleBg] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );
        colors[(int)ImGuiCol.TitleBgActive] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );
        colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4( 0.15f, 0.1505f, 0.151f, 1.0f );
    }

    private static void BeginInitImGui()
    {
        imGuiContext = ImGui.CreateContext();
        imPlotContext = ImPlot.CreateContext();
        imNodesContext = imnodes.EditorContextCreate();
        
        ImPlot.SetCurrentContext(imPlotContext);
        ImPlot.SetImGuiContext(imGuiContext);
        
        imnodes.EditorContextSet(imNodesContext);
        imnodes.SetImGuiContext(imGuiContext);
        imnodes.SetNodeGridSpacePos(1, new Vector2(200.0f, 200.0f));
        
        
        ImGuizmo.SetOrthographic(false);
        
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;
        ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;
        // ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = true;
        
        ImGui.GetStyle().WindowRounding = 5;
        ImGui.GetStyle().ChildRounding = 5;
        ImGui.GetStyle().FrameRounding = 5;
        ImGui.GetStyle().PopupRounding = 5;
        ImGui.GetStyle().ScrollbarRounding = 5;
        ImGui.GetStyle().GrabRounding = 5;
        ImGui.GetStyle().TabRounding = 5;

        ImGui.GetStyle().TabBorderSize = 1;

        ImGui.GetStyle().WindowTitleAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextAlign = new Vector2(0.5f);
        ImGui.GetStyle().SeparatorTextPadding = new Vector2(20, 5);
    }

    private static void SetupMouseCursors()
    {
        if (mouseCursorMap == null) return;
        mouseCursorMap.Clear();
        mouseCursorMap[ImGuiMouseCursor.Arrow] = MouseCursor.MOUSE_CURSOR_ARROW;
        mouseCursorMap[ImGuiMouseCursor.TextInput] = MouseCursor.MOUSE_CURSOR_IBEAM;
        mouseCursorMap[ImGuiMouseCursor.Hand] = MouseCursor.MOUSE_CURSOR_POINTING_HAND;
        mouseCursorMap[ImGuiMouseCursor.ResizeAll] = MouseCursor.MOUSE_CURSOR_RESIZE_ALL;
        mouseCursorMap[ImGuiMouseCursor.ResizeEW] = MouseCursor.MOUSE_CURSOR_RESIZE_EW;
        mouseCursorMap[ImGuiMouseCursor.ResizeNESW] = MouseCursor.MOUSE_CURSOR_RESIZE_NESW;
        mouseCursorMap[ImGuiMouseCursor.ResizeNS] = MouseCursor.MOUSE_CURSOR_RESIZE_NS;
        mouseCursorMap[ImGuiMouseCursor.ResizeNWSE] = MouseCursor.MOUSE_CURSOR_RESIZE_NWSE;
        mouseCursorMap[ImGuiMouseCursor.NotAllowed] = MouseCursor.MOUSE_CURSOR_NOT_ALLOWED;
    }

    private static void ReloadFonts()
    {
        ImGui.SetCurrentContext(imGuiContext);
        
        var io = ImGui.GetIO();
        
        ImGui.GetIO().Fonts.AddFontFromFileTTF("Resources/Fonts/Inter/static/Inter-Regular.ttf", 15);
        
        io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out var width, out var height, out _);

        var image = new Image
        {
            data = pixels,
            width = width,
            height = height,
            mipmaps = 1,
            format = (int) PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
        };

        fontTexture = Raylib.LoadTextureFromImage(image);

        io.Fonts.SetTexID(new IntPtr(fontTexture.id));
    }

    private static void EndInitImGui()
    {
        SetupMouseCursors();

        ImGui.SetCurrentContext(imGuiContext);
        
        var fonts = ImGui.GetIO().Fonts;
        ImGui.GetIO().Fonts.AddFontFromFileTTF("Assets/Fonts/RobotoMono-Regular.ttf", 
            22, null, fonts.GetGlyphRangesCyrillic());

        var io = ImGui.GetIO();
        io.KeyMap[(int)ImGuiKey.Tab] = (int)KeyboardKey.KEY_TAB;
        io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)KeyboardKey.KEY_LEFT;
        io.KeyMap[(int)ImGuiKey.RightArrow] = (int)KeyboardKey.KEY_RIGHT;
        io.KeyMap[(int)ImGuiKey.UpArrow] = (int)KeyboardKey.KEY_UP;
        io.KeyMap[(int)ImGuiKey.DownArrow] = (int)KeyboardKey.KEY_DOWN;
        io.KeyMap[(int)ImGuiKey.PageUp] = (int)KeyboardKey.KEY_PAGE_UP;
        io.KeyMap[(int)ImGuiKey.PageDown] = (int)KeyboardKey.KEY_PAGE_DOWN;
        io.KeyMap[(int)ImGuiKey.Home] = (int)KeyboardKey.KEY_HOME;
        io.KeyMap[(int)ImGuiKey.End] = (int)KeyboardKey.KEY_END;
        io.KeyMap[(int)ImGuiKey.Delete] = (int)KeyboardKey.KEY_DELETE;
        io.KeyMap[(int)ImGuiKey.Backspace] = (int)KeyboardKey.KEY_BACKSPACE;
        io.KeyMap[(int)ImGuiKey.Enter] = (int)KeyboardKey.KEY_ENTER;
        io.KeyMap[(int)ImGuiKey.Escape] = (int)KeyboardKey.KEY_ESCAPE;
        io.KeyMap[(int)ImGuiKey.Space] = (int)KeyboardKey.KEY_SPACE;
        io.KeyMap[(int)ImGuiKey.A] = (int)KeyboardKey.KEY_A;
        io.KeyMap[(int)ImGuiKey.C] = (int)KeyboardKey.KEY_C;
        io.KeyMap[(int)ImGuiKey.V] = (int)KeyboardKey.KEY_V;
        io.KeyMap[(int)ImGuiKey.X] = (int)KeyboardKey.KEY_X;
        io.KeyMap[(int)ImGuiKey.Y] = (int)KeyboardKey.KEY_Y;
        io.KeyMap[(int)ImGuiKey.Z] = (int)KeyboardKey.KEY_Z;

        ReloadFonts();
    }

    private static void NewFrame()
    {
        var io = ImGui.GetIO();

        if (Raylib.IsWindowFullscreen())
        {
            var monitor = Raylib.GetCurrentMonitor();
            io.DisplaySize = new Vector2(Raylib.GetMonitorWidth(monitor), Raylib.GetMonitorHeight(monitor));
        }
        else
        {
            io.DisplaySize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
            
        io.DisplayFramebufferScale = new Vector2(1, 1);
        io.DeltaTime = Raylib.GetFrameTime();

        io.KeyCtrl = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_CONTROL) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL);
        io.KeyShift = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SHIFT) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);
        io.KeyAlt = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_ALT) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT);
        io.KeySuper = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SUPER) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SUPER);

        if (io.WantSetMousePos)
        {
            Raylib.SetMousePosition((int)io.MousePos.X, (int)io.MousePos.Y);
        }
        else
        {
            io.MousePos = Raylib.GetMousePosition();
        }

        io.MouseDown[0] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT);
        io.MouseDown[1] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT);
        io.MouseDown[2] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_MIDDLE);

        if (Raylib.GetMouseWheelMove() > 0)
            io.MouseWheel += 1;
        else if (Raylib.GetMouseWheelMove() < 0)
            io.MouseWheel -= 1;

        if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0) return;

        var imGuiMouseCursor = ImGui.GetMouseCursor();
        if (imGuiMouseCursor == currentMouseCursor && !io.MouseDrawCursor) return;
        currentMouseCursor = imGuiMouseCursor;
        if (io.MouseDrawCursor || imGuiMouseCursor == ImGuiMouseCursor.None)
        {
            Raylib.HideCursor();
        }
        else
        {
            Raylib.ShowCursor();

            if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0 || mouseCursorMap == null) 
                return;
            Raylib.SetMouseCursor(!mouseCursorMap.ContainsKey(imGuiMouseCursor)
                ? MouseCursor.MOUSE_CURSOR_DEFAULT
                : mouseCursorMap[imGuiMouseCursor]);
        }
        
        if (io.ConfigFlags.HasFlag(ImGuiConfigFlags.ViewportsEnable))
        {
            ImGui.UpdatePlatformWindows();
            ImGui.RenderPlatformWindowsDefault();
        }
    }


    private static void FrameEvents()
    {
        var io = ImGui.GetIO();

        if (keyEnumMap != null)
            foreach (var key in keyEnumMap)
            {
                io.KeysDown[(int) key] = Raylib.IsKeyDown(key);
            }

        var pressed = (uint)Raylib.GetCharPressed();
        while (pressed != 0)
        {
            io.AddInputCharacter(pressed);
            pressed = (uint)Raylib.GetCharPressed();
        }
    }

    public static void Begin()
    {
        ImGui.SetCurrentContext(imGuiContext);
        
        ImGuizmo.SetImGuiContext(imGuiContext);
        
        ImPlot.SetCurrentContext(imPlotContext);
        ImPlot.SetImGuiContext(imGuiContext);
        
        imnodes.EditorContextSet(imNodesContext);
        imnodes.SetImGuiContext(imGuiContext);

        NewFrame();
        FrameEvents();
        ImGui.NewFrame();
        ImGuizmo.BeginFrame();
        
        ImGuizmo.SetRect(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        
        ImGuizmo.Enable(true);
        ImGuizmo.SetGizmoSizeClipSpace(0.15f) ;
    }

    private static readonly byte[] ColorArray = new byte[sizeof(uint)];
    
    private static void TriangleVert(ImDrawVertPtr idxVert)
    {
        Unsafe.As<byte, uint>(ref ColorArray[0]) = idxVert.col;
        
        RlGl.rlColor4ub(ColorArray[0], ColorArray[1], ColorArray[2], ColorArray[3]);

        RlGl.rlTexCoord2f(idxVert.uv.X, idxVert.uv.Y);
        RlGl.rlVertex2f(idxVert.pos.X, idxVert.pos.Y);
    }

    private static void RenderTriangles(uint count, uint indexStart, ImVector<ushort> indexBuffer, ImPtrVector<ImDrawVertPtr> vertBuffer, IntPtr texturePtr)
    {
        if (count < 3)
            return;

        uint textureId = 0;
        if (texturePtr != IntPtr.Zero)
            textureId = (uint)texturePtr.ToInt32();

        RlGl.rlBegin(RlGl.RL_TRIANGLES);
        RlGl.rlSetTexture(textureId);

        for (var i = 0; i <= count - 3; i += 3)
        {
            if (RlGl.rlCheckRenderBatchLimit(3))
            {
                RlGl.rlBegin(RlGl.RL_TRIANGLES);
                RlGl.rlSetTexture(textureId);
            }

            var indexA = indexBuffer[(int)indexStart + i];
            var indexB = indexBuffer[(int)indexStart + i + 1];
            var indexC = indexBuffer[(int)indexStart + i + 2];

            var vertexA = vertBuffer[indexA];
            var vertexB = vertBuffer[indexB];
            var vertexC = vertBuffer[indexC];

            TriangleVert(vertexA);
            TriangleVert(vertexB);
            TriangleVert(vertexC);
        }
        RlGl.rlEnd();
    }

    private delegate void Callback(ImDrawListPtr list, ImDrawCmdPtr cmd);

    private static void RenderData()
    {
        RlGl.rlDrawRenderBatchActive();
        RlGl.rlDisableBackfaceCulling();
        
        var data = ImGui.GetDrawData();

        for (var l = 0; l < data.CmdListsCount; l++)
        {
            var commandList = data.CmdListsRange[l];

            for (var cmdIndex = 0; cmdIndex < commandList.CmdBuffer.Size; cmdIndex++)
            {
                var cmd = commandList.CmdBuffer[cmdIndex];

                RlGl.rlEnableScissorTest();

                var clipOff = data.DisplayPos;
                var clipScale = Vector2.One;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    clipScale = new Vector2(2, 2);

                var fbHeight = data.DisplaySize.Y * clipScale.Y;

                var clipMin = new Vector2(
                    (cmd.ClipRect.X - clipOff.X) * clipScale.X,
                    (cmd.ClipRect.Y - clipOff.Y) * clipScale.Y
                    );
                
                var clipMax = new Vector2(
                    (cmd.ClipRect.Z - clipOff.X) * clipScale.X,
                    (cmd.ClipRect.W - clipOff.Y) * clipScale.Y
                );

                RlGl.rlScissor(
                    (int)clipMin.X, 
                    (int)(fbHeight - clipMax.Y), 
                    (int)(clipMax.X - clipMin.X), 
                    (int)(clipMax.Y - clipMin.Y)
                    );


                if (cmd.UserCallback != IntPtr.Zero)
                {
                    var cb = Marshal.GetDelegateForFunctionPointer<Callback>(cmd.UserCallback);
                    cb(commandList, cmd);
                    continue;
                }

                RenderTriangles(cmd.ElemCount, cmd.IdxOffset, commandList.IdxBuffer, commandList.VtxBuffer, cmd.TextureId);

                RlGl.rlDrawRenderBatchActive();
            }
        }
        RlGl.rlSetTexture(0);
        RlGl.rlDisableScissorTest();
        RlGl.rlEnableBackfaceCulling();
    }

    public static void End()
    {
        ImGui.SetCurrentContext(imGuiContext);
        ImGui.Render();
        RenderData();
    }

    public static void Shutdown()
    {
        Raylib.UnloadTexture(fontTexture);
    }

    public static void Image(Texture image)
    {
        ImGui.Image(new IntPtr(image.id), new Vector2(image.width, image.height));
    }

    public static void ImageSize(Texture image, int width, int height)
    {
        ImGui.Image(new IntPtr(image.id), new Vector2(width, height));
    }

    public static void ImageSize(Texture image, Vector2 size)
    {
        ImGui.Image(new IntPtr(image.id), size);
    }

    private static void ImageRect(Texture image, int destWidth, int destHeight, Rectangle sourceRect)
    {
        var uv0 = new Vector2();
        var uv1 = new Vector2();

        if (sourceRect.width < 0)
        {
            uv0.X = -(sourceRect.x / image.width);
            uv1.X = uv0.X - Math.Abs(sourceRect.width) / image.width;
        }
        else
        {
            uv0.X = sourceRect.x / image.width;
            uv1.X = uv0.X + sourceRect.width / image.width;
        }

        if (sourceRect.height < 0)
        {
            uv0.Y = -(sourceRect.y / image.height);
            uv1.Y = uv0.Y - Math.Abs(sourceRect.height) / image.height;
        }
        else
        {
            uv0.Y = sourceRect.y / image.height;
            uv1.Y = uv0.Y + sourceRect.height / image.height;
        }

        ImGui.Image(new IntPtr(image.id), new Vector2(destWidth, destHeight), uv0, uv1);
    }
    
    [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
    public static void ImageRenderTextureFit(RenderTexture image, bool center = true)
    {
        var area = ImGui.GetContentRegionAvail();

        var scale = area.X / image.texture.width;

        var y = image.texture.height * scale;
        if (y > area.Y)
        {
            scale = area.Y / image.texture.height;
        }

        var sizeX = (int)(image.texture.width * scale);
        var sizeY = (int)(image.texture.height * scale);

        if (center)
        {
            ImGui.SetCursorPosX(0);
            ImGui.SetCursorPosX(area.X / 2 - sizeX / 2);
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + (area.Y / 2 - sizeY / 2));
        }

        ImageRect(image.texture, sizeX, sizeY, new Rectangle(0,0, (image.texture.width), -(image.texture.height) ));
    }
}