using Microsoft.AspNetCore.Mvc;
using TraskioApi.DTOs;
using TraskioApi.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TraskioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDTO createTodoDTO)
        {
            var todo = await _todoService.CreateTodoAsync(createTodoDTO);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDTO updateTodoDTO)
        {
            var updated = await _todoService.UpdateTodoAsync(id, updateTodoDTO);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var deleted = await _todoService.DeleteTodoAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}