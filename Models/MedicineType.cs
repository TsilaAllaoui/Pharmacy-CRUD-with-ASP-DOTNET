using System.Text.Json.Serialization;

namespace Pharmacy.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MedicineType
    {
        Liquid,
        Tablet,
        Capsule,
        Suppositorie,
        Drop,
        Inhaler,
        Injection
    }
}
