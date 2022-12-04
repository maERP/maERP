using System.Threading.Tasks;
using maERP.Shared.Dtos.User;

namespace maERP.Shared.Contracts
{
    public interface IDataService<T> where T : class
    {
        public Task<LoginResponseDto> Login(string server, string email, string password);

        public Task<T> Request(string method, string path, object payload = null);
    }
}