using System.Collections.Generic;
using System.Threading.Tasks;
using TraskioApi.DTOs;

namespace TraskioApi.Interfaces
{
    public interface ISubtaskService
    {
        Task<SubtaskItemDTO?> GetSubtaskAsync(int id);
        Task<SubtaskItemDTO> CreateSubtaskAsync(CreateSubtaskDTO createSubtaskDTO);
        Task<bool> UpdateSubtaskAsync(int id, UpdateSubtaskDTO updateSubtaskDTO);
        Task<bool> DeleteSubtaskAsync(int id);
    }
}
