using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class PartInventoryRepository : IPartInventoryRepository
    {
        private readonly AppDbContext _context;

        public PartInventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PartInventory?> GetByIdAsync(long id)
        {
            return await _context.PartInventories
                .Include(pi => pi.Part)
                .FirstOrDefaultAsync(pi => pi.Id == id && pi.IsActive == true);
        }

        public async Task<IEnumerable<PartInventory>> GetAllAsync()
        {
            return await _context.PartInventories
                .Include(pi => pi.Part)
                .Where(pi => pi.IsActive == true)
                .ToListAsync();
        }

        public async Task<PartInventory> AddAsync(PartInventory partInventory)
        {
            partInventory.CreatedAt = DateTime.UtcNow;
            partInventory.IsActive = true;
            _context.PartInventories.Add(partInventory);
            await _context.SaveChangesAsync();
            return partInventory;
        }

        public async Task<PartInventory> UpdateAsync(PartInventory partInventory)
        {
            _context.PartInventories.Update(partInventory);
            await _context.SaveChangesAsync();
            return partInventory;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var partInventory = await _context.PartInventories.FindAsync(id);
            if (partInventory == null || !partInventory.IsActive == true)
                return false;

            partInventory.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PartInventory>> GetByPartIdAsync(long partId)
        {
            return await _context.PartInventories
                .Include(pi => pi.Part)
                .Where(pi => pi.PartId == partId && pi.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<PartInventory>> GetByServiceCenterAsync(long serviceCenterId)
        {
            return await _context.PartInventories
                .Include(pi => pi.Part)
                .Where(pi => pi.ServiceCenterId == serviceCenterId && pi.IsActive == true)
                .ToListAsync();
        }

        public async Task UpdateQuantityAsync(long partInventoryId, int newQuantity)
        {
            var partInventory = await _context.PartInventories.FindAsync(partInventoryId);
            if (partInventory != null && partInventory.IsActive == true)
            {
                partInventory.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalQuantityByPartAsync(long partId)
        {
            return (int)await _context.PartInventories
                .Where(pi => pi.PartId == partId && pi.IsActive == true)
                .SumAsync(pi => pi.Quantity);
        }
    }
}