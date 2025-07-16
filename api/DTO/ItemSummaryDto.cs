namespace api.DTO
{
    public class ItemSummaryDto
    {
        public Guid Id { get; }

        public bool Active { get; }

        public string SerialNumber { get; }

        public string ItemTypeName { get; }

        public string Model { get; }


        public string? AssignedToFullName { get; init; }

        public ItemSummaryDto(Guid id, bool active, string serialNumber, string itemTypeName, string model)
        {
            Id = id;
            Active = active;
            SerialNumber = serialNumber;
            ItemTypeName = itemTypeName;
            Model = model;
        }
    }
}
