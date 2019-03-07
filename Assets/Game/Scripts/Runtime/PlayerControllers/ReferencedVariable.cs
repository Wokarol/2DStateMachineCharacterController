internal class ReferencedVariable<T>
{
    public ReferencedVariable(T value) {
        Value = value;
    }
    public T Value { get; set; }

    public static implicit operator T(ReferencedVariable<T> me) {
        return me.Value;
    }
}