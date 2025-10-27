using Repository.Models;

namespace Repository.Interfaces
{
    public interface IPartRepository
    {
        Task<Part> GetByIdAsync(long id);
        Task<IEnumerable<Part>> GetAllAsync();
        Task<Part> AddAsync(Part part);
        Task<Part> UpdateAsync(Part part);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Part>> GetByTypeAsync(string type);
        Task<IEnumerable<Part>> GetByNameAsync(string name);
        Task<bool> ExistsAsync(long id);
    }
}