namespace JacksonVeroneze.NET.Result;

public class Error(string code, string message)
{
    public string Code { get; } = code;

    public string Message { get; } = message;

    public static implicit operator string(Error error)
        => $"{error.Code} - {error.Message}";

    public static Error None =>
        new(string.Empty, string.Empty);
}