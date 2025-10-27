using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class VehiclePartRepository : IVehiclePartRepository
    {
        private readonly AppDbContext _context;

        public VehiclePartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VehiclePart?> GetByIdAsync(long id)
        {
            return await _context.VehicleParts
                .Include(vp => vp.Vehicle)
                .Include(vp => vp.Part)
                .FirstOrDefaultAsync(vp => vp.Id == id && vp.IsActive == true);
        }

        public async Task<IEnumerable<VehiclePart>> GetAllAsync()
        {
            return await _context.VehicleParts
                .Include(vp => vp.Vehicle)
                .Include(vp => vp.Part)
                .Where(vp => vp.IsActive == true)
                .ToListAsync();
        }

        public async Task<VehiclePart> AddAsync(VehiclePart vehiclePart)
        {
            vehiclePart.CreatedAt = DateTime.UtcNow;
            vehiclePart.IsActive = true;
            _context.VehicleParts.Add(vehiclePart);
            await _context.SaveChangesAsync();
            return vehiclePart;
        }

        public async Task<VehiclePart> UpdateAsync(VehiclePart vehiclePart)
        {
            _context.VehicleParts.Update(vehiclePart);
            await _context.SaveChangesAsync();
            return vehiclePart;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var vehiclePart = await _context.VehicleParts.FindAsync(id);
            if (vehiclePart == null || !vehiclePart.IsActive == true)
                return false;

            vehiclePart.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VehiclePart>> GetByVehicleIdAsync(long vehicleId)
        {
            return await _context.VehicleParts
                .Include(vp => vp.Part)
                .Where(vp => vp.VehicleId == vehicleId && vp.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehiclePart>> GetByPartIdAsync(long partId)
        {
            return await _context.VehicleParts
                .Include(vp => vp.Vehicle)
                .Where(vp => vp.PartId == partId && vp.IsActive == true)
                .ToListAsync();
        }

        public async Task<VehiclePart?> GetBySerialNumberAsync(string serialNumber)
        {
            return await _context.VehicleParts
                .Include(vp => vp.Vehicle)
                .Include(vp => vp.Part)
                .FirstOrDefaultAsync(vp => vp.SerialNumber == serialNumber && vp.IsActive == true);
        }
    }
}