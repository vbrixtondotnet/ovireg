using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.DBModels
{
    public class UserRoosters
    {
        public int Id { get; set; }
        public int RoosterId { get; set; }
        public int DarkLegged { get; set; }
        public int YellowLegged { get; set; }
        public int WhiteLegged { get; set; }
        public int LightHackle { get; set; }
        public int DarkHackle { get; set; }
        public int Bannered { get; set; }
        public int NonBannered { get; set; }
    }
}
