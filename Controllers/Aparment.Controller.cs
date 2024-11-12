using EP02_LP2.Dtos;
using EP02_LP2.Models;
using EP02_LP2.Services;
using Microsoft.AspNetCore.Mvc;

namespace EP02_LP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly ApartmentService _apartmentService;

        public ApartmentController(ApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApartments()
        {
            var apartments = await _apartmentService.GetAllApartmentsAsync();
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartment(int id)
        {
            var apartment = await _apartmentService.GetApartmentByIdAsync(id);
            if (apartment == null)
                return NotFound();

            return Ok(apartment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment([FromBody] ApartmentDto apartmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var apartment = new Apartment
            {
                Name = apartmentDto.Name,
                Phone = apartmentDto.Phone,
                Floor = apartmentDto.Floor,
                Block = apartmentDto.Block
            };

            var createdApartment = await _apartmentService.CreateApartmentAsync(apartment);
            return CreatedAtAction(nameof(GetApartment), new { id = createdApartment.Id }, createdApartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] ApartmentDto apartmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var apartment = new Apartment
            {
                Id = id,
                Name = apartmentDto.Name,
                Phone = apartmentDto.Phone,
                Floor = apartmentDto.Floor,
                Block = apartmentDto.Block
            };

            var success = await _apartmentService.UpdateApartmentAsync(id, apartment);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var success = await _apartmentService.DeleteApartmentAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableApartments()
        {
            var availableApartments = await _apartmentService.GetAvailableApartmentsAsync();
            return Ok(availableApartments);
        }

        [HttpPost("add-available")]
        public async Task<IActionResult> AddAvailableApartment([FromBody] AvailableApartmentDto apartmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _apartmentService.AddAvailableApartmentAsync(apartmentDto.Floor, apartmentDto.Block);
            if (!success)
                return Conflict($"El apartamento en el piso {apartmentDto.Floor} y bloque {apartmentDto.Block} ya existe.");

            return Ok($"Apartamento en el piso {apartmentDto.Floor} y bloque {apartmentDto.Block} añadido exitosamente.");
        }
    }
}