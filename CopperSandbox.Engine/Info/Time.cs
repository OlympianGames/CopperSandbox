using Raylib_CsLo;

namespace CopperSandbox.Engine.Info;

public static class Time
{
    public static float DeltaTime => Raylib.GetFrameTime();
    public static double TotalTime => Raylib.GetTime();
    public static void Wait(double seconds) => Raylib.WaitTime(seconds);
}