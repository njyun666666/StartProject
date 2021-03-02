using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StartProject.Enums;
using StartProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Filters
{
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public ExceptionAttribute()
        {

        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);


            //如果異常沒有處理
            if (!context.ExceptionHandled)
            {
                Console.WriteLine(context.Exception.ToString());
                var result = new ResponseViewModel
                {
                    Code = (int)ResponseCodeEnum.exception,
                    Message = context.Exception.ToString(),
                    Data = null
                };
                context.Result = new JsonResult(result);

                //LogError(context);


            }
            context.ExceptionHandled = true;

            //var _lineNotifyService = (LineNotifyService)context.HttpContext.RequestServices.GetService(typeof(LineNotifyService));
            //await _lineNotifyService.LineNotify_Send(context.HttpContext., null, null, "", "Test");
        }


    }
}
