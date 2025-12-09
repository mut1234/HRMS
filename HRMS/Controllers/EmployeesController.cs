using HRMS.DbContexts;
using HRMS.Dto.Employees;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //public static List<Employee> employes = new List<Employee>
        //{
        //    new Employee {Id = 1 ,Name = "Alice", Position = "Developer",Email="Alice@gmail.com" ,BrithDate =new DateTime(2000,1,25)},
        //    new Employee {Id = 1 ,Name = "Bob", Position = "Designer",Email="Bob@gmail.com"  ,BrithDate =new DateTime(1996,1,22)},
        //    new Employee {Id = 3 ,Name = "Charlie", Position = "Manager",Email="Charlie@gmail.com" ,BrithDate =new DateTime(1991,1,21) }
        //};

        private readonly HRMSContext _dbContext;
        public EmployeesController(HRMSContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("get-by-criteria")]
        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto employeeDto)
        {
            var result =  _dbContext.Employees.AsNoTracking()
                       .Where(emp=>
                         (employeeDto.positionId == null || emp.PositionId == employeeDto.positionId) &&
                         (employeeDto.name == null || emp.FirstName.ToUpper().Contains(employeeDto.name.ToUpper()))
                         ).OrderByDescending(emp => emp.Id)
                         .Select (emp => new EmployeeDto
                         {
                             Id = emp.Id,
                             Name = emp.FirstName + " " + emp.LastName,
                             PositionName = emp.Lookup.Name,
                             PositionId=emp.PositionId,
                             BrithDate = emp.BrithDate,
                             Email = emp.Email,
                             Salary = emp.Salary,
                             DepartmentId = emp.DepartmentId,
                             DepartmentName = emp.Department.Name,
                             MangerId = emp.ManagerId,
                             ManagerName = emp.Manager.FirstName

                         });
        //var result = _dbContext.Employees.AsNoTracking().Where(emp=> employeeDto.positionId ==null || ).ToList();
            return Ok(result);//200
        }

        [HttpGet("get-by-id/{id}")]//route parameter
        public async Task<IActionResult> GetById(long id)
        {
            var emp = await _dbContext.Employees.Select(emp=>new EmployeeDto
            {
                Id = emp.Id,
                Name = emp.FirstName + " " + emp.LastName,
                PositionName =emp.Lookup.Name,
                BrithDate = emp.BrithDate,
                Email = emp.Email,
                Salary =emp.Salary,
                DepartmentId = emp.DepartmentId,
                DepartmentName = emp.Department.Name,
                MangerId = emp.ManagerId,
                ManagerName = emp.Manager.FirstName
                
           
            }).FirstOrDefaultAsync(x=>x.Id == id);

            if (emp == null)
            {
                return NotFound(); // 404
            }

            return Ok(emp); // 200
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] SaveEmployeeDto employeeDto)
        {
         //   long newId = _dbContext.Employees.Any() ? employes.Max(e => e.Id) + 1 :1;
            var newEmployee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PositionId = employeeDto.PositionId,
                BrithDate = employeeDto.BirthDate,
                Salary =employeeDto.Salary,
                DepartmentId=employeeDto.DepartmentId,
                ManagerId=employeeDto.ManagerId
            };
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update ([FromBody] SaveEmployeeDto updateDto)
        {
            var emp = _dbContext.Employees.FirstOrDefault(x => x.Id == updateDto.Id);
          
            if (emp == null)
               return NotFound("not found emp");

            emp.FirstName = updateDto.FirstName;
            emp.LastName = updateDto.LastName;
            emp.Email = updateDto.Email;
            emp.BrithDate = updateDto.BirthDate;
         //   emp.Position = updateDto.Position;

            return Ok("updated succesfully");
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x => x.Id == id);
            if(employee ==null)
            {
                return NotFound("Employee Does Not Exist");
            }
            _dbContext.Employees.Remove(employee);
            return Ok("deleted succesfully");
        }

        #region testing
        [HttpGet("Test")]
        public IActionResult Test()
        {
             var da = _dbContext.Employees.LastOrDefault()?.Id ?? 0;
            return Ok(da);
        }
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
             var da = _dbContext.Employees.FirstOrDefault().Id+1;
            return Ok(da);
        }
        #endregion
    }
}
