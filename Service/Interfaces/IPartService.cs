using Service.DTOs;

namespace Service.Interfaces
{
    public interface IPartService
    {
        Task<PartDto> GetPartByIdAsync(long id);
        Task<IEnumerable<PartDto>> GetAllPartsAsync();
        Task<PartDto> CreatePartAsync(CreatePartDto createPartDto);
        Task<PartDto> UpdatePartAsync(long id, UpdatePartDto updatePartDto);
        Task<bool> DeletePartAsync(long id);
        Task<IEnumerable<PartDto>> GetPartsByTypeAsync(string type);
        Task<IEnumerable<PartDto>> SearchPartsByNameAsync(string name);
    }
}