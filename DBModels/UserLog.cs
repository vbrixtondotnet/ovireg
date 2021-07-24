using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.DBModels
{
    public class UserLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Device { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Latlong { get; set; }
        public string Timezone { get; set; }
        public DateTime LogInDate { get; set; }
    }
}
