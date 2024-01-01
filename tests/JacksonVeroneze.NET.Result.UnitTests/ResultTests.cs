namespace JacksonVeroneze.NET.Result.UnitTests;

[ExcludeFromCodeCoverage]
public class ResultTests
{
    #region Success

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.WithSuccess)
                        + "Should Return Success")]
    public void Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result result = Result.WithSuccess();

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.IsFailure.Should()
            .BeFalse();

        result.Type.Should()
            .Be(ResultType.Success);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .BeNull();
    }

    #endregion

    #region Invalid

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FromInvalid)
                        + "UniqueError - Should Return Success")]
    public void Invalid_UniqueError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Error error = Error.None;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FromInvalid(error);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.IsFailure.Should()
            .BeTrue();

        result.Type.Should()
            .Be(ResultType.Invalid);

        result.Error.Should()
            .NotBeNull()
            .And.Be(error);

        result.Errors.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FromInvalid)
                        + "MultipleErrors - Should Return Success")]
    public void Invalid_MultipleErrors_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        IList<Error> errors = Enumerable.Range(1, 2)
            .Select(item => new Error($"Code_{item}",
                $"Message_{item}"))
            .ToArray();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FromInvalid(errors);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.IsFailure.Should()
            .BeTrue();

        result.Type.Should()
            .Be(ResultType.Invalid);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveSameCount(errors)
            .And.SatisfyRespectively(
                first => first.Should()
                    .Be(errors.First()),
                second => second.Should()
                    .Be(errors.Last()));
    }

    #endregion

    #region Helpers-FirstFailureOrSuccess

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FirstFailureOrSuccess)
                        + "Not error - Should Return Success")]
    public void FirstFailureOrSuccess_Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result sucess = Result.WithSuccess();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FirstFailureOrSuccess(
            sucess);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.IsFailure.Should()
            .BeFalse();

        result.Type.Should()
            .Be(ResultType.Success);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FirstFailureOrSuccess)
                        + "Has error - Should Return Success")]
    public void FirstFailureOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result sucess = Result.WithSuccess();
        Result error = Result.FromInvalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FirstFailureOrSuccess(
            sucess, error);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull()
            .And.Be(error);
    }

    #endregion

    #region Helpers-FailuresOrSuccess

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FailuresOrSuccess)
                        + "Not error - Should Return Success")]
    public void FailuresOrSuccess_Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result sucess = Result.WithSuccess();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FailuresOrSuccess(
            sucess);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.IsFailure.Should()
            .BeFalse();

        result.Type.Should()
            .Be(ResultType.Success);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.FailuresOrSuccess)
                        + "Has error - Should Return Success")]
    public void FailuresOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result sucess = Result.WithSuccess();
        Result error1 = Result.FromInvalid(Error.None);
        Result error2 = Result.FromInvalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result result = Result.FailuresOrSuccess(
            sucess, error1, error2);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.IsFailure.Should()
            .BeTrue();

        result.Type.Should()
            .Be(ResultType.Invalid);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(2);
    }

    #endregion
}