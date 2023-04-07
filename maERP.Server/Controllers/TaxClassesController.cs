using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Models;
using maERP.Shared.Dtos.TaxClass;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaxClassesController : ControllerBase  
{
    private readonly IMapper _mapper;
    private readonly ITaxClassesRepository _repository;

    public TaxClassesController(IMapper mapper, ITaxClassesRepository repository)
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

    // GET: api/TaxClasses/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaxClassListDto>>> GetPagedTaxClass([FromQuery] QueryParameters queryParameters)
    {
        var pagedResult = await _repository.GetAllAsync<TaxClassListDto>(queryParameters);
        return Ok(pagedResult);
    }

    // GET: api/TaxClasses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaxClassDetailDto>> GetTaxClass(int id)
    {
        var result = await _repository.GetDetails(id);
        return Ok(result);
    }

    // PUT: api/TaxClasses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaxClass(int id, TaxClassDetailDto updateTaxClassDetailDto)
    {
        if (id != updateTaxClassDetailDto.Id)
        {
            return BadRequest("Invalid Record Id");
        }

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
    public async Task<IActionResult> DeleteTaxClass(int id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> TaxClassExists(int id)
    {
        return await _repository.Exists(id);
    }
}