using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineSabong.VirtualGuide.ActionFilters;
using OnlineSabong.VirtualGuide.Models;
using OnlineSabong.VirtualGuide.Services.Interfaces;

namespace OnlineSabong.VirtualGuide.Controllers
{

    [ServiceFilter(typeof(SessionAuth))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IFightResultService fightResultService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IFightResultService fightResultService)
        {
            _logger = logger;
            this.userService = userService;
            this.fightResultService = fightResultService;
        }

        public IActionResult Index()
        {
            var user = userService.GetUser();
            var roosters = fightResultService.GetRoosters();
            return View(new IndexViewModel { Roosters = roosters, User = user });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
