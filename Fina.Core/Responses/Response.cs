using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<TData>
{
    private int _code = Configuration.DefaultStatusCode;

    public TData? Data { get; set; }

    public string? Message { get; set; }

    [JsonConstructor]   // Informa ao Asp.Net qual o construtor padrao a ser chamado.
    public Response()
    {
        _code = Configuration.DefaultStatusCode;
    }

    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null)
    {
        _code = code;
        Data = data;
        Message = message;
    }

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;
}