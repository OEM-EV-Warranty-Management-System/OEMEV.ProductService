using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly AppDbContext _context;

        public PartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Part?> GetByIdAsync(long id)
        {
            return await _context.Parts
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive == true);
        }

        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await _context.Parts
                .Where(p => p.IsActive == true)
                .ToListAsync();
        }

        public async Task<Part> AddAsync(Part part)
        {
            part.CreatedAt = DateTime.UtcNow;
            part.IsActive = true;
            await _context.Parts.AddAsync(part);
            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<Part> UpdateAsync(Part part)
        {
            _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null || !part.IsActive == true)
                return false;

            part.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Part>> GetByTypeAsync(string type)
        {
            return await _context.Parts
                .Where(p => p.Type == type && p.IsActive == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<Part>> GetByNameAsync(string name)
        {
            return await _context.Parts
                .Where(p => p.Name.Contains(name) && p.IsActive == true)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Parts.AnyAsync(p => p.Id == id && p.IsActive == true);
        }
    }
}