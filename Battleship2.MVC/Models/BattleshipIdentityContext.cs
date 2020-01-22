using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Models
{
    public class BattleShipIdentityContext : DbContext
    {
        public DbSet<BattleshipIdentity> Users { get; set; }
        public BattleShipIdentityContext(DbContextOptions<BattleShipIdentityContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BattleshipIdentity>().HasKey(ent => ent.Id);
        }
    }
}
