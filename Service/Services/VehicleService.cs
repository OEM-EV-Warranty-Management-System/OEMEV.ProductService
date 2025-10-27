using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VehicleDto> GetVehicleByIdAsync(long id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<VehicleDto> GetVehicleByVINAsync(string vin)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByVINAsync(vin);
            return _mapper.Map<VehicleDto>(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createVehicleDto)
        {
            // Check if VIN already exists
            var vinExists = await _unitOfWork.Vehicles.VINExistsAsync(createVehicleDto.Vin);
            if (vinExists)
                throw new InvalidOperationException($"Vehicle with VIN {createVehicleDto.Vin} already exists.");

            var vehicle = _mapper.Map<Vehicle>(createVehicleDto);
            vehicle.CreatedAt = DateTime.UtcNow;
            vehicle.IsActive = true;
                
            var createdVehicle = await _unitOfWork.Vehicles.AddAsync(vehicle);
            return _mapper.Map<VehicleDto>(createdVehicle);
        }

        public async Task<VehicleDto> UpdateVehicleAsync(long id, UpdateVehicleDto updateVehicleDto)
        {
            var existingVehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (existingVehicle == null)
                throw new KeyNotFoundException($"Vehicle with ID {id} not found.");

            _mapper.Map(updateVehicleDto, existingVehicle);
            var updatedVehicle = await _unitOfWork.Vehicles.UpdateAsync(existingVehicle);
            return _mapper.Map<VehicleDto>(updatedVehicle);
        }

        public async Task<bool> DeleteVehicleAsync(long id)
        {
            return await _unitOfWork.Vehicles.DeleteAsync(id);
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByCustomerAsync(Guid customerId)
        {
            var vehicles = await _unitOfWork.Vehicles.GetByCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByManufactureAsync(long manufactureId)
        {
            var vehicles = await _unitOfWork.Vehicles.GetByManufactureAsync(manufactureId);
            return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
        }

        public async Task UpdateVehicleStatusAsync(long vehicleId, string status)
        {
            await _unitOfWork.Vehicles.UpdateStatusAsync(vehicleId, status);
        }
    }
}