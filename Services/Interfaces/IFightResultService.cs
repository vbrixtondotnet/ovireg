using OnlineSabong.VirtualGuide.DBModels;
using OnlineSabong.VirtualGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Services.Interfaces
{
    public interface IFightResultService
    {
        List<DBModels.FightResult> GetFightResults(int userId = 0);
        bool SaveFightResult(Models.FightResult fightResult);
        bool SaveFightResult(Models.Rooster rooster);
        bool SaveStats(Models.ResetStatsModel resetStatsModel);
        bool ResetResult(int userId);
        List<DBModels.Rooster> GetRoosterStats(int meronRoosterId, int walaRoosterId);
        List<DBModels.Rooster> GetRoosters();
    }
}
