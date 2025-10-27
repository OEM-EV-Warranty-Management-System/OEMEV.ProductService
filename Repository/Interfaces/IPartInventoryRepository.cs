using Repository.Models;

namespace Repository.Interfaces
{
    public interface IPartInventoryRepository
    {
        Task<PartInventory> GetByIdAsync(long id);
        Task<IEnumerable<PartInventory>> GetAllAsync();
        Task<PartInventory> AddAsync(PartInventory partInventory);
        Task<PartInventory> UpdateAsync(PartInventory partInventory);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<PartInventory>> GetByPartIdAsync(long partId);
        Task<IEnumerable<PartInventory>> GetByServiceCenterAsync(long serviceCenterId);
        Task UpdateQuantityAsync(long partInventoryId, int newQuantity);
        Task<int> GetTotalQuantityByPartAsync(long partId);
    }
}