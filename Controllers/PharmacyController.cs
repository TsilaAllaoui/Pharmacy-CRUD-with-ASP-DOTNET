using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<Medicine>>> GetMedicines()
        {
            return Ok(await _pharmacyService.GetMedicines());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetSingleById(long id)
        {
            return Ok(await _pharmacyService.GetSingleById(id));
        }

        // POST Request
        [HttpPost]
        public async Task<ActionResult<List<Medicine>>> AddMedicine(Medicine medicine)
        {
            return Ok(await _pharmacyService.AddMedicine(medicine));
        }

        // DELETE Requet
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Medicine>>> DeleteMedicine(long id)
        {
            return Ok(await _pharmacyService.DeleteMedicine(id));
        }
    }
}
