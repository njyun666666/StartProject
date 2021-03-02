using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartProject.Models.Form;
using StartProject.ViewModels;

namespace StartProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        public IActionResult SingleFile([FromForm] FormSingleFileModel model)
        {
            return Ok(new OKResponse() { Data = model.file.FileName });
        }

        public IActionResult MultipleFile([FromForm] FormMultipleFileModel model)
        {
            return Ok(new OKResponse() { });
        }

    }
}
