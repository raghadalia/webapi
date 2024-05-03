using ToDoApi.Models;

namespace ToDoApi.interfaces
{
   
        public interface IToDosRepository { 
        Task<IEnumerable<ToDos>> GetAllAsync(string userId);
        Task<ToDos> GetByIdAsync(int id);
        Task CreateAsync(ToDos toDos);
        Task UpdateAsync(ToDos toDos);
        Task DeleteAsync(int id);
        bool Exists(int id);
        }
    
}
