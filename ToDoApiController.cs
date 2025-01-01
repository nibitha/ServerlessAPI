using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public ToDoController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ToDo item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.id))
            {
                return BadRequest("Invalid Input data");
            }
            try
            {
                var existingItem = await _context.LoadAsync<ToDo>(item.id);
                if (existingItem != null)
                {
                    return BadRequest($"Item with {item.id} already exists.");
                }
                await _context.SaveAsync<ToDo>(item);
                return CreatedAtAction(nameof(CreateItem), new { id = item.id }, item);

            }
            catch
            {
                return StatusCode(500, "An error occurred while creating the item");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] ToDo updatedItem)
        {
            if (updatedItem == null || id != updatedItem.id)
            {
                return BadRequest("Invalid Input data");
            }
            try
            {
                var existingItem = await _context.LoadAsync<ToDo>(id);
                if (existingItem == null)
                {
                    return NotFound($"Item not found.");
                }
                await _context.SaveAsync<ToDo>(updatedItem);
                return Ok("Item Updated Successfully");

            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the item");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] string name = null, [FromQuery] string status = null)
        {
            try
            {
                var conditions = new List<ScanCondition>();
                if (name != null)
                {
                    conditions.Add(new ScanCondition("Name", ScanOperator.Equal, name));
                }
                if (status != null)
                {
                    conditions.Add(new ScanCondition("Status", ScanOperator.Equal, status));
                }


                var results = await _context.ScanAsync<ToDo>(conditions).GetRemainingAsync();
                if (results.Count == 0)
                {
                    return NoContent();
                }
                return Ok(results);
            }
            catch
            {
                return StatusCode(500, "An error occurred while fetching the items");
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(string id)
        {
            try
            {
                if (id != null)
                {
                    var existingToDo = await _context.LoadAsync<ToDo>(id);
                    if (existingToDo != null)
                    {
                        return Ok(existingToDo);
                    }
                }
                return NotFound("Item Not Found");
            }

            catch
            {
                return StatusCode(500, "An error occurred while fetching the item");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                if (id != null)
                {
                    var existingToDo = await _context.LoadAsync<ToDo>(id);
                    if (existingToDo != null)
                    {
                        await _context.DeleteAsync(existingToDo);
                        return Ok("Item deleted successfully");
                    }
                }
                return NotFound("Item not found");
            }

            catch
            {
                return StatusCode(500, "An error occurred while fetching the item");
            }
        }
    }
}
