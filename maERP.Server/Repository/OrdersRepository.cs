#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Contracts;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Order;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<OrderDetailDto> GetDetails(int id)
    {
        var order = await _context.Customer
            .ProjectTo<OrderDetailDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        if(order == null)
        {
            throw new NotFoundException(nameof(GetDetails), id);
        }

        return order;
    }
}