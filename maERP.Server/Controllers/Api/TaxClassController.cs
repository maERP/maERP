using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Server.Repository;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaxClassController : ControllerBase  
{
    private readonly IMapper _mapper;
    private readonly ITaxClassRepository _repository;

    public TaxClassController(IMapper mapper, ITaxClassRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/TaxClasses
    [HttpGet("GetAll")]
    // GET: api/TaxClasses?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<TaxClassListDto>>> GetTaxClass()
    {
        var result = await _repository.GetAllAsync<TaxClassListDto>();
        return Ok(result);
    }

    // GET: api/TaxClasses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaxClassDetailDto>> GetTaxClass(uint id)
    {
        var result = await _repository.GetByIdAsync(id);
        return Ok(result);
    }

    // PUT: api/TaxClasses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaxClass(uint id, TaxClassDetailDto updateTaxClassDetailDto)
    {
        try
        {
            await _repository.UpdateAsync(id, updateTaxClassDetailDto);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await TaxClassExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/TaxClasses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TaxClassDetailDto>> PostTaxClass(TaxClassDetailDto taxClassDto)
    {
        var taxClass = await _repository.AddAsync<TaxClassDetailDto, TaxClassDetailDto>(taxClassDto);
        return CreatedAtAction(nameof(GetTaxClass), new { id = taxClass.Id }, taxClass);
    }

    // DELETE: api/TaxClasses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaxClass(uint id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> TaxClassExists(uint id)
    {
        return await _repository.Exists(id);
    }
}