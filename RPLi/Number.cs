namespace RPLi;

record Number(double Value) : Value
{
    public Value Add(Value addend)
    {
        if (addend is Number number) 
            return new Number(this.Value + number.Value);
        throw new ArgumentException();
    }

    public Value Subtract(Value subtrahend)
    {
        if (subtrahend is Number number) 
            return new Number(this.Value - number.Value);
        throw new ArgumentException();
    }

    public Value Multiply(Value multiplier)
    {
        if (multiplier is Number number) 
            return new Number(this.Value * number.Value);
        throw new ArgumentException();
    }

    public Value Divide(Value divisor)
    {
        if (divisor is Number number) 
            return new Number(this.Value / number.Value);
        throw new ArgumentException();
    }

    public Value Modulus(Value divisor)
    {
        if (divisor is Number number) 
            return new Number(this.Value % number.Value);
        throw new ArgumentException();
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
