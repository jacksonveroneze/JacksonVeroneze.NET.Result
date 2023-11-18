namespace JacksonVeroneze.NET.Result;

public class Result : IResult
{
    public bool IsSuccess => Status is ResultStatus.Success;

    public bool IsFailure => !IsSuccess;

    public ResultStatus Status { get; }

    public Error? Error { get; set; }

    public IEnumerable<Error>? Errors { get; set; }

    public IEnumerable<IGrouping<string, Error>>? ErrorsGroup =>
        Errors?.GroupBy(error => error.Code);

    #region ctor

    protected Result()
    {
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

    protected Result(ResultStatus status, Error error)
        : this(status)
    {
        Error = error;
    }

    protected Result(ResultStatus status, IEnumerable<Error> errors)
        : this(status)
    {
        Errors = errors;
    }

    #endregion

    #region success

    public static IResult Success()
    {
        return new Result(ResultStatus.Success);
    }

    #endregion

    #region invalid

    public static IResult Invalid(Error error)
    {
        return new Result(ResultStatus.Invalid, error);
    }

    public static IResult Invalid(IEnumerable<Error> error)
    {
        return new Result(ResultStatus.Invalid, error);
    }

    #endregion

    #region helpers

    public static IResult FirstFailureOrSuccess(
        params IResult[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        IResult? result = results.FirstOrDefault(
            item => item.IsFailure);

        return result ?? Success();
    }

    public static IResult FailuresOrSuccess(
        params IResult[] results)
    {
        ArgumentNullException.ThrowIfNull(nameof(results));

        ICollection<Error> failures = results
            .Where(item => item.IsFailure)
            .Select(item => item.Error)
            .ToArray()!;

        return failures.Any() ? Invalid(failures) : Success();
    }

    #endregion
}