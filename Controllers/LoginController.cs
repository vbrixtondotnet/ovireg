using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineSabong.VirtualGuide.Models;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using UAParser;

namespace OnlineSabong.VirtualGuide.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService userService;
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index(AuthResultModelcs authResultModelcs = null)
        {

            return View(authResultModelcs);
        }

        public IActionResult UnderConstruction()
        {
            return View();
        }

        [Route("authenticate")]
        [HttpPost]
        public IActionResult LoginUser(string username, string password)
        {

            //Singapore Standard Time
            var user =  userService.Authenticate(username, password).Result;

            if (user == null)
            {
                return View("Index", new AuthResultModelcs { ErrorCode = 404 });
            }
            else
            {
                if (user.Active)
                {

                    var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                    //var ipAddress = "49.149.30.143";
                    var userAgent = HttpContext.Request.Headers["User-Agent"];
                    var uaParser = Parser.GetDefault();
                    ClientInfo c = uaParser.Parse(userAgent);

                    string info = new WebClient().DownloadString("http://ipinfo.io/" + ipAddress);
                    IPInfo ipinfo = JsonConvert.DeserializeObject<IPInfo>(info);

                    string deviceInfo = $"Device: {c.Device.Family}  {c.Device.Brand}  {c.Device.Model} , OS:  {c.OS.Family}, Browser: {c.UA.Family}";
                    userService.LogUser(user.Id, ipAddress, deviceInfo, ipinfo.country, ipinfo.city, ipinfo.region, ipinfo.loc, ipinfo.timezone);

                    userService.SetUserId(user.Id);
                    HttpContext.Session.Set("userid", BitConverter.GetBytes(user.Id));
                    return Redirect("/Home");
                }
                else
                {
                    return View("Index", new AuthResultModelcs { ErrorCode = 400 });
                }
            }
        }

        [Route("logout")]
        public IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/Login");
        }
    }
}
