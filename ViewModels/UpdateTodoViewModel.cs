using System.ComponentModel.DataAnnotations;

namespace MyTodo.ViewModels
{
  public class UpdateTodoViewModel
  {
    [Required]
    public required string Title { get; set; }
  }
}