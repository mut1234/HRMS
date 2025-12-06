using HRMS.Dto.Dea;
using HRMS.Dto.Department;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
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
                             Id = dept.Id,
                             Name = dept.Name,
                             Descrip = dept.Description,
                             FloorNumber = dept.FloorNumber,
                         };
            return Ok(result); // 200
        }
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(long id)
        {
            var dep = departments.Select(x => new Department
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FloorNumber = x.FloorNumber
            }).FirstOrDefault(x => x.Id == id);

            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            return Ok(dep);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] SaveDepartmenDto saveDto)
        {
            var dep = new Department
            {
                Id = departments.LastOrDefault()?.Id + 1 ?? 1,
                Name = saveDto.Name,
                Description = saveDto.Description,
            };
            departments.Add(dep);
            return Ok(dep);
        }
        [HttpPut("update")]
        public IActionResult Update([FromBody] SaveDepartmenDto saveDto)
        {
            var dep = departments.FirstOrDefault(x => x.Id == saveDto.Id);
            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            dep.Name = saveDto.Name;
            dep.Description = saveDto.Description;
            dep.FloorNumber = saveDto.FloorNumber;
            return Ok(dep);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var dep = departments.FirstOrDefault(x => x.Id == id);
            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            departments.Remove(dep);
            return Ok();
        }

    }
}
    