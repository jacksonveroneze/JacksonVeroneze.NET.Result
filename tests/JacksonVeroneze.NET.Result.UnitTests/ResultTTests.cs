namespace JacksonVeroneze.NET.Result.UnitTests;

[ExcludeFromCodeCoverage]
public class ResultTTests
{
    #region Success

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.WithSuccess)
                        + "NoValue - Should Return Success")]
    public void Success_NoValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<int?> result = Result<int?>.WithSuccess();

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

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.WithSuccess)
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
        Result<object> result = Result<object>.WithSuccess(value);

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

        result.Value.Should()
            .Be(value);
    }

    #endregion

    #region NotFound

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FromNotFound)
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
        Result<int?> result = Result<int?>.FromNotFound(error);

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
            .Be(ResultType.NotFound);

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
                        + nameof(Result<object>.FromInvalid)
                        + "NoValue - Should Return Success")]
    public void Invalid_NoValue_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<int?> result = Result<int?>.FromInvalid();

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
            .BeNull();

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FromInvalid)
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
        Result<int?> result = Result<int?>.FromInvalid(error);

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

        result.Value.Should()
            .BeNull();
    }

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FromInvalid)
                        + "NoValue - CollectionErrors - Should Return Success")]
    public void Invalid_NoValue_CollectionErrors__ShouldReturnSuccess()
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
        Result<int?> result = Result<int?>.FromInvalid(errors);

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
        Result<object> sucess = Result<object>.WithSuccess();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result<object> result = Result<object>.FirstFailureOrSuccess(
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

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FirstFailureOrSuccess)
                        + "Has error - Should Return Success")]
    public void FirstFailureOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result<object> sucess = Result<object>.WithSuccess();
        Result<object> error = Result<object>.FromInvalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result<object> result = Result<object>.FirstFailureOrSuccess(
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
        Result<object> sucess = Result<object>.WithSuccess();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result<object> result = Result<object>.FailuresOrSuccess(
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

    [Fact(DisplayName = nameof(Result<object>)
                        + nameof(Result<object>.FailuresOrSuccess)
                        + "Has error - Should Return Success")]
    public void FailuresOrSuccess_HasError_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Result<object> sucess = Result<object>.WithSuccess();
        Result<object> error1 = Result<object>.FromInvalid(Error.None);
        Result<object> error2 = Result<object>.FromInvalid(Error.None);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Result<object> result = Result<object>.FailuresOrSuccess(
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