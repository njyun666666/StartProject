using StartProject.Models.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services.IServices
{
    public interface ITodoService
    {
        public List<TodoTaskModel> GetTodoTask(string subject);
        public TodoTaskModel GetTodoTaskById(int id);
        public int Add(TodoTaskAddModel model);
        public int SubjectExists(TodoTaskAddModel model);
    }
}
