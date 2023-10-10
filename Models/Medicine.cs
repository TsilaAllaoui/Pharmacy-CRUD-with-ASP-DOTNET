using System.Runtime.InteropServices;

namespace Pharmacy.Models
{
    public class Medicine
    {
        public long Id { get; set; }
        public string Name { get; set; } = "Unknown";

        public int Price { get; set; }
        public long Quantity { get; set; }

        public MedicineType Type { get; set; }
    }
}
