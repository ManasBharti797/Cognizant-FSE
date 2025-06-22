using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApi.Controllers
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Department { get; set; }
        
        public decimal Salary { get; set; }
    }

    [Route("api/Emp")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Smith", Department = "IT", Salary = 75000 },
            new Employee { Id = 2, Name = "Jane Doe", Department = "HR", Salary = 65000 },
            new Employee { Id = 3, Name = "Bob Johnson", Department = "Finance", Salary = 80000 }
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = _employees.Find(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            var index = _employees.IndexOf(existingEmployee);
            _employees[index] = employee;

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employees.Find(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _employees.Remove(employee);
            return NoContent();
        }

        [HttpGet("department/{department}")]
        [ActionName("GetEmployeesByDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            var employees = _employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(employees);
        }
        [HttpGet("salary/above/{threshold}")]
        [ActionName("GetEmployeesAboveSalary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        public ActionResult<IEnumerable<Employee>> GetEmployeesAboveSalary(decimal threshold)
        {
            var employees = _employees.Where(e => e.Salary > threshold).ToList();
            return Ok(employees);
        }
    }
}