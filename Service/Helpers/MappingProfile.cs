using AutoMapper;
using Repository.Models;
using Service.DTOs;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Part mappings
            CreateMap<Part, PartDto>();
            CreateMap<CreatePartDto, Part>();
            CreateMap<UpdatePartDto, Part>();

            // PartInventory mappings
            CreateMap<PartInventory, PartInventoryDto>();
            CreateMap<CreatePartInventoryDto, PartInventory>();
            CreateMap<UpdatePartInventoryDto, PartInventory>();

            // Vehicle mappings
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<CreateVehicleDto, Vehicle>();
            CreateMap<UpdateVehicleDto, Vehicle>();

            // VehiclePart mappings
            CreateMap<VehiclePart, VehiclePartDto>();
            CreateMap<CreateVehiclePartDto, VehiclePart>();
            CreateMap<UpdateVehiclePartDto, VehiclePart>();
        }
    }
}