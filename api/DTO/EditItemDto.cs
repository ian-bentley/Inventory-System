using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class EditItemDto
    {
        [Required(ErrorMessage = "The id of the item is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The active state of the item is required.")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "The serial number of an item is required.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "The item type is of an item is required.")]
        public int ItemTypeId { get; set; }

        [Required(ErrorMessage = "The model name of an item is required.")]
        public string Model { get; set; }

        public Guid? AssignedToId { get; set; }

        public string? Notes { get; set; }

        public EditItemDto(bool active, string serialNumber, int itemTypeId, string model)
        {
            Active = active;
            SerialNumber = serialNumber;
            ItemTypeId = itemTypeId;
            Model = model;
        }
    }
}
