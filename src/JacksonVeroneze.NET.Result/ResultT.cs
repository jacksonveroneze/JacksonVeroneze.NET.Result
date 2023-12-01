namespace JacksonVeroneze.NET.Result;

public class Result<TValue> : Result
{
    public TValue? Value { get; }

    #region ctor

    private Result(ResultType type)
        : base(type)
    {
    }

    private Result(ResultType type, TValue value)
        : base(type)
    {
        Value = value;
    }

    private Result(ResultType type, Error error)
        : base(type, error)
    {
    }

    private Result(ResultType type, IEnumerable<Error> errors)
        : base(type, errors)
    {
    }

    #endregion

    #region success

    public static new Result<TValue> WithSuccess()
    {
        return new Result<TValue>(ResultType.Success);
    }

    public static Result<TValue> WithSuccess(TValue value)
    {
        return new Result<TValue>(ResultType.Success, value);
    }

    #endregion

    #region notFound

    public static Result<TValue> FromNotFound(Error error)
    {
        return new Result<TValue>(ResultType.NotFound, error);
    }

    #endregion

    #region invalid

    public static Result<TValue> FromInvalid()
    {
        return new Result<TValue>(ResultType.Invalid);
    }

    public static new Result<TValue> FromInvalid(Error error)
    {
        return new Result<TValue>(ResultType.Invalid, error);
    }

    public static new Result<TValue> FromInvalid(IEnumerable<Error> errors)
    {
        return new Result<TValue>(ResultType.Invalid, errors);
    }

    #endregion

    #region error

    public static Result<TValue> WithError()
    {
        return new Result<TValue>(ResultType.Error);
    }

    public static new Result<TValue> WithError(Error error)
    {
        return new Result<TValue>(ResultType.Error, error);
    }

    public static Result<TValue> WithError(IEnumerable<Error> errors)
    {
        return new Result<TValue>(ResultType.Error, errors);
    }

    #endregion

    #region helpers

    public static Result<TValue> FirstFailureOrSuccess(
        params Result<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        Result<TValue>? failure = results.FirstOrDefault(
            item => item.IsFailure);

        return failure ?? WithSuccess();
    }

    public static Result<TValue> FailuresOrSuccess(
        params Result<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        IList<Error> failures = results
            .Where(item => item.IsFailure)
            .Select(item => item.Error)
            .ToArray()!;

        return failures.Any() ? FromInvalid(failures) : WithSuccess();
    }

    #endregion
}