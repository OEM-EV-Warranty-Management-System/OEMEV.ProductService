using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class VehiclePartService : IVehiclePartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehiclePartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VehiclePartDto> GetVehiclePartByIdAsync(long id)
        {
            var vehiclePart = await _unitOfWork.VehicleParts.GetByIdAsync(id);
            return _mapper.Map<VehiclePartDto>(vehiclePart);
        }

        public async Task<IEnumerable<VehiclePartDto>> GetAllVehiclePartsAsync()
        {
            var vehicleParts = await _unitOfWork.VehicleParts.GetAllAsync();
            return _mapper.Map<IEnumerable<VehiclePartDto>>(vehicleParts);
        }

        public async Task<VehiclePartDto> CreateVehiclePartAsync(CreateVehiclePartDto createVehiclePartDto)
        {
            // Check if vehicle exists
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(createVehiclePartDto.VehicleId);
            if (vehicle == null)
                throw new KeyNotFoundException($"Vehicle with ID {createVehiclePartDto.VehicleId} not found.");

            // Check if part exists
            var part = await _unitOfWork.Parts.GetByIdAsync(createVehiclePartDto.PartId);
            if (part == null)
                throw new KeyNotFoundException($"Part with ID {createVehiclePartDto.PartId} not found.");

            // Check if serial number already exists
            var existingSerial = await _unitOfWork.VehicleParts.GetBySerialNumberAsync(createVehiclePartDto.SerialNumber);
            if (existingSerial != null)
                throw new InvalidOperationException($"Vehicle part with serial number {createVehiclePartDto.SerialNumber} already exists.");

            var vehiclePart = _mapper.Map<VehiclePart>(createVehiclePartDto);
            var createdVehiclePart = await _unitOfWork.VehicleParts.AddAsync(vehiclePart);
            return _mapper.Map<VehiclePartDto>(createdVehiclePart);
        }

        public async Task<VehiclePartDto> UpdateVehiclePartAsync(long id, UpdateVehiclePartDto updateVehiclePartDto)
        {
            var existingVehiclePart = await _unitOfWork.VehicleParts.GetByIdAsync(id);
            if (existingVehiclePart == null)
                throw new KeyNotFoundException($"Vehicle part with ID {id} not found.");

            _mapper.Map(updateVehiclePartDto, existingVehiclePart);
            var updatedVehiclePart = await _unitOfWork.VehicleParts.UpdateAsync(existingVehiclePart);
            return _mapper.Map<VehiclePartDto>(updatedVehiclePart);
        }

        public async Task<bool> DeleteVehiclePartAsync(long id)
        {
            return await _unitOfWork.VehicleParts.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehiclePartDto>> GetVehiclePartsByVehicleIdAsync(long vehicleId)
        {
            var vehicleParts = await _unitOfWork.VehicleParts.GetByVehicleIdAsync(vehicleId);
            return _mapper.Map<IEnumerable<VehiclePartDto>>(vehicleParts);
        }

        public async Task<IEnumerable<VehiclePartDto>> GetVehiclePartsByPartIdAsync(long partId)
        {
            var vehicleParts = await _unitOfWork.VehicleParts.GetByPartIdAsync(partId);
            return _mapper.Map<IEnumerable<VehiclePartDto>>(vehicleParts);
        }

        public async Task<VehiclePartDto> GetVehiclePartBySerialNumberAsync(string serialNumber)
        {
            var vehiclePart = await _unitOfWork.VehicleParts.GetBySerialNumberAsync(serialNumber);
            return _mapper.Map<VehiclePartDto>(vehiclePart);
        }
    }
}