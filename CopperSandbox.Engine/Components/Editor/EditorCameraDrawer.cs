using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Utility;

namespace CopperSandbox.Engine.Components.Editor;

internal class EditorCameraDrawer : CopperComponent
{
    public override void Update()
    {
        if(!Core.CopperEngine.DebugVisuals)
            return;
            
        if (Core.CopperEngine.EditorActive)
        {
            DrawPositionSphere(Core.CopperEngine.GameCamera.position);
            DrawPositionSphere(Core.CopperEngine.GameCamera.target);
        }
        else
        {
            DrawPositionSphere(Core.CopperEngine.EditorCamera.position);
            DrawPositionSphere(Core.CopperEngine.EditorCamera.target);
        }
    }
    
    private void DrawPositionSphere(Vector3 position)
    {
        ModelUtil.DrawSphere(position, 0.15f, 8, 8, ColorUtil.Red);
        ModelUtil.DrawSphereWires(position, 0.15f, 8, 8, ColorUtil.Maroon);
    }
}