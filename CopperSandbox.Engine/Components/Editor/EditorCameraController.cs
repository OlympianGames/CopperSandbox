using System.Numerics;
using CopperSandbox.Engine.Components.Common;
using CopperSandbox.Engine.Core;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Components.Editor;

// TODO: Change speed with scroll wheel & display a popup with current speed
internal class EditorCameraController : CameraController
{
    private static EditorCameraController? Instance;

    protected override Camera3D Camera
    {
        get => Core.CopperEngine.EditorCamera;
        set => Core.CopperEngine.EditorCamera = value;
    }
    
    protected override bool CanMove => Core.CopperEngine.EditorActive && CopperEditor.GameViewFocused;

    internal static Vector3 Position => Instance!.Camera.position;
    internal static Vector3 Direction => Instance!.direction;
    internal static Vector3 Forward => Instance!.cameraFront;
    internal static Vector3 Right => Instance!.cameraRight;
    internal static Vector3 Up => Instance!.cameraUp;
    internal static float Pitch => Instance!.pitch;
    internal static float Yaw => Instance!.yaw;

    public override void Start()
    {
        Instance = this;
    }
}