namespace JacksonVeroneze.NET.Result.UnitTests;

[ExcludeFromCodeCoverage]
public class ResultTTests
{
    #region Success

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.Success)
                        + "NoValue - Should Return Success")]
    public void Success_NoValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<int?> result = Result<int?>.Success();

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

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.Success)
                        + "HasValue - Should Return Success")]
    public void Success_HasValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        const int value = 1;

        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<object> result = Result<object>.Success(value);

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

        result.Value.Should()
            .Be(value);
    }

    #endregion

    #region NotFound

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.NotFound)
                        + "NoValue - Should Return Success")]
    public void NotFound_NoValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Error error = Error.None;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<int?> result = Result<int?>.NotFound(error);

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
            .Be(ResultStatus.NotFound);

        result.Error.Should()
            .NotBeNull()
            .And.Be(error);

        result.Errors.Should()
            .BeNull();

        result.Value.Should()
            .BeNull();
    }

    #endregion

    #region Invalid

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.Invalid)
                        + "NoValue - Should Return Success")]
    public void Invalid_NoValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<int?> result = Result<int?>.Invalid();

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
            .BeNull();

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.Invalid)
                        + "NoValue - UniqueError - Should Return Success")]
    public void Invalid_NoValue_UniqueError__ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Error error = Error.None;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<int?> result = Result<int?>.Invalid(error);

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

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.Invalid)
                        + "NoValue - CollectionErrors - Should Return Success")]
    public void Invalid_NoValue_CollectionErrors__ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        ICollection<Error> errors = Enumerable.Range(1, 2)
            .Select(_ => Error.None)
            .ToArray();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<int?> result = Result<int?>.Invalid(errors);

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
            .NotBeNull()
            .And.HaveSameCount(errors);

        result.Value.Should()
            .BeNull();
    }

    #endregion

    #region Helpers-FirstFailureOrSuccess

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FirstFailureOrSuccess)
                        + "Not error - Should Return Success")]
    public void FirstFailureOrSuccess_Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        IResult<object> sucess = Result<object>.Success();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<object> result = Result<object>.FirstFailureOrSuccess(
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

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FirstFailureOrSuccess)
                        + "Has error - Should Return Success")]
    public void FirstFailureOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        IResult<object> sucess = Result<object>.Success();
        IResult<object> error = Result<object>.Invalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<object> result = Result<object>.FirstFailureOrSuccess(
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

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FailuresOrSuccess)
                        + "Not error - Should Return Success")]
    public void FailuresOrSuccess_Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        IResult<object> sucess = Result<object>.Success();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<object> result = Result<object>.FailuresOrSuccess(
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

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FailuresOrSuccess)
                        + "Has error - Should Return Success")]
    public void FailuresOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        IResult<object> sucess = Result<object>.Success();
        IResult<object> error1 = Result<object>.Invalid(Error.None);
        IResult<object> error2 = Result<object>.Invalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<object> result = Result<object>.FailuresOrSuccess(
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