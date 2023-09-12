namespace JacksonVeroneze.NET.Result.UnitTests;

[ExcludeFromCodeCoverage]
public class ResultTests
{
    #region Success

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.Success)
                        + "Should Return Success")]
    public void Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult result = Result.Success();

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.IsFailure.Should()
            .BeFalse();

        result.Status.Should()
            .Be(ResultStatus.Success);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .BeNull();
    }

    #endregion

    #region Invalid

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.Invalid)
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
        IResult result = Result.Invalid(error);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.IsFailure.Should()
            .BeTrue();

        result.Status.Should()
            .Be(ResultStatus.Invalid);

        result.Error.Should()
            .NotBeNull()
            .And.Be(error);

        result.Errors.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result)
                        + nameof(Result.Invalid)
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
        IResult result = Result.Invalid(errors);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.IsFailure.Should()
            .BeTrue();

        result.Status.Should()
            .Be(ResultStatus.Invalid);

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
        IResult sucess = Result.Success();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = Result.FirstFailureOrSuccess(
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

        result.Status.Should()
            .Be(ResultStatus.Success);

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
        IResult sucess = Result.Success();
        IResult error = Result.Invalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = Result.FirstFailureOrSuccess(
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
        IResult sucess = Result.Success();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = Result.FailuresOrSuccess(
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

        result.Status.Should()
            .Be(ResultStatus.Success);

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
        IResult sucess = Result.Success();
        IResult error1 = Result.Invalid(Error.None);
        IResult error2 = Result.Invalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = Result.FailuresOrSuccess(
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

        result.Status.Should()
            .Be(ResultStatus.Invalid);

        result.Error.Should()
            .BeNull();

        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(2);
    }

    #endregion
}