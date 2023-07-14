#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Shared.Models;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Server.Exceptions;

namespace maERP.Server.Repository;

public interface ISalesChannelRepository : IGenericRepository<SalesChannel>
{
    Task<SalesChannelDetailDto> GetDetails(int id);
    Task<SalesChannelDetailDto> AddWithDetailsAsync(SalesChannelCreateDto salesChannelCreateDto);
    Task UpdateWithDetailsAsync(int id, SalesChannelUpdateDto salesChannelUpdateDto);

}

public class SalesChannelRepository : GenericRepository<SalesChannel>, ISalesChannelRepository
{
    private readonly ApplicationDbContext _context;

    public SalesChannelRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }

    public async Task<SalesChannelDetailDto> GetDetails(int id)
    {
        var salesChannel = await _context.SalesChannel
            .Include(s => s.Warehouse)
            .FirstOrDefaultAsync(s => s.Id == id);

        salesChannel.WarehouseId = salesChannel.Warehouse.Id;

        var salesChannelDto = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        return salesChannelDto;
    }

    public async Task<SalesChannelDetailDto> AddWithDetailsAsync(SalesChannelCreateDto salesChannelCreateDto)
    {
        var salesChannel = _mapper.Map<SalesChannel>(salesChannelCreateDto);

        salesChannel.Warehouse = await _context.Warehouse.FirstOrDefaultAsync(w => w.Id == salesChannelCreateDto.WarehouseId);
         
        _context.Add(salesChannel);
        await _context.SaveChangesAsync();

        var salesChannelDetailDto= _mapper.Map<SalesChannelDetailDto>(salesChannel);

        return salesChannelDetailDto;
    }

    public async Task UpdateWithDetailsAsync(int id, SalesChannelUpdateDto salesChannelUpdateDto) 
    {
        if (!await base.Exists(id))
        {
            throw new NotFoundException("SalesChannel not found", id);
        }

        var salesChannel = _mapper.Map<SalesChannel>(salesChannelUpdateDto);
        salesChannel.Warehouse = await _context.Warehouse.FirstOrDefaultAsync(w => w.Id == salesChannelUpdateDto.WarehouseId);

        _context.Update(salesChannel);
        await _context.SaveChangesAsync();
    }
}