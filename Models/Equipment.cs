using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Equipment
    {
        [Key]
        public int EquipmentID { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public decimal Cost { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }

        public string Status { get; set; } = "Functional";
    }
}
