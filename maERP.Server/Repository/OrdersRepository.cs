using AutoMapper;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public interface IOrderRepository : IGenericRepository<Order>
{
}

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}