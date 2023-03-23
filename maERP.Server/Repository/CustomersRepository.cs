#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomersRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<CustomerDto> GetDetails(int id)
    {
        var customer = await _context.Customer
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if(customer == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return customer;
    }
}