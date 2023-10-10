using Pharmacy.Models;
using System.Threading.Tasks;

namespace Pharmacy.Services
{
    public interface IPharmacyService
    {
        Task<ServiceResponse<List<Medicine>>> GetMedicines();
        Task<ServiceResponse<Medicine>> GetSingleById(long id);
        Task<ServiceResponse<List<Medicine>>> AddMedicine(Medicine medicine);
        Task<ServiceResponse<Medicine>> DeleteMedicine(long id);
    }
}
 