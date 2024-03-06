namespace RPLi;

interface Value
{
    public Value Add(Value addend);
    public Value Subtract(Value subtrahend);
    public Value Multiply(Value multiplier);
}
