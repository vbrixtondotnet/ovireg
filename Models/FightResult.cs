using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.Models
{
    public class FightResult
    {
        public int Id { get; set; }
        public string RingSide { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }

        public decimal Points { get; set; }
    }
}
