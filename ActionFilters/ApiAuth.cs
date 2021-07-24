using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlineSabong.VirtualGuide.ActionFilters
{
    public class ApiAuth : ActionFilterAttribute
    {

        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        public ApiAuth(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var environment = configuration["Environment"];
            if (environment != "dev")
            {
                byte[] userIdBytes = null;
                if (context.HttpContext.Session.TryGetValue("userid", out userIdBytes))
                {
                    int userId = BitConverter.ToInt32(userIdBytes, 0);
                    userService.SetUserId(userId);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.HttpContext.Session.Set("userid", BitConverter.GetBytes(1));
                userService.SetUserId(1);
            }

            base.OnActionExecuting(context);
        }
    }
}
