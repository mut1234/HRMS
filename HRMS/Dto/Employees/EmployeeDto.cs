namespace HRMS.Dto.Employees
{
    public class EmployeeDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public long? PositionId { get; set; }
        public string? PositionName { get; set; }
        public DateTime? BrithDate { get; set; }   
        public string? Email { get; set; }
        public Decimal? Salary { get; set; }
        public Decimal? MangerId { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
