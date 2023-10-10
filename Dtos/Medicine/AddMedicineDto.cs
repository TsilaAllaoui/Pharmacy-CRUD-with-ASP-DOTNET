using Pharmacy.Models;

namespace Pharmacy.Dtos.Medicine
{
    public class AddMedicineDto
    {
        public string Name { get; set; } = "Unknown";

        public int Price { get; set; }
        public long Quantity { get; set; }

        public MedicineType Type { get; set; }
    }
}
