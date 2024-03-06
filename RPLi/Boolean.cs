namespace RPLi;

public record Boolean(bool Value) : Value
{
    public Value Add(Value addend)
    {
        throw new NotImplementedException();
    }

    public Value Divide(Value divisor)
    {
        throw new NotImplementedException();
    }

    public Value Modulus(Value divisor)
    {
        throw new NotImplementedException();
    }

    public Value Multiply(Value multiplier)
    {
        throw new NotImplementedException();
    }

    public Value Subtract(Value subtrahend)
    {
        throw new NotImplementedException();
    }
}