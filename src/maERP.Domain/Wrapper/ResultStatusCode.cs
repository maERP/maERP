namespace maERP.Domain.Wrapper;

public enum ResultStatusCode
{
    Ok = 200,
    Created = 201,
    BadRequest = 400,
    Unauthorized = 403,
    NotFound = 404,
    InternalServerError = 500,
}