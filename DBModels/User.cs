using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public DateTime? LasLoggedIn { get; set; }

        public bool Active { get; set; }
    }
}
