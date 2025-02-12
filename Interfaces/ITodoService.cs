using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DTOs;

namespace TodoApi.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItemDTO?> GetTodoAsync(int id);
        Task<TodoItemDTO> CreateTodoAsync(CreateTodoDTO createTodoDTO);
        Task<bool> UpdateTodoAsync(int id, UpdateTodoDTO updateTodoDTO);
        Task<bool> DeleteTodoAsync(int id);
    }
}
