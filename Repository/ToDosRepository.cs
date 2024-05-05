// ToDosRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Interfaces;
using ToDoApi.Models;

namespace ToDoApi.Services
{
    public class ToDosRepository : IToDosRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDos>> GetAllAsync(string userId)
        {
            return await _context.ToDos.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<ToDos> GetByIdAsync(int id)
        {
            return await _context.ToDos.FindAsync(id);
        }

        public async Task CreateAsync(ToDos todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDos todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.ToDos.FindAsync(id);
            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
    }
}
