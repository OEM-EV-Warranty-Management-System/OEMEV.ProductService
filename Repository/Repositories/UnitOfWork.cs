using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IPartInventoryRepository _partInventoryRepository;
        private IPartRepository _partRepository;
        private IVehicleRepository _vehicleRepository;
        private IVehiclePartRepository _vehiclePartRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPartInventoryRepository PartInventories =>
            _partInventoryRepository ??= new PartInventoryRepository(_context);

        public IPartRepository Parts =>
            _partRepository ??= new PartRepository(_context);

        public IVehicleRepository Vehicles =>
            _vehicleRepository ??= new VehicleRepository(_context);

        public IVehiclePartRepository VehicleParts =>
            _vehiclePartRepository ??= new VehiclePartRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}