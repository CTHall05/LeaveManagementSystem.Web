using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Data
{
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string? LeaveTypeName { get; set; }
        public int NumberOfDays { get; set; }
    }
}
