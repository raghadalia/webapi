using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Models
{
    public class ApplicationUser : IdentityUser
    {

        public List<ToDos> Tasks { get; set; } = new List<ToDos>();
    }
}
