using System.Numerics;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Info;

public class FourAxisInput
{
    private readonly KeyboardKey positiveVertical = KeyboardKey.KEY_W;
    private readonly KeyboardKey negativeVertical = KeyboardKey.KEY_S;
    private readonly KeyboardKey positiveHorizontal = KeyboardKey.KEY_A;
    private readonly KeyboardKey negativeHorizontal = KeyboardKey.KEY_D;

    public FourAxisInput() {}
        
    public FourAxisInput(KeyboardKey positiveVertical, KeyboardKey negativeVertical, KeyboardKey positiveHorizontal, KeyboardKey negativeHorizontal)
    {
        this.positiveVertical = positiveVertical;
        this.negativeVertical = negativeVertical;
        this.positiveHorizontal = positiveHorizontal;
        this.negativeHorizontal = negativeHorizontal;
    }

    public Vector2 GetInput()
    {
        var input = new Vector2();

        var posVerDown = Input.IsKeyDown(positiveVertical);
        var negVerDown = Input.IsKeyDown(negativeVertical);

        input.X = posVerDown switch
        {
            true when negVerDown => 0,
            true when !negVerDown => 1,
            false when negVerDown => -1,
            _ => 0
        };

        var posHorDown = Input.IsKeyDown(positiveHorizontal);
        var negHorDown = Input.IsKeyDown(negativeHorizontal);

        input.Y = posHorDown switch
        {
            true when negHorDown => 0,
            true when !negHorDown => 1,
            false when negHorDown => -1,
            _ => 0
        };

        input.Y = (int)MathF.Round(-input.Y);
            
        return Vector2.Normalize(input);
    }

    public static implicit operator Vector2(FourAxisInput input)
    {
        return input.GetInput();
    }
}