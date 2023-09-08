namespace JacksonVeroneze.NET.Result;

public sealed class Error
{
    public string Code { get; }

    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static implicit operator string(Error error)
        => $"{error.Code} - {error.Message}";

    public static Error None =>
        new(string.Empty, string.Empty);
}