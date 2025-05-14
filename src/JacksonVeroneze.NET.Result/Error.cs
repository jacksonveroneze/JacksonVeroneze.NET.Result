namespace JacksonVeroneze.NET.Result;

public class Error
{
    public string Code { get; }

    public string Message { get; }

    public string? Target { get; }

    private Error(
        string code, string message,
        string? target = null)
    {
        Code = code;
        Message = message;
        Target = target;
    }

    public static Error Create(
        string code, string message,
        string? target = null) =>
        new(code, message, target);

    public static Error None =>
        new(string.Empty, string.Empty);

    public static implicit operator string(Error error) =>
        $"{error.Code} - {error.Message}";
}