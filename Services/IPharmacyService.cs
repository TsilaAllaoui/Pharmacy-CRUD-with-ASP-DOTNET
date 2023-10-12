using Pharmacy.Dtos.Medicine;
using Pharmacy.Models;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IPharmacyService
    {
        Task<ServiceResponse<List<GetMedicineDto>>> GetMedicines();
        Task<ServiceResponse<GetMedicineDto>> GetSingleById(long id);
        Task<ServiceResponse<List<GetMedicineDto>>> AddMedicine(AddMedicineDto medicine);
        Task<ServiceResponse<GetMedicineDto>> DeleteMedicine(long id);

        Task<ServiceResponse<GetMedicineDto>> UpdateMedicine(Medicine medicine);
    }
}
 