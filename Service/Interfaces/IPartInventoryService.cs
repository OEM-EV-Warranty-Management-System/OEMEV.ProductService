using Service.DTOs;

namespace Service.Interfaces
{
    public interface IPartInventoryService
    {
        Task<PartInventoryDto> GetInventoryByIdAsync(long id);
        Task<IEnumerable<PartInventoryDto>> GetAllInventoriesAsync();
        Task<PartInventoryDto> CreateInventoryAsync(CreatePartInventoryDto createInventoryDto);
        Task<PartInventoryDto> UpdateInventoryAsync(long id, UpdatePartInventoryDto updateInventoryDto);
        Task<bool> DeleteInventoryAsync(long id);
        Task<IEnumerable<PartInventoryDto>> GetInventoryByPartIdAsync(long partId);
        Task<IEnumerable<PartInventoryDto>> GetInventoryByServiceCenterAsync(long serviceCenterId);
        Task UpdateInventoryQuantityAsync(long inventoryId, int newQuantity);
        Task<int> GetTotalQuantityByPartAsync(long partId);
        Task UpdateInventoryServiceCenterAsync(long inventoryId, long serviceCenterId);
        Task UpdateInventoryManufacturerAsync(long inventoryId, long manufacturerId);
        Task UpdateInventoryLocationAsync(long inventoryId, long serviceCenterId, long manufacturerId);
    }
}