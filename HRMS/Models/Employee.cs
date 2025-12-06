using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS.Models
{
    public class Employee
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string? Email { get; set; }

        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime? BrithDate { get; set; }
        public Department Department { get; set; }

        [ForeignKey("Department")]
        public long? DepartmentId { get; set; }
        public Employee Manager { get; set; }
        [ForeignKey("Manager")]
        public long? ManagerId { get; set; }

        public Lookup Lookup { get; set; }

        [ForeignKey("Lookup")]
        public long PositionId { get; set; }

    }
}
