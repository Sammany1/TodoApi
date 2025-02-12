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
    public class SubtaskController : ControllerBase
    {
        private readonly ISubtaskService _subtaskService;

        public SubtaskController(ISubtaskService subtaskService)
        {
            _subtaskService = subtaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubtask(int id)
        {
            var subtask = await _subtaskService.GetSubtaskAsync(id);
            if (subtask == null)
            {
                return NotFound();
            }
            return Ok(subtask);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubtask([FromBody] CreateSubtaskDTO createSubtaskDTO)
        {
            var subtask = await _subtaskService.CreateSubtaskAsync(createSubtaskDTO);
            return CreatedAtAction(nameof(GetSubtask), new { id = subtask.Id }, subtask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubtask(int id, [FromBody] UpdateSubtaskDTO updateSubtaskDTO)
        {
            var updated = await _subtaskService.UpdateSubtaskAsync(id, updateSubtaskDTO);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubtask(int id)
        {
            var deleted = await _subtaskService.DeleteSubtaskAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}