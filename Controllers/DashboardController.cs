using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TodoApi.Authorization;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        private int GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
            {
                throw new System.UnauthorizedAccessException("Invalid user token");
            }
            return id;
        }

        [HttpGet("{id}")]
        [DashboardOwner]
        public async Task<IActionResult> GetDashboard(int id)
        {
            var dashboard = await _dashboardService.GetDashboardAsync(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            return Ok(dashboard);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserDashboards()
        {
            int userId = GetUserId();
            var dashboards = await _dashboardService.GetUserDashboardsAsync(userId);
            return Ok(dashboards);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDashboard([FromBody] CreateDashboardDTO createDashboardDTO)
        {
            createDashboardDTO.UserId = GetUserId();
            var dashboard = await _dashboardService.CreateDashboardAsync(createDashboardDTO);
            return CreatedAtAction(nameof(GetDashboard), new { id = dashboard.Id }, dashboard);
        }

        [HttpPut("{id}")]
        [DashboardOwner]
        public async Task<IActionResult> UpdateDashboard(int id, [FromBody] UpdateDashboardDTO updateDashboardDTO)
        {
            var updated = await _dashboardService.UpdateDashboardAsync(id, updateDashboardDTO);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [DashboardOwner]
        public async Task<IActionResult> DeleteDashboard(int id)
        {
            var deleted = await _dashboardService.DeleteDashboardAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}