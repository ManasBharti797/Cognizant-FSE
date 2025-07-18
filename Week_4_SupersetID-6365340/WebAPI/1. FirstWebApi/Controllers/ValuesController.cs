using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied");
            }

            if (id > 10)
            {
                return NotFound($"Value with ID {id} not found");
            }

            return Ok($"value{id}");
        }

        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be empty");
            }

            return CreatedAtAction(nameof(Get), new { id = 11 }, value);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied");
            }

            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("Value cannot be empty");
            }

            if (id > 10)
            {
                return NotFound($"Value with ID {id} not found");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID supplied");
            }

            if (id > 10)
            {
                return NotFound($"Value with ID {id} not found");
            }
            return NoContent();
        }
    }
}