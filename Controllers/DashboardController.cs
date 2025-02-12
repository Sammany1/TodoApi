using Microsoft.AspNetCore.Mvc;
using TraskioApi.DTOs;
using TraskioApi.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TraskioApi.Controllers
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDashboard(int id)
        {
            var dashboard = await _dashboardService.GetDashboardAsync(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            return Ok(dashboard);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDashboard([FromBody] CreateDashboardDTO createDashboardDTO)
        {
            var dashboard = await _dashboardService.CreateDashboardAsync(createDashboardDTO);
            return CreatedAtAction(nameof(GetDashboard), new { id = dashboard.Id }, dashboard);
        }

        [HttpPut("{id}")]
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