using System.Collections.Immutable;

namespace JacksonVeroneze.NET.Result;

public class Result
{
    public bool IsSuccess => Type is ResultType.Success;

    public bool IsFailure => !IsSuccess;

    public ResultType Type { get; }

    public IReadOnlyCollection<Error> Errors { get; } = [];

    public IEnumerable<IGrouping<string, Error>> ErrorsGroup =>
        Errors.GroupBy(error => error.Target ?? string.Empty);

    #region ctor

    protected Result(ResultType type)
    {
        Type = type;
    }

    protected Result(ResultType type, Error error)
        : this(type)
    {
        Errors = [error];
    }

    protected Result(ResultType type, IEnumerable<Error> errors)
        : this(type)
    {
        Errors = errors.ToImmutableArray();
    }

    #endregion

    #region success

    public static Result WithSuccess()
    {
        return new Result(ResultType.Success);
    }

    #endregion

    #region invalid

    public static Result FromInvalid(Error error)
    {
        return new Result(ResultType.Invalid, error);
    }

    public static Result FromInvalid(IEnumerable<Error> errors)
    {
        return new Result(ResultType.Invalid, errors);
    }

    #endregion

    #region conflict

    public static Result FromConflict(Error error)
    {
        return new Result(ResultType.Conflict, error);
    }

    public static Result FromConflict(IEnumerable<Error> errors)
    {
        return new Result(ResultType.Conflict, errors);
    }

    #endregion

    #region ruleViolation

    public static Result FromRuleViolation(Error error)
    {
        return new Result(ResultType.RuleViolation, error);
    }

    public static Result FromRuleViolation(IEnumerable<Error> error)
    {
        return new Result(ResultType.RuleViolation, error);
    }

    #endregion

    #region notFound

    public static Result FromNotFound(Error error)
    {
        return new Result(ResultType.NotFound, error);
    }

    #endregion

    #region error

    public static Result WithError(Error error)
    {
        return new Result(ResultType.Error, error);
    }

    #endregion

    #region helpers

    public bool HasErrors => Errors.Count > 0;

    public Error? FirstError => Errors.FirstOrDefault();

    public static Result FirstFailureOrSuccess(
        params Result[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        Result? result = results.FirstOrDefault(item => item.IsFailure);

        return result ?? WithSuccess();
    }

    public static Result FailuresOrSuccess(
        params Result[] results)
    {
        ArgumentNullException.ThrowIfNull(results);

        ICollection<Error> failures = results
            .Where(item => item.IsFailure)
            .SelectMany(item => item.Errors)
            .ToArray();

        return failures.Any() ? FromInvalid(failures) : WithSuccess();
    }

    public bool HasErrorCode(string code) =>
        Errors.Any(error => error.Code.Equals(
            code, StringComparison.OrdinalIgnoreCase));

    public bool HasErrorForTarget(string target) =>
        Errors.Any(error => error.Target?.Equals(
            target, StringComparison.OrdinalIgnoreCase) ?? false);

    #endregion
}