namespace RPLi;

public abstract record Value
{
    public abstract Value Add(Value addend);
    public abstract Boolean Equal(Value value);

    public T As<T>()
        where T: Value
    {
        if (this is not T typedValue)
            throw new Exception($"Expected a {typeof(T).Name} but found a {this.GetType().Name}");

        return typedValue;
    }
}
