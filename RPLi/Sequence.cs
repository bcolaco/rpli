namespace RPLi;

public record Sequence(Value[] Values) : Value
{
    public override Value Add(Value addend)
    {
        throw new NotImplementedException();
    }

    public override Boolean Equal(Value value)
    {
        if (value is not Sequence sequenceValue)
            return Boolean.False;

        if (sequenceValue.Values.Length != this.Values.Length)
            return Boolean.False;

        for (var i = 0; i < this.Values.Length; i++)
        {
            if (this.Values[i].Equal(sequenceValue.Values[i]).Not().Value)
                return Boolean.False;
        }

        return Boolean.True;
    }
}
