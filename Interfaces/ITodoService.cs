using System.Collections.Generic;
using System.Threading.Tasks;
using TraskioApi.DTOs;

namespace TraskioApi.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItemDTO?> GetTodoAsync(int id);
        Task<TodoItemDTO> CreateTodoAsync(CreateTodoDTO createTodoDTO);
        Task<bool> UpdateTodoAsync(int id, UpdateTodoDTO updateTodoDTO);
        Task<bool> DeleteTodoAsync(int id);
    }
}
