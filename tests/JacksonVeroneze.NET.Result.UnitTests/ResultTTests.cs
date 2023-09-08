namespace JacksonVeroneze.NET.Result.UnitTests;

public class ResultTTests
{
    #region Success

    [Fact(DisplayName = nameof(Result<int>)
                        + nameof(Result<int>.Success)
                        + "Should Return Success")]
    public void Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        const int value = 1;

        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<int> result = Result<int>.Success(value);

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
}