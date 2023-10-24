using System.Numerics;
using CopperEngine.Core;
using CopperEngine.NetCode;
using CopperEngine.NetCode.Messages;
using CopperEngine.Utility;

namespace CopperSandbox.Networking;

public class ClientNetworkedCubes : CopperComponent
{
    private Client? client;
    private static Vector3 cubePosition;

    public override void Start()
    {
        client = CopperNetCode.Peer as Client;
    }

    [MessageHandler(0)]
    public static void HandleCubePositionMessage(Message message)
    {
        cubePosition = (Vector3)message.Data;
    }

    public override void Update()
    {
        ModelUtil.DrawCube(cubePosition, Vector3.One * 2, ColorUtil.Red);
    }
}