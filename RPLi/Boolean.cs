namespace RPLi;

public record Boolean(bool Value) : Value
{
    public static readonly Boolean False = new(false);
    public static readonly Boolean True = new(true);

    public static Boolean FromBoolean(bool value)
    {
        if (value == true)
            return True;
        
        return False;
    }

    public override Value Add(Value addend)
    {
        throw new NotImplementedException();
    }

    public Boolean And(Boolean operand)
    {
        return FromBoolean(this.Value && operand.Value);
    }

    public Boolean Or(Boolean operand)
    {
        return FromBoolean(this.Value || operand.Value);
    }

    public Boolean Not()
    {
        return FromBoolean(!this.Value);
    }

    public override Boolean Equal(Value value)
    {
        if (value is not Boolean booleanValue)
            return False;
        
        return FromBoolean(booleanValue.Value == this.Value);
    }
}