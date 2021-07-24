using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.DBModels
{
    public class Rooster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }

        public int NoOfFights { get; set; }
    }
}
