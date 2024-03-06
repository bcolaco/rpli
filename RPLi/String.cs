namespace RPLi;

record String(string Value) : Value
{
    public Value Add(Value addend)
    {
        return new String(this.Value + addend.ToString());
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}
