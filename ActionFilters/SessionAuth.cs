using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.ActionFilters
{

    public class SessionAuth : ActionFilterAttribute
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        public SessionAuth(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var environment = configuration["Environment"];
            if (environment != "dev")
            {
                byte[] userid = null;
                if (!context.HttpContext.Session.TryGetValue("userid", out userid))
                {
                    if (userid == null)
                    {
                        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                        redirectTargetDictionary.Add("action", "index");
                        redirectTargetDictionary.Add("controller", "Login");
                        context.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    }
                }
                else
                {
                    int userId = BitConverter.ToInt32(userid, 0);
                    userService.SetUserId(userId);
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
