using api.Models;
using System.ComponentModel.DataAnnotations;

namespace api.DTO
{
    public class NewItemEventDto
    {
        public NewItemEventDto()
        {
        }

        [Required(ErrorMessage = "Item id for an item event is required.")]
        public Guid ItemId { get; set; }

        [Required(ErrorMessage = "Employee id for an item event is required.")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Event type id for an item event is required.")]
        public int EventTypeId { get; set; }

        [Required(ErrorMessage = "A date for an item event is required.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "A reason for an item event is required.")]
        public string Reason { get; set; }

        public NewItemEventDto(Guid itemId, Guid employeeId, int eventTypeId, DateTime dateTime, string reason)
        {
            ItemId = itemId;
            EmployeeId = employeeId;
            EventTypeId = eventTypeId;
            DateTime = dateTime;
            Reason = reason;
        }
    }
}
