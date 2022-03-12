namespace Todolist.Api.Bases.ExceptionHandling.Filters;

public class ErrorCode
{
    public const string NoErrorCode = "NoErrorCode";
    public const string ErrorApiInvalideModelState = "ErrorApiInvalideModelState";

    private static readonly Dictionary<string, ErrorCode> ErrorCodes = new()
    {
        { NoErrorCode, new ErrorCode(NoErrorCode, "This error is not repertoried") },
        { ErrorApiInvalideModelState, new ErrorCode(ErrorApiInvalideModelState, "One or more validation errors occurred") }
    };

    public ErrorCode(string code)
    {
        Code = code;
    }

    public ErrorCode(string code, string label)
        : this(code)
    {
        Label = label;
    }

    public string Code { get; }
    public string? Label { get; }
    public static ErrorCode Get(string code) => ErrorCodes.GetValueOrDefault(code, ErrorCodes[NoErrorCode]);

    public static ErrorCode[] GetValues() => ErrorCodes.Values.ToArray();

    public static void AddErrorCodes(IEnumerable<ErrorCode> errorCodes)
    {
        foreach (var errorCode in errorCodes)
        {
            ErrorCodes.TryAdd(errorCode.Code, errorCode);
        }
    }

    public override string ToString() => Label ?? string.Empty;
}