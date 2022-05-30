using maERP.Data.Dtos.User;

namespace maERP.Client.Contracts.Services
{
    public interface IDataService<T> where T :  class
    {
        public Task<LoginResponseDto> Login(string server, string email, string password);

        public Task<T> Request(string method, string path, object payload = null);
    }
}
