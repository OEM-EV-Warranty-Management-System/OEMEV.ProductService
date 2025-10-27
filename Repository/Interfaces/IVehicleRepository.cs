using Repository.Models;

namespace Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(long id);
        Task<Vehicle> GetByVINAsync(string vin);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
        Task<bool> DeleteAsync(long id);
        Task<IEnumerable<Vehicle>> GetByCustomerAsync(Guid customerId);
        Task<IEnumerable<Vehicle>> GetByManufactureAsync(long manufactureId);
        Task UpdateStatusAsync(long vehicleId, string status);
        Task<bool> VINExistsAsync(string vin);
    }
}