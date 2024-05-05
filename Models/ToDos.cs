using System.ComponentModel.DataAnnotations;
namespace ToDoApi.Models
{
    public class ToDos
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        //public DateTime DueDate { get; set; }
        //public string Categories { get; set; }
        //public string PriorityLevel { get; set; }
        public string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser User { get; set; } // Navigation property

    }
}
