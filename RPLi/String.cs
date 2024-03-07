namespace RPLi;

record String(string Value) : Value
{
    public override Value Add(Value addend)
    {
        return new String(this.Value + addend.ToString());
    }

    public override Boolean Equal(Value value)
    {
        if (value is not String stringValue)
            return Boolean.False;
        
        return Boolean.FromBoolean(stringValue.Value == this.Value);
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
