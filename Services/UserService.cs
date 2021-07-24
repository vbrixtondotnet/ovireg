using OnlineSabong.VirtualGuide.DBModels;
using OnlineSabong.VirtualGuide.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Services
{
    public class UserService : IUserService
    {
        SabongDBContext db;

        private int userId;
        public UserService(SabongDBContext db)
        {
            this.db = db;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            return db.Users.Where(u => u.UserName == username && u.Password == password).FirstOrDefault();
        }

        public User GetUser()
        {
            return db.Users.FirstOrDefault(u => u.Id == this.userId);
        }

        public void SetUserId(int userId)
        {
            this.userId = userId;
        }

        public int GetUserId()
        {
            return this.userId;
        }

        public bool LogUser(int userId, string ipAddress, string device, string country, string city, string region, string latlog, string timezone)
        {

            DateTime timeUtc = DateTime.UtcNow; 
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
           

            var userLog = new UserLog
            {
                UserId = userId,
                Device = device,
                IpAddress = ipAddress,
                LogInDate = cstTime,
                Country = country,
                City = city,
                Region = region,
                Latlong = latlog,
                Timezone = timezone
            };
            db.UserLogs.Add(userLog);
            db.SaveChanges();
            return true;
        }

    }
}
