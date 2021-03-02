using Microsoft.AspNetCore.Http;
using StartProject.Common;
using StartProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services
{
    public class LogService : ILogService
    {
        private IMyService _myService;
        private IHttpContextAccessor _accessor;
        private string controllerName;
        private string actionName;
        private string ip;

        public LogService(IMyService myService, IHttpContextAccessor accessor)
        {
            _myService = myService;
            _accessor = accessor;

            if (accessor != null)
            {
                controllerName = _accessor.HttpContext.Request.RouteValues["Controller"].ToString();
                actionName = _accessor.HttpContext.Request.RouteValues["Action"].ToString();
                ip = CommonTools.Userip_Get(_accessor.HttpContext);
            }
        }


        public void Add(int status, string message)
        {

        }


    }
}
