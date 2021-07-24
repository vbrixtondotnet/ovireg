using Microsoft.EntityFrameworkCore;
using OnlineSabong.VirtualGuide.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSabong.VirtualGuide
{
    public class SabongDBContext : DbContext
    {
        public SabongDBContext(DbContextOptions<SabongDBContext> options)
              : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<FightResult> FightResults { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }
        public virtual DbSet<Rooster> Roosters { get; set; }
        public virtual DbSet<UserRoosters> UserRoosters { get; set; }
    }
}
