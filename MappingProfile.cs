using AutoMapper;
using Pharmacy.Dtos.Medicine;
using Pharmacy.Models;

namespace Pharmacy
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Medicine, GetMedicineDto>();
            CreateMap<Medicine, AddMedicineDto>();
            CreateMap<GetMedicineDto, Medicine>();
            CreateMap<AddMedicineDto, Medicine>();
            CreateMap<AddMedicineDto, GetMedicineDto>();
            CreateMap<GetMedicineDto, AddMedicineDto>();
        }

    }
}
