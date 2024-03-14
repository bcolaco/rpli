namespace RPLi;

public record Hash(IDictionary<string, Value> Values) : Value
{
    public Hash(): this(new Dictionary<string, Value>())
    {
    }

    public override Value Add(Value addend)
    {
        throw new NotImplementedException();
    }

    public override Boolean Equal(Value value)
    {
        if (value is not Hash hashValue)
            return Boolean.False;
        
        if (hashValue.Values.Count != this.Values.Count)
            return Boolean.False;

        foreach (var key in hashValue.Values.Keys)
        {
            if (!Values.TryGetValue(key, out Value? keyValue))
                return Boolean.False;

            if (!keyValue.Equal(Values[key]).Value)
                return Boolean.False;
        }
        
        return Boolean.True;
    }
}
