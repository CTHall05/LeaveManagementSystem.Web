﻿namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class IndexVM
    {
        public int Id { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
    }
}