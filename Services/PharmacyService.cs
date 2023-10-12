using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
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
        private readonly DataContext _context;

        public PharmacyService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetMedicineDto>>> AddMedicine(AddMedicineDto medicine)
        {
            try
            {
                var newMedicine = _mapper.Map<Medicine>(medicine);
                _context.Medicines.Add(newMedicine);
                await _context.SaveChangesAsync();
                var dbMedicines = await _context.Medicines.ToListAsync();
                var serviceResponse = new ServiceResponse<List<GetMedicineDto>> {
                    Data = dbMedicines.Select(med => _mapper.Map<GetMedicineDto>(med)).ToList(),
                    Message = "Item added successfully!",
                    Success = true
                };
                return serviceResponse;
            }
            catch(Exception e)
            {
                return new ServiceResponse<List<GetMedicineDto>>
                {
                    Message = "Error adding item to db: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<GetMedicineDto>> DeleteMedicine(long id)
        {

            try
            {
                var foundMedicine = await _context.Medicines.FindAsync(id);
                if (foundMedicine is null)
                {
                    var serviceResponceWithError = new ServiceResponse<GetMedicineDto>
                    {
                        Message = $"No item with specific Id = {id} found!",
                        Success = false,
                    };

                    return serviceResponceWithError;
                }

                _context.Medicines.Remove(foundMedicine);
                await _context.SaveChangesAsync();

                var serviceResponse = new ServiceResponse<GetMedicineDto>
                {
                    Data = _mapper.Map<GetMedicineDto>(foundMedicine),
                    Message = $"Item with id = {id} deleted successfully!",
                    Success = true
                };

                return serviceResponse;
            }
            catch (Exception e)
            {
                return new ServiceResponse<GetMedicineDto>
                {
                    Message = "Error deleting item:" + e.Message,
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<List<GetMedicineDto>>> DeleteAllMedicine()
        {
            try
            {

                var medicines = await _context.Medicines.ToListAsync();
                _context.Medicines.RemoveRange(medicines);
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();

                var serviceResponse = new ServiceResponse<List<GetMedicineDto>>
                {
                    Message = $"All items deleted successfully!",
                    Success = true
                };

                return serviceResponse;
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<GetMedicineDto>>
                {
                    Message = "Error deleting all items:" + e.Message,
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<List<GetMedicineDto>>> GetAllMedicines()
        {
            try
            {
                var dbMedicines = await _context.Medicines.ToListAsync();
                var serviceResponse = new ServiceResponse<List<GetMedicineDto>>
                {
                    Data = dbMedicines.Select(med => _mapper.Map<GetMedicineDto>(med)).ToList(),
                    Message = medicines.Count > 0 ? "Items fetched successfully!" : "No item found!",
                    Success = true
                };

                return serviceResponse;
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<GetMedicineDto>>
                {
                    Message = "Error getting items:" + e.Message,
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<GetMedicineDto>> GetSingleById(long id)
        {
            try
            {
                var foundMedicine = await _context.Medicines.FindAsync(id);
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
            catch (Exception e)
            {
                return new ServiceResponse<GetMedicineDto>
                {
                    Message = "Error getting item:" + e.Message,
                    Success = false,
                };
            }
        }

        public async Task<ServiceResponse<GetMedicineDto>> UpdateMedicine(Medicine medicine)
        {
            try
            {
                var foundMedicine = await _context.Medicines.FindAsync(medicine.Id);
                if (foundMedicine is null)
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

                _context.Medicines.Update(foundMedicine);
                await _context.SaveChangesAsync();


                var serviceResponce = new ServiceResponse<GetMedicineDto>
                {
                    Data = _mapper.Map<GetMedicineDto>(foundMedicine),
                    Message = $"Item with Id = {medicine.Id} updated successfully!",
                    Success = true
                };

                return serviceResponce;
            }
            catch (Exception e)
            {
                return new ServiceResponse<GetMedicineDto>
                {
                    Message = "Error getting item:" + e.Message,
                    Success = false,
                };
            }
        }
    }
}
