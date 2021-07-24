using OnlineSabong.VirtualGuide.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        User GetUser();
        int GetUserId();
        void SetUserId(int userId);
        bool LogUser(int userId, string ipAddress, string device
            , string country, string city, string region, string latlog, string timezone);
    }
}
