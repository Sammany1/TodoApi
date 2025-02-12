using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Services
{
    public class SubtaskService : ISubtaskService
    {
        private readonly AppDbContext _context;

        public SubtaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SubtaskItemDTO?> GetSubtaskAsync(int id)
        {
            var subtask = await _context.Subtasks.FindAsync(id);
            return subtask != null ? new SubtaskItemDTO(subtask) : null;
        }

        public async Task<SubtaskItemDTO> CreateSubtaskAsync(CreateSubtaskDTO createSubtaskDTO)
        {
            var subtask = new Subtask
            {
                TodoId = createSubtaskDTO.TodoId,
                Description = createSubtaskDTO.Description,
            };

            _context.Subtasks.Add(subtask);
            await _context.SaveChangesAsync();

            return new SubtaskItemDTO(subtask);
        }

        public async Task<bool> UpdateSubtaskAsync(int id, UpdateSubtaskDTO updateSubtaskDTO)
        {
            var subtask = await _context.Subtasks.FindAsync(id);
            if (subtask == null)
            {
                return false;
            }

            subtask.Description = updateSubtaskDTO.Description;
            subtask.IsDone = updateSubtaskDTO.IsDone;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubtaskAsync(int id)
        {
            var subtask = await _context.Subtasks.FindAsync(id);
            if (subtask == null)
            {
                return false;
            }

            _context.Subtasks.Remove(subtask);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
