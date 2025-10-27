using Repository.Models;

namespace Repository.Interfaces
{
    public interface IVehiclePartRepository
    {
        Task<VehiclePart> GetByIdAsync(long id);
        Task<IEnumerable<VehiclePart>> GetAllAsync();
        Task<VehiclePart> AddAsync(VehiclePart vehiclePart);
        Task<VehiclePart> UpdateAsync(VehiclePart vehiclePart);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<VehiclePart>> GetByVehicleIdAsync(long vehicleId);
        Task<IEnumerable<VehiclePart>> GetByPartIdAsync(long partId);
        Task<VehiclePart> GetBySerialNumberAsync(string serialNumber);
    }
}