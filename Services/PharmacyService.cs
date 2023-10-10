using Microsoft.AspNetCore.Identity;
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
            new Medicine {Name = "Morphidox", Price = 1000, Quantity = 50, Type = MedicineType.Capsule},
            new Medicine(),
        };
        public async Task<ServiceResponse<List<Medicine>>> AddMedicine(Medicine medicine)
        {
            var count = medicines.Count;
            medicines.Add(medicine);
            var serviceResponse = new ServiceResponse<List<Medicine>> {
                Data = medicines,
                Message = count < medicines.Count ? "Item added successfully!" : "Error adding item!",
                Success = count < medicines.Count
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<Medicine>> DeleteMedicine(long id)
        {
            var count = medicines.Count;
            var foundMedicine = medicines.FirstOrDefault(medicine => medicine.Id == id);

            if (foundMedicine is null)
            {
                throw new Exception($"No item with specific Id = {id} found!");
            }

            medicines.Remove(foundMedicine);
            var serviceResponse = new ServiceResponse<Medicine>
            {
                Data = foundMedicine,
                Message = count > medicines.Count ? $"Item with id = {id} deleted successfully!" : $"Error deleting item with id = {id}!",
                Success = count > medicines.Count
            };

            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<Medicine>>> GetMedicines()
        {
            var serviceResponse = new ServiceResponse<List<Medicine>>
            {
                Data = medicines,
                Message = medicines.Count > 0 ? "Items fetched successfully!" : "No item found!",
                Success = true
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<Medicine>> GetSingleById(long id)
        {

            var foundMedicine = medicines.FirstOrDefault(medicine => medicine.Id == id);
            if (foundMedicine is null)
            {
                throw new Exception($"No item with specific Id = {id} found!");
            }

            var serviceResponse = new ServiceResponse<Medicine>
            {
                Data = foundMedicine,
                Message = foundMedicine is not null ? $"Item with id = {id} found successfully!" : $"Item with id = {id} not found!",
                Success = foundMedicine is not null
            };

            return serviceResponse;
        }
    }
}
