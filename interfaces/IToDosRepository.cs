using ToDoApi.Models;

namespace ToDoApi.Interfaces
{
    public interface IToDosRepository
    {
        Task<List<ToDos>> GetAllAsync(string userId);
        Task<ToDos> GetByIdAsync(int id);
        Task CreateAsync(ToDos todo);
        Task UpdateAsync(ToDos todo);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}
