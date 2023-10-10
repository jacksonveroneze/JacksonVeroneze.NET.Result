namespace JacksonVeroneze.NET.Result;

/// <summary>
/// Success = 200/201/204
/// Error = 400
/// Invalid = 409
/// NotFound = 404
/// ---
/// Create
/// - Success: 201
/// - Invalid: 409
/// - Error: 400
///
/// GetById
/// - Success: 200
/// - Error: 400
/// - NotFound: 404
/// </summary>
public enum ResultStatus
{
    Success,
    Error,
    Invalid,
    NotFound
}
