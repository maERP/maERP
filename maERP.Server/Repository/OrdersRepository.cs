#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Order;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface IOrderRepository : IGenericRepository<Order>
{
    
}

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}