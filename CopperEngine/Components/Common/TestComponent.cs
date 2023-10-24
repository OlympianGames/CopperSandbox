using System.Numerics;
using CopperEngine.Core;
using CopperEngine.Utility;
using Raylib_CsLo;

namespace CopperEngine.Components.Common;

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