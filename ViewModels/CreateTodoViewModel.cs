using System.ComponentModel.DataAnnotations;

namespace MyTodo.ViewModels
{
  public class CreateTodoViewModel
  {
    [Required]
    public required string Title { get; set; }
  }
}