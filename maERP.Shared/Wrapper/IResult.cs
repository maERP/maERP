namespace maERP.Shared.Wrapper;

public interface IResult
{
    List<string> Messages { get; set; }

    bool Succeeded { get; set; }
    ResultStatusCode StatusCode { get; set; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}