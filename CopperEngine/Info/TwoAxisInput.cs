using Raylib_CsLo;

namespace CopperEngine.Info;

public class TwoAxisInput
{
    private readonly KeyboardKey positiveInput = KeyboardKey.KEY_W;
    private readonly KeyboardKey negativeInput = KeyboardKey.KEY_S;
    
    public TwoAxisInput() {}

    public TwoAxisInput(KeyboardKey positiveInput, KeyboardKey negativeInput)
    {
        this.positiveInput = positiveInput;
        this.negativeInput = negativeInput;
    }

    public float GetValue()
    {
        float input = 0;
        var posVerDown = Input.IsKeyDown(positiveInput);
        var negVerDown = Input.IsKeyDown(negativeInput);

        input = posVerDown switch
        {
            true when negVerDown => 0,
            true when !negVerDown => 1,
            false when negVerDown => -1,
            _ => 0
        };

        return input;
    }

    public static implicit operator float(TwoAxisInput input)
    {
        return input.GetValue();
    }
}