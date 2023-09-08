namespace JacksonVeroneze.NET.Result;

public interface IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IEnumerable<Error>? Errors { get; set; }
}