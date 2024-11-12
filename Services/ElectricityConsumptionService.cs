using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EP02_LP2.Data;
using EP02_LP2.Models;

namespace EP02_LP2.Services
{
    public class ElectricityConsumptionService
    {
        private readonly TenantDbContext _context;

        public ElectricityConsumptionService(TenantDbContext context)
        {
            _context = context;
        }

        // Obtener todos los consumos eléctricos
        public async Task<List<ElectricityConsumption>> GetAllElectricityConsumptionsAsync()
        {
            return await _context.ElectricityConsumptions
                .Include(ec => ec.Apartment) // Incluye los datos del apartamento relacionado
                .ToListAsync();
        }

        // Obtener un consumo eléctrico por ID
        public async Task<ElectricityConsumption> GetElectricityConsumptionByIdAsync(int id)
        {
            var consumption = await _context.ElectricityConsumptions
                .Include(ec => ec.Apartment)
                .FirstOrDefaultAsync(ec => ec.Id == id);

            if (consumption == null)
            {
                throw new KeyNotFoundException($"Consumo eléctrico con ID {id} no encontrado.");
            }

            return consumption;
        }

        // Crear un nuevo consumo eléctrico
        public async Task<ElectricityConsumption> CreateElectricityConsumptionAsync(ElectricityConsumption consumption)
        {
            // Validar que el apartamento exista
            var apartmentExists = await _context.Apartments.AnyAsync(a => a.Id == consumption.ApartmentId);
            if (!apartmentExists)
            {
                throw new KeyNotFoundException("El apartamento especificado no existe.");
            }

            _context.ElectricityConsumptions.Add(consumption);
            await _context.SaveChangesAsync();
            return consumption;
        }

        // Actualizar un consumo eléctrico existente
        public async Task<bool> UpdateElectricityConsumptionAsync(int id, ElectricityConsumption consumption)
        {
            var existingConsumption = await _context.ElectricityConsumptions.FindAsync(id);
            if (existingConsumption == null)
                return false;

            existingConsumption.Date = consumption.Date;
            existingConsumption.QuantityKw = consumption.QuantityKw;
            existingConsumption.ApartmentId = consumption.ApartmentId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar un consumo eléctrico por ID
        public async Task<bool> DeleteElectricityConsumptionAsync(int id)
        {
            var consumption = await _context.ElectricityConsumptions.FindAsync(id);
            if (consumption == null)
                return false;

            _context.ElectricityConsumptions.Remove(consumption);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
