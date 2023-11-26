using System.Text.Json.Serialization;

namespace Shared.Library;

public class ApiResponse<T>
{
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    [JsonIgnore] public int StatusCode { get; set; }

    //Static Factory Method
    public static ApiResponse<T> Success(T Data, int statusCode = 200)
    {
        return new ApiResponse<T> { Data = Data, StatusCode = statusCode };
    }

    public static ApiResponse<T> Success(int statusCode = 200)
    {
        return new ApiResponse<T> { Data = default, StatusCode = statusCode };
    }

    public static ApiResponse<T> Fail(List<string> errors, int statusCode = 0)
    {
        return new ApiResponse<T> { Data = default, Errors = errors, StatusCode = statusCode };
    }

    public static ApiResponse<T> Fail(string error, int statusCode = 0)
    {
        return new ApiResponse<T> { Data = default, Errors = new List<string> { error }, StatusCode = statusCode };
    }
}
