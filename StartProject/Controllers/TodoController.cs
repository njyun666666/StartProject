using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartProject.Enums;
using StartProject.Models.Todo;
using StartProject.Services.IServices;
using StartProject.ViewModels;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Get(string subject)
        {
            return Ok(new OKResponse() { Data=_todoService.GetTodoTask(subject) });
        }

        [HttpPost]
        public IActionResult Add(TodoTaskAddModel model)
        {
            int result=_todoService.Add(model);
            ResponseViewModel response ;

            if (result==1)
            {
                response = new ResponseViewModel(ResponseCodeEnum.success);
            }
            else
            {
                response = new ResponseViewModel(ResponseCodeEnum.todolist_add_task_error);
            }


            return Ok(response);
        }

    }

}
