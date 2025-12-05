using HRMS.Dto.Employees;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public static List<Employee> employes = new List<Employee>
        {
            new Employee {Id = 1 ,Name = "Alice", Position = "Developer",Email="Alice@gmail.com" ,BrithDate =new DateTime(2000,1,25)},
            new Employee {Id = 1 ,Name = "Bob", Position = "Designer",Email="Bob@gmail.com"  ,BrithDate =new DateTime(1996,1,22)},
            new Employee {Id = 3 ,Name = "Charlie", Position = "Manager",Email="Charlie@gmail.com" ,BrithDate =new DateTime(1991,1,21) }
        };
        [HttpGet("get-by-criteria")]
        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto employeeDto)
        {
            var result = from emp in employes
                         where (employeeDto.position == null || emp.Position.ToUpper().Contains(employeeDto.position.ToUpper()))&&
                         (employeeDto.name == null || emp.Name.ToUpper().Contains(employeeDto.name.ToUpper()))
                         orderby emp.Id descending
                         select new EmployeeDto
                         { Id = emp.Id,
                             Name = emp.FirstName + " " + emp.LastName,
                             Position = emp.Position,
                             BrithDate = emp.BrithDate,
                             Email = emp.Email
                         };
            return Ok(result);//200
        }

        [HttpGet("get-by-id/{id}")]//route parameter
        public IActionResult GetById(long id)
        {
            var emp = employes.FirstOrDefault(e => e.Id == id);
            if (emp == null)
            {
                return NotFound(); // 404
            }
            var employeeDto = new EmployeeDto
            {
                Id = emp.Id,
                Name = emp.FirstName + " " + emp.LastName,
                Position = emp.Position,
                BrithDate = emp.BrithDate,
                Email = emp.Email
            };
            return Ok(employeeDto); // 200
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody] SaveEmployeeDto employeeDto)
        {
            long newId = employes.Any() ? employes.Max(e => e.Id) + 1 :1;
            var newEmployee = new Employee
            {
                Id = newId,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Position = employeeDto.Position,
                BrithDate = employeeDto.BrithDate
            };
            employes.Add(newEmployee);
            return Ok(newEmployee);
        }
        [HttpPut("update")]
        public IActionResult Update ([FromBody] SaveEmployeeDto updateDto)
        {
            var emp = employes.FirstOrDefault(x => x.Id == updateDto.Id);
          
            if (emp == null)
               return NotFound("not found emp");

            emp.FirstName = updateDto.FirstName;
            emp.LastName = updateDto.LastName;
            emp.Email = updateDto.Email;
            emp.BrithDate = updateDto.BrithDate;
            emp.Position = updateDto.Position;

            return Ok("updated succesfully");
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            var employee = employes.FirstOrDefault(x => x.Id == id);
            if(employee ==null)
            {
                return NotFound("Employee Does Not Exist");
            }
            employes.Remove(employee);
            return Ok("deleted succesfully");
        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
             var da = employes.LastOrDefault()?.Id ?? 0;
            return Ok(da);
        }
        [HttpGet("Test2")]
        public IActionResult Test2()
        {
             var da = employes.FirstOrDefault().Id+1;
            return Ok(da);
        }
    }
}
