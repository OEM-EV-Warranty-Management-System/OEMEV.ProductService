using Repository.Interfaces;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPartInventoryRepository PartInventories { get; }
        IPartRepository Parts { get; }
        IVehicleRepository Vehicles { get; }
        IVehiclePartRepository VehicleParts { get; }
        Task<int> SaveAsync();
    }
}