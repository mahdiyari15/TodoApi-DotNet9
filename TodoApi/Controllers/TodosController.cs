using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todos = new List<TodoItem>
        {
            new TodoItem {Id = 1, Title = "Learn ASP.NET Core", IsDone = true},
            new TodoItem {Id = 2, Title = "Build a Web API", IsDone = false}
        };
        private static int _nextId = 3;
        //[HttpGet("{id}")]
        [HttpGet("gettodo")]
        //public IActionResult GetTodoById(int id)
        public IActionResult GetTodoById()
        {
            //var todo = _todos.Find(t => t.Id == id);
            //if (todo == null)
            //{
            //    return NotFound();
            //}
            return Ok("Hello");
        }
        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoItem newTodo)
        {
            //newTodo.Id = _nextId++;
            //_todos.Add(newTodo);
            //return CreatedAtAction(nameof(GetAllTodos), newTodo);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTodo([FromRoute] int id, [FromBody] TodoItem updatedTodo)
        {
            var todo = _todos.Find(t => t.Id == id);
            if ((todo == null))
            {
                return NotFound();
            }
            todo.Title = updatedTodo.Title;
            todo.IsDone = updatedTodo.IsDone;
            return Ok(todo);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = _todos.Find(t => t.Id ==id);
            if(todo == null)
            {
                return NotFound();
            }
            _todos.Remove(todo);
            return NoContent();
        }
    }
}