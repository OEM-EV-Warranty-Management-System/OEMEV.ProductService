using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class PartInventoryService : IPartInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartInventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PartInventoryDto> GetInventoryByIdAsync(long id)
        {
            var inventory = await _unitOfWork.PartInventories.GetByIdAsync(id);
            return _mapper.Map<PartInventoryDto>(inventory);
        }

        public async Task<IEnumerable<PartInventoryDto>> GetAllInventoriesAsync()
        {
            var inventories = await _unitOfWork.PartInventories.GetAllAsync();
            return _mapper.Map<IEnumerable<PartInventoryDto>>(inventories);
        }

        public async Task<PartInventoryDto> CreateInventoryAsync(CreatePartInventoryDto createInventoryDto)
        {
            // Check if part exists
            var partExists = await _unitOfWork.Parts.ExistsAsync(createInventoryDto.PartId);
            if (!partExists)
                throw new KeyNotFoundException($"Part with ID {createInventoryDto.PartId} not found.");

            var inventory = _mapper.Map<PartInventory>(createInventoryDto);
            inventory.CreatedAt = DateTime.UtcNow;
            inventory.IsActive = true;

            await _unitOfWork.PartInventories.AddAsync(inventory);
            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                // Đặt Breakpoint (dấu chấm đỏ) tại dòng dưới này:
                var inner = ex.InnerException;
                while (inner != null)
                {
                    // Xem giá trị của inner.Message
                    Console.WriteLine(inner.Message);
                    inner = inner.InnerException;
                }
                throw;
            }

            return _mapper.Map<PartInventoryDto>(inventory);
        }

        public async Task<PartInventoryDto> UpdateInventoryAsync(long id, UpdatePartInventoryDto updateInventoryDto)
        {
            var existingInventory = await _unitOfWork.PartInventories.GetByIdAsync(id);
            if (existingInventory == null)
                throw new KeyNotFoundException($"Inventory with ID {id} not found.");

            _mapper.Map(updateInventoryDto, existingInventory);
            await _unitOfWork.PartInventories.UpdateAsync(existingInventory);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PartInventoryDto>(existingInventory);
        }

        public async Task<bool> DeleteInventoryAsync(long id)
        {
            var result = await _unitOfWork.PartInventories.DeleteAsync(id);
            if (result)
            {
                await _unitOfWork.SaveAsync();
            }
            return result;
        }

        public async Task<IEnumerable<PartInventoryDto>> GetInventoryByPartIdAsync(long partId)
        {
            var inventories = await _unitOfWork.PartInventories.GetByPartIdAsync(partId);
            return _mapper.Map<IEnumerable<PartInventoryDto>>(inventories);
        }

        public async Task<IEnumerable<PartInventoryDto>> GetInventoryByServiceCenterAsync(long serviceCenterId)
        {
            var inventories = await _unitOfWork.PartInventories.GetByServiceCenterAsync(serviceCenterId);
            return _mapper.Map<IEnumerable<PartInventoryDto>>(inventories);
        }

        public async Task UpdateInventoryQuantityAsync(long inventoryId, int newQuantity)
        {
            await _unitOfWork.PartInventories.UpdateInformationAsync(inventoryId, newQuantity); // repository saves
        }

        public async Task<int> GetTotalQuantityByPartAsync(long partId)
        {
            return await _unitOfWork.PartInventories.GetTotalQuantityByPartAsync(partId);
        }

        // Newly implemented patch methods
        public async Task UpdateInventoryServiceCenterAsync(long inventoryId, long serviceCenterId)
        {
            var existingInventory = await _unitOfWork.PartInventories.GetByIdAsync(inventoryId);
            if (existingInventory == null)
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");

            existingInventory.ServiceCenterId = serviceCenterId;
            await _unitOfWork.PartInventories.UpdateAsync(existingInventory);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateInventoryManufacturerAsync(long inventoryId, long manufacturerId)
        {
            var existingInventory = await _unitOfWork.PartInventories.GetByIdAsync(inventoryId);
            if (existingInventory == null)
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");

            existingInventory.ManufactureId = manufacturerId;
            await _unitOfWork.PartInventories.UpdateAsync(existingInventory);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateInventoryLocationAsync(long inventoryId, long serviceCenterId, long manufacturerId)
        {
            var existingInventory = await _unitOfWork.PartInventories.GetByIdAsync(inventoryId);
            if (existingInventory == null)
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");

            existingInventory.ServiceCenterId = serviceCenterId;
            existingInventory.ManufactureId = manufacturerId;
            await _unitOfWork.PartInventories.UpdateAsync(existingInventory);
            await _unitOfWork.SaveAsync();
        }
    }
}