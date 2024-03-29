﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Dtos.Warehouse;
using maERP.Server.Contracts;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaxClassesController : ControllerBase  
{
    private readonly ITaxClassRepository _repository;

    public TaxClassesController(ITaxClassRepository repository)
    {
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
    public async Task<ActionResult<TaxClassDetailDto>> GetTaxClass(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        return Ok(result);
    }

    // PUT: api/TaxClasses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTaxClass(int id, [FromBody] TaxClassUpdateDto taxClassUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync<TaxClassUpdateDto>(id, taxClassUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/TaxClasses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TaxClassDetailDto>> PostTaxClass(TaxClassCreateDto taxClassCreateDto)
    {
        var taxClass = await _repository.AddAsync<TaxClassCreateDto, TaxClassDetailDto>(taxClassCreateDto);
        return CreatedAtAction(nameof(GetTaxClass), new { id = taxClass.Id }, taxClass);
    }

    // DELETE: api/TaxClasses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaxClass(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}