using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Models
{
    public class Rooster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool DarkLegged { get; set; }
        public bool YellowLegged { get; set; }
        public bool WhiteLegged { get; set; }
        public bool LightHackle { get; set; }
        public bool DarkHackle { get; set; }
        public bool Bannered { get; set; }
        public bool NonBannered { get; set; }
        public bool Winner { get; set; }
        public int OpponentId { get; set; }
    }
}
