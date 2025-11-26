//namespace TodoApp.Models
//{
//    public class TodoItem
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }  
//        public bool IsDone { get; set; }
//    }
//}

namespace TodoApp.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        // اون علامت سوال جلوی string یعنی این فیلد می‌تونه خالی (Null) باشه
        // این کار ارور زرد رنگ رو از بین می‌بره
        public string? Title { get; set; }

        public bool IsDone { get; set; }
    }
}