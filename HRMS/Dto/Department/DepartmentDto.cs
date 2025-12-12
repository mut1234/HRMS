namespace HRMS.Dto.Dea
{
    public class DepartmentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? LookupName { get; set; }
        public long? TypeId { get; set; }
        public int FloorNumber { get; set; }
    }
}
