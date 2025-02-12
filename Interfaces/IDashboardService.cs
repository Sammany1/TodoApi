using System.Collections.Generic;
using System.Threading.Tasks;
using TraskioApi.DTOs;

namespace TraskioApi.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardItemDTO?> GetDashboardAsync(int id);
        Task<DashboardItemDTO> CreateDashboardAsync(CreateDashboardDTO createDashboardDTO);
        Task<bool> UpdateDashboardAsync(int id, UpdateDashboardDTO updateDashboardDTO);
        Task<bool> DeleteDashboardAsync(int id);
    }
}
