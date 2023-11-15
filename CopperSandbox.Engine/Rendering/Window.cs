using System.Numerics;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Rendering; 

public static class Window 
{
    public static void Init(int width, int height, string title) => Raylib.InitWindow(width, height, title);
    public static bool ShouldClose() => Raylib.WindowShouldClose();
    public static void Close() => Raylib.CloseWindow();
    public static void TakeScreenshot(string path) => Raylib.TakeScreenshot(path);
    public static bool IsReady() => Raylib.IsWindowReady();
    public static bool IsFullscreen() => Raylib.IsWindowFullscreen();
    public static bool IsHidden() => Raylib.IsWindowHidden();
    public static bool IsMinimized() => Raylib.IsWindowMinimized();
    public static bool IsMaximized() => Raylib.IsWindowMaximized();
    public static bool IsFocused() => Raylib.IsWindowFocused();
    public static bool IsResized() => Raylib.IsWindowResized();
    public static bool IsState(ConfigFlags state) => Raylib.IsWindowState(state);
    public static void SetConfigFlags(ConfigFlags state) => Raylib.SetConfigFlags(state);
    public static void SetState(ConfigFlags state) => Raylib.SetWindowState(state);
    public static void ClearState(ConfigFlags state) => Raylib.ClearWindowState(state);
    public static void ToggleFullscreen() => Raylib.ToggleFullscreen();
    public static void Maximize() => Raylib.MaximizeWindow();
    public static void Minimize() => Raylib.MinimizeWindow();
    public static void Restore() => Raylib.RestoreWindow();
    public static void SetIcon(Image image) => Raylib.SetWindowIcon(image);
    public static void SetTitle(string title) => Raylib.SetWindowTitle(title);
    public static void SetClipboardText(string path) => Raylib.SetClipboardText(path);
    public static void SetPosition(int x, int y) => Raylib.SetWindowPosition(x, y);
    public static void SetMonitor(int monitor) => Raylib.SetWindowMonitor(monitor);
    public static void SetMinSize(int width, int height) => Raylib.SetWindowMinSize(width, height);
    public static void SetSize(int width, int height) => Raylib.SetWindowSize(width, height);
    public static void SetOpacity(float opacity) => Raylib.SetWindowOpacity(opacity);
    public static void EnableEventWaiting() => Raylib.EnableEventWaiting();
    public static void DisableEventWaiting() => Raylib.DisableEventWaiting();
    public static int GetScreenWidth() => Raylib.GetScreenWidth();
    public static int GetScreenHeight() => Raylib.GetScreenHeight();
    public static int GetRenderWidth() => Raylib.GetRenderWidth();
    public static int GetRenderHeight() => Raylib.GetRenderHeight();
    public static int GetMonitorCount() => Raylib.GetMonitorCount();
    public static int GetCurrentMonitor() => Raylib.GetCurrentMonitor();
    public static Vector2 GetMonitorPosition(int monitor) => Raylib.GetMonitorPosition(monitor);
    public static int GetMonitorWidth(int monitor) => Raylib.GetMonitorWidth(monitor);
    public static int GetMonitorHeight(int monitor) => Raylib.GetMonitorHeight(monitor);
    public static int GetMonitorPhysicalWidth(int monitor) => Raylib.GetMonitorPhysicalWidth(monitor);
    public static int GetMonitorPhysicalHeight(int monitor) => Raylib.GetMonitorPhysicalHeight(monitor);
    public static int GetMonitorRefreshRate(int monitor) => Raylib.GetMonitorRefreshRate(monitor);
    public static Vector2 GetPosition() => Raylib.GetWindowPosition();
    public static Vector2 GetScaleDpi() => Raylib.GetWindowScaleDPI();
    public static string GetMonitorName(int monitor) => Raylib.GetMonitorName_(monitor);
    public static string GetClipboardText() => Raylib.GetClipboardText_();
}