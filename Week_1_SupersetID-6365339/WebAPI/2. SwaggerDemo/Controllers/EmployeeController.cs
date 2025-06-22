using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SwaggerDemo.Controllers
{
    [Route("api/Emp")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "IT", Salary = 75000 },
            new Employee { Id = 2, Name = "Jane Smith", Department = "HR", Salary = 65000 },
            new Employee { Id = 3, Name = "Bob Johnson", Department = "Finance", Salary = 80000 }
        };

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_employees);
        }

        [HttpGet("{id}")]
        [ActionName("GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            _employees.Add(employee);
            return CreatedAtAction("GetEmployeeById", new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var existingEmployee = _employees.Find(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Department = employee.Department;
            existingEmployee.Salary = employee.Salary;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Employee>> GetEmployeesByDepartment(string department)
        {
            var employees = _employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(employees);
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}