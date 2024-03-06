using System.Diagnostics.CodeAnalysis;

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

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
