using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSabong.VirtualGuide.ActionFilters;
using OnlineSabong.VirtualGuide.DBModels;
using OnlineSabong.VirtualGuide.Models;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using OnlineSabong.VirtualGuide.Utilities;
using FightResult = OnlineSabong.VirtualGuide.DBModels.FightResult;

namespace OnlineSabong.VirtualGuide.Controllers
{
    [Route("api")]
    [ServiceFilter(typeof(ApiAuth))]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IFightResultService fightResultService;
        private readonly IUserService userService;
        private List<string> pattern = new List<string>();
        [ThreadStatic]
        private static Random random;
        public APIController(IFightResultService fightResultService, IUserService userService)
        {
            this.fightResultService = fightResultService;
            this.userService = userService;

        }


        [Route("fightresults")]
        [HttpGet]
        public IActionResult GetFightResults(int id)
        {
            var userId = userService.GetUserId();
            //int userId = BitConverter.ToInt32(byteId, 0);
            List<FightResult> fightResults = fightResultService.GetFightResults(userId);

            return Ok(fightResults.Batch(5).ToList());

        }

        [Route("savefightresult")]
        [HttpPost]
        public IActionResult SaveFightResult([FromBody] Models.Rooster rooster)
        {
            var result = fightResultService.SaveFightResult(rooster);

            return Ok(result);
        }
        [Route("savefightresult2")]
        [HttpPost]
        public IActionResult SaveFightResult2([FromBody] Models.FightResult rooster)
        {
            var result = fightResultService.SaveFightResult(rooster);

            return Ok(result);
        }

        [Route("reset")]
        [HttpPost]
        public IActionResult ResetResults([FromBody] Models.ResetStatsModel resetStatsModel)
        {
            var result = fightResultService.SaveStats(resetStatsModel);
            return Ok(result);
        }

        [Route("nextresult")]
        [HttpPost]
        public IActionResult GetNextResult([FromBody] List<Models.Rooster> roosters)
        {
            Thread.Sleep(2000);
            int userId = userService.GetUserId();

            var newResult = GetNextFightResult(roosters);
            return Ok(newResult);

        }

        [Route("nextresult2")]
        [HttpGet]
        public IActionResult GetNextResult2()
        {
            Thread.Sleep(2000);
            int userId = userService.GetUserId();
            var results = fightResultService.GetFightResults(1);
            random = new Random();
            var result = results[random.Next(results.Count)];
            return Ok(result);

        }

        private Models.FightResult GetNextFightResult(List<Models.Rooster> roosters)
        {
            Models.FightResult retval = new Models.FightResult();
            var roosterStats = fightResultService.GetRoosterStats(roosters[0].Id, roosters[1].Id);

            var fightingMeronRooster = roosters[0];
            var fightingWalaRooster = roosters[1];

            var meronRoosterStat = roosterStats[0];
            var walaRoosterStat = roosterStats[1];

            return GetByAttributes(fightingMeronRooster, fightingWalaRooster, meronRoosterStat, walaRoosterStat);

        }

        private Models.FightResult GetByAttributes(Models.Rooster fightingMeronRooster, Models.Rooster fightingWalaRooster, DBModels.Rooster meronRoosterStat, DBModels.Rooster walaRoosterStat)
        {
            Models.FightResult retval = new Models.FightResult();
            Models.FightResult meronBet = new Models.FightResult();
            Models.FightResult walaBet = new Models.FightResult();

            meronBet.RingSide = "MERON";
            meronBet.Points = Convert.ToDecimal((float)meronRoosterStat.Wins / (float)meronRoosterStat.NoOfFights);

            walaBet.RingSide = "WALA";
            walaBet.Points = Convert.ToDecimal((float)walaRoosterStat.Wins / (float)walaRoosterStat.NoOfFights);

            /*if (fightingMeronRooster.DarkLegged)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.DarkLegged / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.YellowLegged)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.YellowLegged / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.WhiteLegged)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.WhiteLegged / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.LightHackle)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.LightHackle / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.DarkHackle)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.DarkHackle / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.Bannered)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.Bannered / (float)meronRoosterStat.NoOfFights);
            }
            if (fightingMeronRooster.NonBannered)
            {
                meronBet.Points += Convert.ToDecimal((float)meronRoosterStat.NonBannered / (float)meronRoosterStat.NoOfFights);
            }

            if (fightingWalaRooster.DarkLegged)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.DarkLegged / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.YellowLegged)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.YellowLegged / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.WhiteLegged)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.WhiteLegged / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.LightHackle)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.LightHackle / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.DarkHackle)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.DarkHackle / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.Bannered)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.Bannered / (float)walaRoosterStat.NoOfFights);
            }
            if (fightingWalaRooster.NonBannered)
            {
                walaBet.Points += Convert.ToDecimal((float)walaRoosterStat.NonBannered / (float)walaRoosterStat.NoOfFights);
            }*/


            if (meronBet.Points > walaBet.Points) 
            {
                return meronBet;
            }
            else if (walaBet.Points > meronBet.Points)
            {
                return walaBet;
            }
            else
            {
                return new Models.FightResult { RingSide = "DRAW" };
            } 
        }
    }

    public static class Util
    {
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items,
                                                      int maxItems)
        {
            return items.Select((item, inx) => new { item, inx })
                        .GroupBy(x => x.inx / maxItems)
                        .Select(g => g.Select(x => x.item));
        }
    }
}
