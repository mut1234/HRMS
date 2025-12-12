using HRMS.DbContexts;
using HRMS.Dto.Dea;
using HRMS.Dto.Department;
using HRMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //public List<Department> _dbContext.Departments = new List<Department>
        //{
        //    new Department {Id = 1 ,Name = "IT", Description = "Information Technology", FloorNumber = 5},
        //    new Department {Id = 2 ,Name = "HR", Description = "Human Resources", FloorNumber = 3},
        //    new Department {Id = 3 ,Name = "Finance", Description = "Financial Department", FloorNumber = 4}
        //};

        public readonly HRMSContext _dbContext;
        public DepartmentController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("get-by-criteria")]
        public async Task<IActionResult> GetByCriteria([FromQuery] FiliterDepartmentDto FiliterDepartmentDto)
        {
            var result = await _dbContext.Departments
                .AsNoTracking()
                .Where(d =>
                    (FiliterDepartmentDto.Name == null ||
                     d.Name.ToUpper().Contains(FiliterDepartmentDto.Name.ToUpper())) &&
                    (FiliterDepartmentDto.FloorNumber == null ||
                     d.FloorNumber == FiliterDepartmentDto.FloorNumber))
                .OrderByDescending(d => d.Id)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    FloorNumber = d.FloorNumber,
                    TypeId = d.TypeId,
                    LookupName = d.Lookup.Name
                })
                .ToListAsync();

            return Ok(result); // 200
        }
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(long id)
        {
            var dep = _dbContext.Departments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FloorNumber = x.FloorNumber,
                TypeId = x.TypeId,
                LookupName = x.Lookup.Name
            }).FirstOrDefault(x => x.Id == id);

            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            return Ok(dep);
        }
        [Authorize("HR,Admin")]
        [HttpPost("Add")]
        public IActionResult Add([FromBody] SaveDepartmenDto saveDto)
        {
            var dep = new Department
            {
                Name = saveDto.Name,
                Description = saveDto.Description,
                TypeId= saveDto.TypeId
            };
            _dbContext.Departments.Add(dep);
            _dbContext.SaveChanges();

            return Ok(dep);
        }
        [Authorize("HR,Admin")]
        [HttpPut("update")]
        public IActionResult Update([FromBody] SaveDepartmenDto saveDto)
        {
            var dep = _dbContext.Departments.FirstOrDefault(x => x.Id == saveDto.Id);
            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            dep.Name = saveDto.Name;
            dep.Description = saveDto.Description;
            dep.FloorNumber = saveDto.FloorNumber;
            dep.TypeId = saveDto.TypeId;
            _dbContext.SaveChanges();
            return Ok(dep);
        }
        [Authorize("HR,Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var dep = _dbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (dep == null)
            {
                return NotFound("dep not exist");
            }
            var hasEmp = _dbContext.Employees.Any(x => x.DepartmentId == id);
            if (hasEmp)
            {
                return BadRequest("Cannot delete department with assigned employees.");
            }
            _dbContext.Departments.Remove(dep);
            _dbContext.SaveChanges();

            return Ok();
        }

    }
}
    