using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Dtos.Medicine;
using Pharmacy.Models;
using Pharmacy.Services;
using System.Xml.Linq;

namespace Pharmacy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        // GET Requests

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetMedicineDto>>> GetMedicines()
        {
            return Ok(await _pharmacyService.GetMedicines());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMedicineDto>> GetSingleById(long id)
        {
            return Ok(await _pharmacyService.GetSingleById(id));
        }

        // POST Request
        [HttpPost]
        public async Task<ActionResult<List<GetMedicineDto>>> AddMedicine(AddMedicineDto medicine)
        {
            return Ok(await _pharmacyService.AddMedicine(medicine));
        }

        // DELETE Requet
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<GetMedicineDto>>> DeleteMedicine(long id)
        {
            return Ok(await _pharmacyService.DeleteMedicine(id));
        }

        // PUT Request
        [HttpPut]
        public async Task<ActionResult<GetMedicineDto>> UpdateMedicine(Medicine medicine)
        {
            return Ok(await _pharmacyService.UpdateMedicine( medicine));
        }
    }
}
