namespace Gym.DTOs
{
    // Response DTO
    public class EquipmentDto
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? PurchaseDate { get; set; }
        public decimal Cost { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    // Create DTO - for POST requests
    public class CreateEquipmentDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime? PurchaseDate { get; set; }
        public decimal Cost { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string Status { get; set; } = "Functional";
    }

    // Update DTO - for PUT requests
    public class UpdateEquipmentDto
    {
        public int EquipmentID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? PurchaseDate { get; set; }
        public decimal Cost { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime? NextMaintenanceDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
