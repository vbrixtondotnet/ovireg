using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSabong.VirtualGuide.Services.Interfaces;

namespace OnlineSabong.VirtualGuide.Controllers
{
    [AllowAnonymous]
    [Route("api/harvest")]
    [ApiController]
    public class HarvestApiController : ControllerBase
    {
        private readonly IFightResultService fightResultService;
        public HarvestApiController(IFightResultService fightResultService)
        {
            this.fightResultService = fightResultService;
        }
        [Route("fightresults")]
        [HttpPost]
        public IActionResult SaveFightResults([FromBody] List<Models.FightResult> fightResults)
        {
            var result = fightResultService.ResetResult(fightResults.FirstOrDefault().UserId);
            foreach (var fightResult in fightResults)
            {
                fightResultService.SaveFightResult(fightResult);
            }

            return Ok(true);
        }
    }
}
