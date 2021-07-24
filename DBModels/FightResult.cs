using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide.DBModels
{
    public class FightResult
    {
        public int Id { get; set; }
        public string RingSide { get; set; }
        public int Status { get; set; }

        public int FightNo { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime FightDate { get; set; }
    }
}
