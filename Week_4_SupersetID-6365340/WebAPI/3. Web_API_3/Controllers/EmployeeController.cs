using EmployeeAPI.Filters;
using EmployeeAPI.Models;
// Add NuGet package: Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthFilter] // Apply the custom auth filter to all actions in this controller
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        [AllowAnonymous] // Allow anonymous access to this action
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Employee>> Get()
        {
            try
            {
                // Intentionally throw an exception to demonstrate the exception filter
                if (DateTime.Now.Second % 10 == 0) // Randomly throw an exception
                {
                    throw new Exception("This is a test exception to demonstrate the exception filter");
                }

                var employees = GetStandardEmployeeList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get method");
                throw; // This will be caught by the CustomExceptionFilter
            }
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Employee> Get(int id)
        {
            var employees = GetStandardEmployeeList();
            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Create a new employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Created employee</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee cannot be null");
            }

            // In a real application, we would add the employee to a database
            // For this demo, we'll just return the employee with an assigned Id
            employee.Id = 100; // Assign a dummy Id

            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Update an existing employee
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="employee">Updated employee object</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest("Employee Id mismatch");
            }

            var employees = GetStandardEmployeeList();
            var existingEmployee = employees.FirstOrDefault(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            // In a real application, we would update the employee in a database
            // For this demo, we'll just return NoContent

            return NoContent();
        }

        /// <summary>
        /// Get a standard employee
        /// </summary>
        /// <returns>Standard employee</returns>
        [HttpGet("standard")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        public ActionResult<Employee> GetStandard()
        {
            var employees = GetStandardEmployeeList();
            return employees.First();
        }

        /// <summary>
        /// Get a list of standard employees
        /// </summary>
        /// <returns>List of standard employees</returns>
        private List<Employee> GetStandardEmployeeList()
        {
            // Create a list of employees with sample data
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Salary = 60000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "IT" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 1, Name = "C#" },
                        new Skill { Id = 2, Name = "ASP.NET Core" }
                    },
                    DateOfBirth = new DateTime(1990, 1, 1)
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Salary = 70000,
                    Permanent = true,
                    Department = new Department { Id = 2, Name = "HR" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 3, Name = "Communication" },
                        new Skill { Id = 4, Name = "Management" }
                    },
                    DateOfBirth = new DateTime(1985, 5, 15)
                },
                new Employee
                {
                    Id = 3,
                    Name = "Bob Johnson",
                    Salary = 55000,
                    Permanent = false,
                    Department = new Department { Id = 3, Name = "Finance" },
                    Skills = new List<Skill>
                    {
                        new Skill { Id = 5, Name = "Accounting" },
                        new Skill { Id = 6, Name = "Excel" }
                    },
                    DateOfBirth = new DateTime(1992, 8, 21)
                }
            };

            return employees;
        }
    }
}