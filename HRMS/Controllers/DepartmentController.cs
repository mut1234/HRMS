using HRMS.Dto.Dea;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController :ControllerBase
    {
        public List<Department> departments = new List<Department>
        {
            new Department {Id = 1 ,Name = "IT", Description = "Information Technology", FloorNumber = 5},
            new Department {Id = 2 ,Name = "HR", Description = "Human Resources", FloorNumber = 3},
            new Department {Id = 3 ,Name = "Finance", Description = "Financial Department", FloorNumber = 4}
        };

        [HttpGet("get-by-criteria")]
        public IActionResult GetByCriteria([FromQuery] FiliterDepartmentDto FiliterDepartmentDto)
        {
            var result = from dept in departments
                         where (FiliterDepartmentDto.Name == null || dept.Name.ToUpper().Contains(FiliterDepartmentDto.Name.ToUpper())) &&
                          (FiliterDepartmentDto.FloorNumber == null || dept.FloorNumber == FiliterDepartmentDto.FloorNumber)
                         orderby dept.Id descending
                         select new DepartmentDto 
                         {
                           Id=dept.Id,
                           Name=dept.Name,
                           Descrip=dept.Description
                         };
            return Ok(result); // 200
        }
    }
}
    