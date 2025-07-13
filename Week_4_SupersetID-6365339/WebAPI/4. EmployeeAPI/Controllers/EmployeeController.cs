using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Models;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Department = "IT", Salary = 75000, JoiningDate = new DateTime(2020, 5, 15) },
            new Employee { Id = 2, Name = "Jane Smith", Department = "HR", Salary = 65000, JoiningDate = new DateTime(2019, 3, 10) },
            new Employee { Id = 3, Name = "Michael Johnson", Department = "Finance", Salary = 80000, JoiningDate = new DateTime(2021, 1, 20) },
            new Employee { Id = 4, Name = "Emily Brown", Department = "Marketing", Salary = 70000, JoiningDate = new DateTime(2018, 8, 5) },
            new Employee { Id = 5, Name = "David Wilson", Department = "Operations", Salary = 72000, JoiningDate = new DateTime(2022, 2, 12) }
        };

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return Ok(_employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public ActionResult<Employee> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is null");
            }

            // Assign a new ID (in a real application, this would be handled by the database)
            employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
            
            _employees.Add(employee);
            
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            // Check if the id is valid (greater than 0)
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            // Find the employee with the given id
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            
            // Check if the employee exists
            if (existingEmployee == null)
            {
                return BadRequest("Invalid employee id");
            }

            // Update the employee data
            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.JoiningDate = updatedEmployee.JoiningDate;

            // Return the updated employee
            return Ok(existingEmployee);
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _employees.Remove(employee);
            return NoContent();
        }
    }
}