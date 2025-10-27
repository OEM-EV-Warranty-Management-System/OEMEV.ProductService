using Service.DTOs;

namespace Service.Interfaces
{
    public interface IVehiclePartService
    {
        Task<VehiclePartDto> GetVehiclePartByIdAsync(long id);
        Task<IEnumerable<VehiclePartDto>> GetAllVehiclePartsAsync();
        Task<VehiclePartDto> CreateVehiclePartAsync(CreateVehiclePartDto createVehiclePartDto);
        Task<VehiclePartDto> UpdateVehiclePartAsync(long id, UpdateVehiclePartDto updateVehiclePartDto);
        Task<bool> DeleteVehiclePartAsync(long id);
        Task<IEnumerable<VehiclePartDto>> GetVehiclePartsByVehicleIdAsync(long vehicleId);
        Task<IEnumerable<VehiclePartDto>> GetVehiclePartsByPartIdAsync(long partId);
        Task<VehiclePartDto> GetVehiclePartBySerialNumberAsync(string serialNumber);
    }
}