using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTodo.Data;
using MyTodo.Models;
using MyTodo.ViewModels;

namespace MyTodo.Controller
{
  [ApiController]
  [Route("v1")]
  public class TodoController : ControllerBase
  {
    [HttpGet]
    [Route("todos")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
      var todos = await context.Todos.AsNoTracking().ToListAsync(); // AsNoTracking makes LINQ faster in this case
      return Ok(todos);
    }

    [HttpGet]
    [Route("todos/{id}")]
    public async Task<IActionResult> GetByIdAsync(
      [FromServices] AppDbContext context,
      [FromRoute] int id)
    {
      var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
      return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost("todos")] // Can be done like that
    public async Task<IActionResult> PostAsync(
      [FromServices] AppDbContext context,
      [FromBody] CreateTodoViewModel model)
    {
      if (!ModelState.IsValid) return BadRequest();

      var todo = new Todo
      {
        date = DateTime.Now,
        Done = false,
        Title = model.Title
      };

      try
      {
        await context.Todos.AddAsync(todo); // it just saves in memory, do not commit it
        await context.SaveChangesAsync(); // it will commit changes
        return Created($"v1/todos/{todo.Id}", todo);
      }
      catch (System.Exception)
      {
        return BadRequest();
      }
    }
  }
}