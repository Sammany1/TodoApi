using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DTOs;

namespace TodoApi.Interfaces
{
    public interface ISubtaskService
    {
        Task<SubtaskItemDTO?> GetSubtaskAsync(int id);
        Task<SubtaskItemDTO> CreateSubtaskAsync(CreateSubtaskDTO createSubtaskDTO);
        Task<bool> UpdateSubtaskAsync(int id, UpdateSubtaskDTO updateSubtaskDTO);
        Task<bool> DeleteSubtaskAsync(int id);
    }
}
