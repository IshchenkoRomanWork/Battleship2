using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models
{
    public class BattleshipIdentityContext : DbContext
    {
        public DbSet<BattleshipIdentity> Users { get; set; }
        public BattleshipIdentityContext(DbContextOptions<BattleshipIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
