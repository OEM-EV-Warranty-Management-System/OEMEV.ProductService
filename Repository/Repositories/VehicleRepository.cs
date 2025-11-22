using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetByIdAsync(long id)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleParts)
                .FirstOrDefaultAsync(v => v.Id == id && v.IsActive == true);
        }

        public async Task<Vehicle?> GetByVINAsync(string vin)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleParts)
                .FirstOrDefaultAsync(v => v.Vin == vin && v.IsActive == true);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Include(v => v.VehicleParts)
                .Where(v => v.IsActive == true)
                .ToListAsync();
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            vehicle.CreatedAt = DateTime.UtcNow;
            vehicle.IsActive = true;
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null || !vehicle.IsActive == true)
                return false;

            vehicle.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Vehicle>> GetByCustomerAsync(Guid customerId)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleParts)
                .Where(v => v.CustomerId == customerId && v.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByManufactureAsync(long manufactureId)
        {
            return await _context.Vehicles
                .Include(v => v.VehicleParts)
                .Where(v => v.ManufactureId == manufactureId && v.IsActive == true)
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(long vehicleId, string status)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle != null && vehicle.IsActive == true)
            {
                vehicle.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> VINExistsAsync(string vin)
        {
            return await _context.Vehicles.AnyAsync(v => v.Vin == vin && v.IsActive == true);
        }
    }
}