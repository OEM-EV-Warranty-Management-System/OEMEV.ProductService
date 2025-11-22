using AutoMapper;
using Repository.Interfaces;
using Repository.Models;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services
{
    public class PartService : IPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PartDto> GetPartByIdAsync(long id)
        {
            var part = await _unitOfWork.Parts.GetByIdAsync(id);
            return _mapper.Map<PartDto>(part);
        }

        public async Task<IEnumerable<PartDto>> GetAllPartsAsync()
        {
            var parts = await _unitOfWork.Parts.GetAllAsync();
            return _mapper.Map<IEnumerable<PartDto>>(parts);
        }

        public async Task<PartDto> CreatePartAsync(CreatePartDto createPartDto)
        {
            var part = _mapper.Map<Part>(createPartDto);
            part.CreatedAt = DateTime.UtcNow;
            part.IsActive = true;

            var createdPart = await _unitOfWork.Parts.AddAsync(part);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<PartDto>(createdPart);
        }

        public async Task<PartDto> UpdatePartAsync(long id, UpdatePartDto updatePartDto)
        {
            var existingPart = await _unitOfWork.Parts.GetByIdAsync(id);
            if (existingPart == null)
                throw new KeyNotFoundException($"Part with ID {id} not found.");

            _mapper.Map(updatePartDto, existingPart);
            var updatedPart = await _unitOfWork.Parts.UpdateAsync(existingPart);
            return _mapper.Map<PartDto>(updatedPart);
        }

        public async Task<bool> DeletePartAsync(long id)
        {
            return await _unitOfWork.Parts.DeleteAsync(id);
        }

        public async Task<IEnumerable<PartDto>> GetPartsByTypeAsync(string type)
        {
            var parts = await _unitOfWork.Parts.GetByTypeAsync(type);
            return _mapper.Map<IEnumerable<PartDto>>(parts);
        }

        public async Task<IEnumerable<PartDto>> SearchPartsByNameAsync(string name)
        {
            var parts = await _unitOfWork.Parts.GetByNameAsync(name);
            return _mapper.Map<IEnumerable<PartDto>>(parts);
        }
    }
}