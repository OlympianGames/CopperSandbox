using System.Numerics;
using CopperSandbox.Engine.Core;
using CopperSandbox.Engine.Utility;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Components.Common;

public class TestComponent : CopperComponent
{
    private Model model;
    
    public override void Start()
    {
        model = ModelUtil.LoadFromMesh(MeshUtil.GenCube(2));
        model.WithGridTexture(2, 2, ColorUtil.Red, ColorUtil.Maroon);
    }
    
    public override void Update()
    {
        ModelUtil.DrawModel(model, Vector3.Zero, 1, ColorUtil.White);
    }
    
}