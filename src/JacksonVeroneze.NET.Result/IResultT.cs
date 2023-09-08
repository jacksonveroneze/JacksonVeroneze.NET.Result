namespace JacksonVeroneze.NET.Result;

public interface IResult<out TValue> : IResult
{
    public TValue? Value { get; }
}