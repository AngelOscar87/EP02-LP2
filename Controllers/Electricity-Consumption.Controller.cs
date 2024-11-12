using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP02_LP2.Models;
using EP02_LP2.Data;

[Route("api/[controller]")]
[ApiController]
public class ElectricityConsumptionController : ControllerBase
{
    private readonly TenantDbContext _context;

    public ElectricityConsumptionController(TenantDbContext context)
    {
        _context = context;
    }

    // GET: api/electricityconsumption
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ElectricityConsumption>>> GetElectricityConsumptions()
    {
        return await _context.ElectricityConsumptions
            .Include(ec => ec.Apartment) // Incluye detalles del apartamento
            .ToListAsync();
    }

    // GET: api/electricityconsumption/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ElectricityConsumption>> GetElectricityConsumption(int id)
    {
        var consumption = await _context.ElectricityConsumptions
            .Include(ec => ec.Apartment)
            .FirstOrDefaultAsync(ec => ec.Id == id);

        if (consumption == null)
        {
            return NotFound();
        }

        return consumption;
    }

    // POST: api/electricityconsumption
    [HttpPost]
    public async Task<ActionResult<ElectricityConsumption>> CreateElectricityConsumption([FromBody] ElectricityConsumption consumption)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var apartmentExists = await _context.Apartments.AnyAsync(a => a.Id == consumption.ApartmentId);
        if (!apartmentExists)
        {
            return BadRequest("El apartamento especificado no existe.");
        }

        _context.ElectricityConsumptions.Add(consumption);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetElectricityConsumption), new { id = consumption.Id }, consumption);
    }

    // PUT: api/electricityconsumption/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateElectricityConsumption(int id, [FromBody] ElectricityConsumption consumption)
    {
        if (id != consumption.Id)
        {
            return BadRequest();
        }

        _context.Entry(consumption).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ElectricityConsumptionExists(id))
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

    // DELETE: api/electricityconsumption/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteElectricityConsumption(int id)
    {
        var consumption = await _context.ElectricityConsumptions.FindAsync(id);
        if (consumption == null)
        {
            return NotFound();
        }

        _context.ElectricityConsumptions.Remove(consumption);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ElectricityConsumptionExists(int id)
    {
        return _context.ElectricityConsumptions.Any(ec => ec.Id == id);
    }
}
