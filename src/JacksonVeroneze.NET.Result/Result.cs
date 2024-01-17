namespace JacksonVeroneze.NET.Result;

public class Result
{
    public bool IsSuccess => Type is ResultType.Success;

    public bool IsFailure => !IsSuccess;

    public ResultType Type { get; }

    public Error? Error { get; }

    public IEnumerable<Error>? Errors { get; set; }

    public IEnumerable<IGrouping<string, Error>>? ErrorsGroup =>
        Errors?.GroupBy(error => error.Code);

    #region ctor

    protected Result(ResultType type)
    {
        Type = type;
    }

    protected Result(ResultType type, Error error)
        : this(type)
    {
        Error = error;
    }

    protected Result(ResultType type, IEnumerable<Error> errors)
        : this(type)
    {
        Errors = errors;
    }

    #endregion

    #region success

    public static Result WithSuccess()
    {
        return new Result(ResultType.Success);
    }

    #endregion

    #region notFound

    public static Result FromNotFound(Error error)
    {
        return new Result(ResultType.NotFound, error);
    }

    #endregion

    #region invalid

    public static Result FromInvalid(Error error)
    {
        return new Result(ResultType.Invalid, error);
    }

    public static Result FromInvalid(IEnumerable<Error> error)
    {
        return new Result(ResultType.Invalid, error);
    }

    #endregion

    #region error

    public static Result WithError(Error error)
    {
        return new Result(ResultType.Error, error);
    }

    #endregion

    #region helpers

    public static Result FirstFailureOrSuccess(
        params Result[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        Result? result = results.FirstOrDefault(
            item => item.IsFailure);

        return result ?? WithSuccess();
    }

    public static Result FailuresOrSuccess(
        params Result[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        ICollection<Error> failures = results
            .Where(item => item.IsFailure)
            .Select(item => item.Error)
            .ToArray()!;

        return failures.Any() ? FromInvalid(failures) : WithSuccess();
    }

    #endregion
}