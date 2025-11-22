namespace HRMS.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? BrithDate { get; set; }
     
    }
}
