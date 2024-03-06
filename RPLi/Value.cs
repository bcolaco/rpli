namespace RPLi;

public interface Value
{
    public Value Add(Value addend);
    public Value Subtract(Value subtrahend);
    public Value Multiply(Value multiplier);
    public Value Divide(Value divisor);
    public Value Modulus(Value divisor);
}
