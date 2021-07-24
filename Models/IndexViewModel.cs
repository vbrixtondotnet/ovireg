using OnlineSabong.VirtualGuide.DBModels;
using System.Collections.Generic;

namespace OnlineSabong.VirtualGuide.Models
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public List<DBModels.Rooster> Roosters { get; set; }
    }
}
