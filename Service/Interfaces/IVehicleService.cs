using Service.DTOs;

namespace Service.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> GetVehicleByIdAsync(long id);
        Task<VehicleDto> GetVehicleByVINAsync(string vin);
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createVehicleDto);
        Task<VehicleDto> UpdateVehicleAsync(long id, UpdateVehicleDto updateVehicleDto);
        Task<bool> DeleteVehicleAsync(long id);
        Task<IEnumerable<VehicleDto>> GetVehiclesByCustomerAsync(Guid customerId);
        Task<IEnumerable<VehicleDto>> GetVehiclesByManufactureAsync(long manufactureId);
        Task UpdateVehicleStatusAsync(long vehicleId, string status);
    }
}