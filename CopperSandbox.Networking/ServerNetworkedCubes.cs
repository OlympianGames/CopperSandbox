using System.Numerics;
using CopperEngine.Core;
using CopperEngine.Info;
using CopperEngine.NetCode;
using CopperEngine.NetCode.Messages;
using CopperEngine.Utility;
using Raylib_CsLo;

namespace CopperSandbox.Networking;

public class ServerNetworkedCubes : CopperComponent
{
    private Server? server;
    private Vector3 cubePosition;

    public override void Start()
    {
        server = CopperNetCode.Peer as Server;
    }

    public override void Update()
    {
        ModelUtil.DrawCube(cubePosition, Vector3.One * 2, ColorUtil.Red);
    }

    public override void FixedUpdate()
    {
        cubePosition = Vector3.Zero.WithY((MathF.Sin((float)Time.TotalTime) * 2) + 4);
        server?.SendMessage(new Message(cubePosition, 0));
    }
}