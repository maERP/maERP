#nullable disable

using AutoMapper;
using maERP.Server.Contracts;
using maERP.Shared.Models;
using maERP.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace maERP.Server.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
        }

        public async Task<Customer> getDetails(int id)
        {
            return await _context.Customer.FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}