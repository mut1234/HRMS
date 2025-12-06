namespace HRMS.Dto.Department
{
    public class SaveDepartmenDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int FloorNumber { get; set; }
    }
}
