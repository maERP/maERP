#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<CustomerDetailDto> GetDetails(uint id);
}

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<CustomerDetailDto> GetDetails(uint id)
    {
        var customer = await _context.Customer
            .ProjectTo<CustomerDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if(customer == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return customer;
    }
}