using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Dtos.Medicine;
using Pharmacy.Models;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly List<Medicine> medicines = new List<Medicine>
        {
            new Medicine {Id = 1, Name = "Aquavirine", Price = 1500, Quantity = 100, Type = MedicineType.Liquid},
            new Medicine {Id = 2, Name = "Morphidox", Price = 1000, Quantity = 50, Type = MedicineType.Capsule},
            new Medicine {Id = 3, Name = "Paraquine", Price = 700, Quantity = 25, Type = MedicineType.Tablet},
        };

        private readonly IMapper _mapper;

        public PharmacyService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetMedicineDto>>> AddMedicine(AddMedicineDto medicine)
        {
            var count = medicines.Count;
            var newMedicine = _mapper.Map<Medicine>(medicine);
            newMedicine.Id = medicines.Max(med => med.Id) + 1;
            medicines.Add(newMedicine);
            var serviceResponse = new ServiceResponse<List<GetMedicineDto>> {
                Data = medicines.Select(med => _mapper.Map<GetMedicineDto>(med)).ToList(),
                Message = count < medicines.Count ? "Item added successfully!" : "Error adding item!",
                Success = count < medicines.Count
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMedicineDto>> DeleteMedicine(long id)
        {
            var count = medicines.Count;
            var foundMedicine = medicines.FirstOrDefault(medicine => medicine.Id == id);

            if (foundMedicine is null)
            {
                var serviceResponceWithError = new ServiceResponse<GetMedicineDto>
                {
                    Message = $"No item with specific Id = {id} found!",
                    Success = false,
                };

                return serviceResponceWithError;
            }

            medicines.Remove(foundMedicine);
            var serviceResponse = new ServiceResponse<GetMedicineDto>
            {
                Data = _mapper.Map<GetMedicineDto>(foundMedicine),
                Message = count > medicines.Count ? $"Item with id = {id} deleted successfully!" : $"Error deleting item with id = {id}!",
                Success = count > medicines.Count
            };

            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<GetMedicineDto>>> GetMedicines()
        {
            var serviceResponse = new ServiceResponse<List<GetMedicineDto>>
            {
                Data = medicines.Select(med => _mapper.Map<GetMedicineDto>(med)).ToList(),
                Message = medicines.Count > 0 ? "Items fetched successfully!" : "No item found!",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMedicineDto>> GetSingleById(long id)
        {

            var foundMedicine = medicines.FirstOrDefault(medicine => medicine.Id == id);
            if (foundMedicine is null)
            {
                var serviceResponceWithError = new ServiceResponse<GetMedicineDto>
                {
                    Message = $"No item with specific Id = {id} found!",
                    Success = false,
                };

                return serviceResponceWithError;
            }

            var serviceResponse = new ServiceResponse<GetMedicineDto>
            {
                Data = _mapper.Map<GetMedicineDto>(foundMedicine),
                Message = foundMedicine is not null ? $"Item with id = {id} found successfully!" : $"Item with id = {id} not found!",
                Success = foundMedicine is not null
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetMedicineDto>> UpdateMedicine(Medicine medicine)
        {
            var foundMedicine = medicines.Find(med => med.Id == medicine.Id);
            if (foundMedicine is null )
            {
                var serviceResponceWithError = new ServiceResponse<GetMedicineDto>
                {
                    Message = $"No item with Id = {medicine.Id} found!",
                    Success = false,
                };

                return serviceResponceWithError;
            }

            foundMedicine.Quantity = medicine.Quantity;
            foundMedicine.Price = medicine.Price;
            foundMedicine.Name = medicine.Name;
            foundMedicine.Type = medicine.Type;

            var serviceResponce = new ServiceResponse<GetMedicineDto>
            {
                Data = _mapper.Map<GetMedicineDto>(foundMedicine),
                Message = $"Item with Id = {medicine.Id} updated successfully!",
                Success = true
            };

            return serviceResponce;
        }
    }
}
