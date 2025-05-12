namespace Shark_Tech.API;

public class ApiException : GeneralResult
{
    public ApiException(int statusCode, string message = null,string details = null) : base(statusCode, message)
    {
        Details = details;
    }
    public string Details { get; set; }
}