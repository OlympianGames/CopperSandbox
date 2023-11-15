using Raylib_CsLo;

namespace CopperSandbox.Engine.Rendering; 

public static class Graphics
{
    public static void ClearBackground(Color color) => Raylib.ClearBackground(color);
    public static void BeginDrawing() => Raylib.BeginDrawing();
    public static void EndDrawing() => Raylib.EndDrawing();
    public static void BeginTextureMode(RenderTexture target) => Raylib.BeginTextureMode(target);
    public static void EndTextureMode() => Raylib.EndTextureMode();
    public static void BeginShaderMode(Shader shader) => Raylib.BeginShaderMode(shader);
    public static void EndShaderMode() => Raylib.EndShaderMode();
    public static void BeginBlendMode(BlendMode mode) => Raylib.BeginBlendMode(mode);
    public static void EndBlendMode() => Raylib.EndBlendMode();
    public static void BeginScissorMode(int x, int y, int width, int height) => Raylib.BeginScissorMode(x, y, width, height);
    public static void EndScissorMode() => Raylib.EndScissorMode();
    public static void BeginCamera(Camera2D camera) => Raylib.BeginMode2D(camera);
    public static void BeginCamera(Camera3D camera) => Raylib.BeginMode3D(camera);
    public static void End2D() => Raylib.EndMode2D();
    public static void End3D() => Raylib.EndMode3D();
}