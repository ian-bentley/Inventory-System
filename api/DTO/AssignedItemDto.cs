namespace api.DTO
{
    public class AssignedItemDto
    {
        public string SerialNumber { get; }

        public string ItemTypeName { get; }

        public string Model { get; }

        public AssignedItemDto(string serialNumber, string itemTypeName, string model)
        {
            SerialNumber = serialNumber;
            ItemTypeName = itemTypeName;
            Model = model;
        }
    }
}
