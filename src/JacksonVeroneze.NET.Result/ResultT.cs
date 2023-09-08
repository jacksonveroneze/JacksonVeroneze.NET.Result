namespace JacksonVeroneze.NET.Result;

public class Result<TValue> : Result, IResult<TValue>
{
    public TValue? Value { get; }

    #region ctor

    protected Result()
    {
    }

    protected Result(ResultStatus status)
        : base(status)
    {
    }

    protected Result(ResultStatus status, TValue value)
        : base(status)
    {
        Value = value;
    }

    protected Result(ResultStatus status, Error error)
        : base(status, error)
    {
    }

    protected Result(ResultStatus status, IEnumerable<Error> errors)
        : base(status, errors)
    {
    }

    #endregion

    #region success

    public static new IResult<TValue> Success()
    {
        return new Result<TValue>(ResultStatus.Success);
    }

    public static IResult<TValue> Success(TValue value)
    {
        return new Result<TValue>(ResultStatus.Success, value);
    }

    #endregion

    #region notFound

    public static IResult<TValue> NotFound(Error error)
    {
        return new Result<TValue>(ResultStatus.NotFound, error);
    }

    #endregion

    #region invalid

    public static IResult<TValue> Invalid()
    {
        return new Result<TValue>(ResultStatus.Invalid);
    }

    public static new IResult<TValue> Invalid(Error error)
    {
        return new Result<TValue>(ResultStatus.Invalid, error);
    }

    public static new IResult<TValue> Invalid(IEnumerable<Error> errors)
    {
        return new Result<TValue>(ResultStatus.Invalid, errors);
    }

    #endregion

    #region helpers

    public static IResult<TValue> FirstFailureOrSuccess(
        params IResult<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        IResult<TValue>? failure = results.FirstOrDefault(
            item => item.IsFailure);

        return failure ?? Success();
    }

    public static IResult<TValue> FailuresOrSuccess(
        params IResult<TValue>[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        IList<Error> failures = results
            .Where(item => item.IsFailure)
            .Select(item => item.Error)
            .ToArray()!;

        return failures.Any() ? Invalid(failures) : Success();
    }

    #endregion
}