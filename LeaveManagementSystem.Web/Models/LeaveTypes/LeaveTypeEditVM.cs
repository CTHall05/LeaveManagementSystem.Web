using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeEditVM :BaseLeaveTypeVM
    {
        [Required]
        [StringLength(150, MinimumLength = 4, ErrorMessage = "You have violated the length requirement.")]
        public string LeaveTypeName { get; set; }

        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
