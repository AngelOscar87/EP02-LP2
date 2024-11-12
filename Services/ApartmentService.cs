using EP02_LP2.Data;
using EP02_LP2.Models;
using Microsoft.EntityFrameworkCore;

namespace EP02_LP2.Services
{
    public class ApartmentService
    {
        private readonly TenantDbContext _context;

        public ApartmentService(TenantDbContext context)
        {
            _context = context;
        }

        // Obtener todos los apartamentos
        public async Task<List<Apartment>> GetAllApartmentsAsync()
        {
            return await _context.Apartments.ToListAsync();
        }

        // Obtener un apartamento por ID
        public async Task<Apartment?> GetApartmentByIdAsync(int id)
        {
            return await _context.Apartments.FindAsync(id);
        }

        // Crear un nuevo apartamento
        public async Task<Apartment> CreateApartmentAsync(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        // Actualizar un apartamento existente
        public async Task<bool> UpdateApartmentAsync(int id, Apartment apartment)
        {
            var existingApartment = await _context.Apartments.FindAsync(id);
            if (existingApartment == null)
                return false;

            existingApartment.Name = apartment.Name;
            existingApartment.Phone = apartment.Phone;
            existingApartment.Floor = apartment.Floor;
            existingApartment.Block = apartment.Block;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un apartamento por ID
        public async Task<bool> DeleteApartmentAsync(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
                return false;

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return true;
        }

        // Listar apartamentos disponibles en 15 pisos con varios bloques
        public async Task<Dictionary<int, List<string>>> GetAvailableApartmentsAsync()
        {
            var floors = Enumerable.Range(1, 15).ToList(); // 15 pisos
            var blocks = new List<string> { "A", "B", "C", "D" }; // Bloques por piso
            var availableApartments = new Dictionary<int, List<string>>();

            foreach (var floor in floors)
            {
                var apartmentsInFloor = await _context.Apartments
                    .Where(a => a.Floor == floor)
                    .Select(a => a.Block)
                    .ToListAsync();

                var availableBlocks = blocks.Except(apartmentsInFloor).ToList();
                availableApartments[floor] = availableBlocks;
            }

            return availableApartments;
        }

        public async Task<bool> AddAvailableApartmentAsync(int floor, string block)
        {
            // Verificar si ya existe el apartamento
            var exists = await _context.Apartments
                .AnyAsync(a => a.Floor == floor && a.Block == block);

            if (exists)
            {
                return false; // Ya existe
            }

            // Crear un nuevo apartamento disponible
            var apartment = new Apartment
            {
                Name = "Disponible",
                Phone = "N/A",
                Floor = floor,
                Block = block
            };

            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}