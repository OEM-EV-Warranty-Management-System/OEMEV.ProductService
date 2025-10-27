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
            var createdInventory = await _unitOfWork.PartInventories.AddAsync(inventory);
            return _mapper.Map<PartInventoryDto>(createdInventory);
        }

        public async Task<PartInventoryDto> UpdateInventoryAsync(long id, UpdatePartInventoryDto updateInventoryDto)
        {
            var existingInventory = await _unitOfWork.PartInventories.GetByIdAsync(id);
            if (existingInventory == null)
                throw new KeyNotFoundException($"Inventory with ID {id} not found.");

            _mapper.Map(updateInventoryDto, existingInventory);
            var updatedInventory = await _unitOfWork.PartInventories.UpdateAsync(existingInventory);
            return _mapper.Map<PartInventoryDto>(updatedInventory);
        }

        public async Task<bool> DeleteInventoryAsync(long id)
        {
            return await _unitOfWork.PartInventories.DeleteAsync(id);
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
            await _unitOfWork.PartInventories.UpdateQuantityAsync(inventoryId, newQuantity);
        }

        public async Task<int> GetTotalQuantityByPartAsync(long partId)
        {
            return await _unitOfWork.PartInventories.GetTotalQuantityByPartAsync(partId);
        }
    }
}