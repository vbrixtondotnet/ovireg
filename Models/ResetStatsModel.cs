using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Models
{
    public class ResetStatsModel
    {
        public int RoosterId { get; set; }
        public int DarkLegs { get; set; }
        public int YellowLegs { get; set; }
        public int WhiteLegs { get; set; }
        public int DarkHackle { get; set; }
        public int LightHackle { get; set; }
        public int Bannered { get; set; }
        public int NonBannered { get; set; }
        public int Wins { get; set; }
        public int Fights { get; set; }
    }
}
