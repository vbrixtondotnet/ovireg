using Microsoft.EntityFrameworkCore;
using OnlineSabong.VirtualGuide.DBModels;
using OnlineSabong.VirtualGuide.Models;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Services
{
    public class FightResultService : IFightResultService
    {
        SabongDBContext db;
        public FightResultService(SabongDBContext db) {
            this.db = db;
        }
        public List<DBModels.FightResult> GetFightResults(int userId = 0)
        {
            List<DBModels.FightResult> fightResults = new List<DBModels.FightResult>();
            if (userId != 0)
                //fightResults = db.FightResults.Where(f => f.UserId == userId).Where(b => b.FightDate <= DateTime.Now && b.FightDate > DateTime.Now.AddDays(-1)).ToList();
                fightResults = db.FightResults.Where(f => f.UserId == userId).ToList();
            else
                //fightResults = db.FightResults.Where(b => b.FightDate <= DateTime.Now && b.FightDate > DateTime.Now.AddDays(-1)).ToList();
                fightResults = db.FightResults.ToList();

            return fightResults;
        }

        public bool SaveFightResult(Models.FightResult fightResult)
        {
            var lastFightNo = db.FightResults.Count(f => f.UserId == fightResult.UserId);

            var newFightResult = new DBModels.FightResult();
            newFightResult.RingSide = fightResult.RingSide;
            newFightResult.Status = fightResult.Status;
            newFightResult.UserId = fightResult.UserId;
            newFightResult.FightNo = lastFightNo + 1;
            newFightResult.FightDate = DateTime.Now;
            db.FightResults.Add(newFightResult);
            db.SaveChanges();
            return true;
        }

        public bool SaveFightResult(Models.Rooster rooster)
        {

            /*var dbRooster = db.Roosters.Where(r => r.Id == rooster.Id).FirstOrDefault();
            if (rooster.DarkLegged) dbRooster.DarkLegged += 1;
            if (rooster.YellowLegged) dbRooster.YellowLegged += 1;
            if (rooster.WhiteLegged) dbRooster.WhiteLegged += 1;
            if (rooster.LightHackle) dbRooster.LightHackle += 1;
            if (rooster.DarkHackle) dbRooster.DarkHackle += 1;
            if (rooster.Bannered) dbRooster.Bannered += 1;
            if (rooster.NonBannered) dbRooster.NonBannered += 1;
           
            dbRooster.Wins += 1;
            dbRooster.NoOfFights += 1;

            db.Update(dbRooster);

            var dbLoosingRooser = db.Roosters.Where(r => r.Id == rooster.OpponentId).FirstOrDefault();

            if(rooster.OpponentId != rooster.Id)
            {
                dbLoosingRooser.NoOfFights += 1;
                db.Update(dbLoosingRooser);
            }

            db.SaveChanges();*/
            return true;
        }

        public bool ResetResult(int userId)
        {
            this.ExecuteQuery<int>($"DELETE FROM FIGHTRESULTS WHERE UserId = {userId}").FirstOrDefault();

            return true;
        }

        public List<DBModels.Rooster> GetRoosterStats(int meronRoosterId, int walaRoosterId)
        {
            List<DBModels.Rooster> roosterStats = new List<DBModels.Rooster>();
            var meronRooster = db.Roosters.FirstOrDefault(r => r.Id == meronRoosterId);
            var walaRooster = db.Roosters.FirstOrDefault(r => r.Id == walaRoosterId);

            roosterStats.Add(meronRooster);
            roosterStats.Add(walaRooster);
            return roosterStats;
        }

        protected List<T> ExecuteQuery<T>(string query, Dictionary<string, object> Params = null)
        {
            var currentType = typeof(T);
            var retval = new List<T>();

            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = query;
                if (cmd.Connection.State != ConnectionState.Open) { cmd.Connection.Open(); }

                if (Params != null)
                {
                    foreach (KeyValuePair<string, object> p in Params)
                    {
                        cmd.CommandText += $" {p.Value}, ";
                    }

                    cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.LastIndexOf(","));
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }

                        var props = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.GetSetMethod() != null);
                        var obj = Activator.CreateInstance(currentType);

                        foreach (var prop in props)
                        {
                            try { prop.SetValue(obj, row[prop.Name]); } catch (Exception e) { prop.SetValue(obj, null); }
                        }

                        retval.Add((T)Convert.ChangeType(obj, currentType));
                    }
                }
            }

            return (List<T>)Convert.ChangeType(retval, typeof(List<T>));
        }

        public bool SaveStats(ResetStatsModel resetStatsModel)
        {
            var rooster = db.Roosters.FirstOrDefault(r => r.Id == resetStatsModel.RoosterId);
            /*rooster.DarkLegged = resetStatsModel.DarkLegs;
            rooster.WhiteLegged = resetStatsModel.WhiteLegs;
            rooster.YellowLegged = resetStatsModel.YellowLegs;
            rooster.DarkHackle = resetStatsModel.DarkHackle;
            rooster.LightHackle = resetStatsModel.LightHackle;
            rooster.Bannered = resetStatsModel.Bannered;
            rooster.NonBannered = resetStatsModel.NonBannered;
            rooster.Wins = resetStatsModel.Wins;
            rooster.NoOfFights = resetStatsModel.Fights;*/

            db.Update(rooster);
            db.SaveChanges();
            return true;
        }

        public List<DBModels.Rooster> GetRoosters()
        {
            var roosters = db.Roosters.ToList();
            return roosters;
        }
    }
}
