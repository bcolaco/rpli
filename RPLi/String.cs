namespace RPLi;

record String(string Value) : Value
{
    public Value Add(Value addend)
    {
        return new String(this.Value + addend.ToString());
    }

    public Value Subtract(Value subtrahend)
    {
        throw new ArgumentException();
    }

    public Value Multiply(Value multiplier)
    {
        throw new ArgumentException();
    }

    public Value Divide(Value divisor)
    {
        throw new ArgumentException();
    }

    public Value Modulus(Value divisor)
    {
        throw new ArgumentException();
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
