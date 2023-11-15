using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Info;
using CopperSandbox.Engine.Utility;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Components.Common;

// TODO: Change speed with scroll wheel & display a popup with current speed
public class CameraController : CopperComponent
{
    protected virtual Camera3D Camera
    {
        get => Core.CopperEngine.GameCamera;
        set => Core.CopperEngine.GameCamera = value;
    }

    protected virtual bool CanMove => !Core.CopperEngine.EditorActive;

    private bool fastMove = false;
    private const float FastMoveModifier = 3;
    private float moveSpeed = 0.15f;

    protected Vector3 direction;
    protected Vector3 cameraFront;
    protected Vector3 cameraRight;
    protected Vector3 cameraUp;
    protected float pitch;
    protected float yaw;

    public override void Start()
    {
        var camera = Camera;
        Raylib.SetCameraMode(Camera, CameraMode.CAMERA_CUSTOM);

        pitch = -0.6f;
        yaw = -2.45f;
        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.target = Vector3.Add(camera.position, cameraFront);
        Camera = camera;
    }

    public override void UiUpdate()
    {
        
    }

    public override void Update()
    {
        if (!CanMove)
            return;

        var camera = Camera;
        
        MoveInput(ref camera);
        LookInput(ref camera);
        
        Camera = camera;
    }

    private void SpeedControls()
    {
        var targetMoveSpeed = 0f;
        
        var mouseWheelMovement = Input.GetMouseWheelMove();
        if (mouseWheelMovement != 0)
            targetMoveSpeed += mouseWheelMovement / 25;

        if (targetMoveSpeed < 0.15f)
            targetMoveSpeed = 0.15f;
        if (targetMoveSpeed > 1)
            targetMoveSpeed = 1;

        fastMove = Input.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);
        if (fastMove)
            targetMoveSpeed *= FastMoveModifier;

        moveSpeed = targetMoveSpeed;
    }

    private void MoveInput(ref Camera3D camera)
    {
        if (Input.IsKeyDown(KeyboardKey.KEY_W))
        {
            camera.position = Vector3.Add(camera.position, Utils.Scale(cameraFront, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_S))
        {
            camera.position = Vector3.Subtract(camera.position, Utils.Scale(cameraFront, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_A))
        {
            camera.position = Vector3.Subtract(camera.position,
                Utils.Scale(Vector3.Cross(cameraFront, cameraUp), moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_D))
        {
            camera.position = Vector3.Add(camera.position,
                Utils.Scale(Vector3.Cross(cameraFront, cameraUp), moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            camera.position = Vector3.Add(camera.position, Utils.Scale(cameraUp, moveSpeed));
        }

        if (Input.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
        {
            camera.position = Vector3.Subtract(camera.position, Utils.Scale(cameraUp, moveSpeed));
        }
    }

    private void LookInput(ref Camera3D camera)
    {
        var deltaTime = Time.DeltaTime;
        
        if (Input.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
        {
            Input.DisableCursor();
            var mouseDelta = Input.GetMouseDelta();
            yaw += (mouseDelta.X * 0.75f) * deltaTime;
            pitch += -(mouseDelta.Y * 0.75f) * deltaTime;

            if (pitch > 1.5)
                pitch = 1.5f;
            else if (pitch < -1.5)
                pitch = -1.5f;
        }
        else
        {
            Input.EnableCursor();
        }

        direction.X = MathF.Cos(yaw) * MathF.Cos(pitch);
        direction.Y = MathF.Sin(pitch);
        direction.Z = MathF.Sin(yaw) * MathF.Cos(pitch);
        cameraFront = Vector3.Normalize(direction);
        cameraRight = Vector3.Normalize(Vector3.Cross(camera.up, cameraFront));
        cameraUp = Vector3.Cross(direction, cameraRight);

        camera.target = Vector3.Add(camera.position, cameraFront);
    }
}