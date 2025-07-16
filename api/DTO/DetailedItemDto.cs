using api.Models;

namespace api.DTO
{
    public class DetailedItemDto
    {
        public Guid Id { get; }

        public bool Active { get; }

        public string SerialNumber { get; }

        public string ItemTypeName { get; }

        public string Model { get; set; }

        public string? AssignedToFullName { get; set; }

        public string? Notes { get; set; }

        public DetailedItemDto(Guid id, bool active, string serialNumber, string itemTypeName, string model)
        {
            Id = id;
            Active = active;
            SerialNumber = serialNumber;
            ItemTypeName = itemTypeName;
            Model = model;
        }
    }
}
