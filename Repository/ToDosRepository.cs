using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.interfaces;
using ToDoApi.Models;

namespace ToDoApi.Repository
{
    public class ToDosRepository : IToDosRepository
    {
        private readonly ApplicationDbContext _context;

        public ToDosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDos>> GetAllAsync(string userId )
        {
            if (userId != null)
            {
                return await _context.ToDos.Where(todo => todo.User.Id == userId).ToListAsync();
            }
            else
            {
                return await _context.ToDos.Include(t => t.User).ToListAsync();
            }
        }

        public async Task<ToDos> GetByIdAsync(int id)
        {
            return await _context.ToDos.Include(t => t.User).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task CreateAsync(ToDos toDos)
        {
            _context.ToDos.Add(toDos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ToDos toDos)
        {
            _context.ToDos.Update(toDos);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var toDos = await _context.ToDos.FindAsync(id);
            if (toDos != null)
            {
                _context.ToDos.Remove(toDos);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }

       
    }

}
