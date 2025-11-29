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
            new TodoItem { Id = 1, Title = "کار خیلی مهم", IsDone = false, Priority = 1, CreatedItem = new DateTime(2025, 9, 23, 12, 33, 56)},
            new TodoItem { Id = 2, Title = "کار معمولی", IsDone = false, Priority = 2, CreatedItem = DateTime.Now },
            new TodoItem { Id = 3, Title = "کار فرعی", IsDone = false, Priority = 3, CreatedItem = new DateTime(2025, 2, 4, 23, 43, 13)}
        };

        private static int _nextId = 4;
        // GET: api/Todo
        [HttpGet]   
        public IActionResult GetAllTodos()
        {
            var sortedTodos = _todos.OrderBy(t => t.Priority).ToList();
            return Ok(sortedTodos);
        }

       

        // GET: api/Todo/5
        [HttpGet("{id}")] 
        public IActionResult GetTodoById(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        // POST: api/Todo
        [HttpPost]
        public IActionResult CreateTodo([FromBody] TodoItem newTodo)
        {
            // check priority
            if (!newTodo.IsDone && _todos.Any(t => !t.IsDone && t.Priority == newTodo.Priority))
            {
                return BadRequest($"اولویت شماره {newTodo.Priority} قبلاً رزرو شده است. لطفاً عدد دیگری انتخاب کنید.");
            }

            // max priority
            int maxPriority = _todos.Where(t => !t.IsDone).Max(t => (int?)t.Priority) ?? 0;

            if (newTodo.Priority > maxPriority + 1)
            {
                return BadRequest($"اولویت نمی‌تواند بیشتر از {maxPriority + 1} باشد. (پرش مجاز نیست)");
            }

            newTodo.Id = _nextId++;
            // default create time
            if (!newTodo.CreatedItem.HasValue) newTodo.CreatedItem = DateTime.Now;

            _todos.Add(newTodo);

            return CreatedAtAction(nameof(GetTodoById), new { id = newTodo.Id }, newTodo);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
        {
            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo == null) return NotFound();

            
            bool isDuplicate = _todos.Any(t => t.Id != id && !t.IsDone && t.Priority == updatedTodo.Priority);
            if (isDuplicate)
            {
                return BadRequest($"اولویت {updatedTodo.Priority} تکراری است. لطفاً عدد دیگری انتخاب کنید.");
            }

            
            int maxPriority = _todos.Where(t => !t.IsDone).Max(t => (int?)t.Priority) ?? 0;

            
            if (updatedTodo.Priority > maxPriority + 1)
            {
                return BadRequest($"اولویت نمی‌تواند بیشتر از {maxPriority + 1} باشد. (پرش مجاز نیست)");
            }

            

            // save last info
            bool wasDone = existingTodo.IsDone;
            int removedPriority = existingTodo.Priority;

            // update info
            existingTodo.Title = updatedTodo.Title;
            existingTodo.Priority = updatedTodo.Priority;
            existingTodo.CreatedItem = updatedTodo.CreatedItem;
            existingTodo.Deadline = updatedTodo.Deadline; 
            existingTodo.IsDone = updatedTodo.IsDone;

            if (!wasDone && updatedTodo.IsDone)
            {
                ShiftPrioritiesAfterCompletion(removedPriority);
            }

            return Ok(existingTodo);
        }

        private void ShiftPrioritiesAfterCompletion(int completedTaskPriority)
        {
            var tasksToShift = _todos
                .Where(t => !t.IsDone && t.Priority > completedTaskPriority)
                .ToList();

            foreach (var task in tasksToShift)
            {
                task.Priority = task.Priority - 1;
            }
        }
    }
}