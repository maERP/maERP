using System.Threading.Tasks;
using maERP.Shared.Dtos.User;

namespace maERP.Client.Contracts
{
    public interface IDataService<T> where T : class
    {
        public Task<bool> Login(string server, string email, string password);

        public Task<T> Request(string method, string path, object payload = null);
    }
}