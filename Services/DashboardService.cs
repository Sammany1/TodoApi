using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardItemDTO?> GetDashboardAsync(int id)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);
            return dashboard != null ? new DashboardItemDTO(dashboard) : null;
        }

        public async Task<DashboardItemDTO> CreateDashboardAsync(CreateDashboardDTO createDashboardDTO)
        {
            var dashboard = new Dashboard
            {
                Name = createDashboardDTO.Name,
                Description = createDashboardDTO.Description,
                UserId = createDashboardDTO.UserId,
                Color = createDashboardDTO.Color
            };

            _context.Dashboards.Add(dashboard);
            await _context.SaveChangesAsync();

            return new DashboardItemDTO(dashboard);
        }

        public async Task<bool> UpdateDashboardAsync(int id, UpdateDashboardDTO updateDashboardDTO)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);
            if (dashboard == null)
            {
                return false;
            }

            dashboard.Name = updateDashboardDTO.Name;
            dashboard.Description = updateDashboardDTO.Description;
            dashboard.Color = updateDashboardDTO.Color;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDashboardAsync(int id)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);
            if (dashboard == null)
            {
                return false;
            }

            _context.Dashboards.Remove(dashboard);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateOwnershipAsync(int dashboardId, int userId)
        {
            var dashboard = await _context.Dashboards
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == dashboardId && d.UserId == userId);
            return dashboard != null;
        }

        public async Task<IEnumerable<DashboardItemDTO>> GetUserDashboardsAsync(int userId)
        {
            return await _context.Dashboards
                .AsNoTracking()
                .TagWith("GetUserDashboards")
                .Where(d => d.UserId == userId)
                .Select(d => new DashboardItemDTO 
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    UserId = d.UserId,
                    Color = d.Color,
                    CreatedAt = d.CreatedAt
                })
                .OrderBy(d => d.Id)
                .ToListAsync();
        }
    }
}
