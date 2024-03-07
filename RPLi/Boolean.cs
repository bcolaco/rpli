namespace RPLi;

public record Boolean(bool Value) : Value
{
    public Value Add(Value addend)
    {
        throw new NotImplementedException();
    }

    public Boolean And(Boolean operand)
    {
        return new Boolean(this.Value && operand.Value);
    }

    public Boolean Or(Boolean operand)
    {
        return new Boolean(this.Value || operand.Value);
    }

    public Boolean Not()
    {
        return new Boolean(!this.Value);
    }
}