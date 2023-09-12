namespace JacksonVeroneze.NET.Result.UnitTests;

[ExcludeFromCodeCoverage]
public class ErrorTests
{
    #region Success

    [Fact(DisplayName = nameof(Error)
                        + "Should Return Success")]
    public void Success_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        const string code = "code";
        const string message = "message";

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Error error = new(code, message);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        error.Should()
            .NotBeNull();

        string errorString = error;

        errorString.Should()
            .NotBeNull()
            .And.Be($"{code} - {message}");
    }

    #endregion
}