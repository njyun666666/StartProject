using StartProject.Common;
using StartProject.DB;
using StartProject.Models.Todo;
using StartProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class TodoService : ITodoService
    {
        private ITestDB _testDB;
        private ICacheService _cache;
        private IMyService _myService;

        public TodoService(ITestDB testDB, ICacheService cache, IMyService myService)
        {
            _testDB = testDB;
            _cache = cache;
            _myService = myService;
        }

        public List<TodoTaskModel> GetTodoTask(string subject)
        {
            return _testDB.GetTodoTask(subject);
        }
        public TodoTaskModel GetTodoTaskById(int id)
        {
            return _testDB.GetTodoTaskById(id);
        }

        public int Add(TodoTaskAddModel model)
        {
            return _testDB.Add(model);
        }

        public int SubjectExists(TodoTaskAddModel model)
        {
            return _testDB.SubjectExists(model.subject);
        }

    }
}
