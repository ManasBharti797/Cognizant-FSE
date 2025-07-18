using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,POC")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var employees = new[]
            {
                new { Id = 1, Name = "John Doe", Department = "IT" },
                new { Id = 2, Name = "Jane Smith", Department = "HR" },
                new { Id = 3, Name = "Bob Johnson", Department = "Finance" }
            };

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // In a real application, you would fetch the employee from a database
            var employee = new { Id = id, Name = "John Doe", Department = "IT" };
            return Ok(employee);
        }
    }
}