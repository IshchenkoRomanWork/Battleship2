using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Data
{
    public class BattleShipContext : DbContext
    {
        public DbSet<GameDetails> GameDetails { get; set; }
        public DbSet<GameShot> GameShots { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ShipInformation> ShipInformations { get; set; }
        public DbSet<ShipLocation> ShipLocations { get; set; }
        public DbSet<StatisticsItem> Statistics  { get; set; }
        public BattleShipContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GameDetails>().HasKey(gd => gd.Id);
            builder.Entity<GameDetails>().HasMany(gd => gd.PlayerMaps);
            builder.Entity<GameDetails>().HasMany(gd => gd.ShotList);
            builder.Entity<GameDetails>().HasMany(gd => gd.Players);

            builder.Entity<Map>().HasKey(m => m.Id);
            builder.Entity<Map>().HasMany(m => m.ShipInformationList);

            builder.Entity<Player>().HasKey(p => p.Id);
            builder.Entity<Player>().Ignore(p => p.CurrentMap);

            builder.Entity<ShipInformation>().HasKey(si => si.Id);
            builder.Entity<ShipInformation>().HasOne(si => si.Ship);
            builder.Entity<ShipInformation>().HasOne(si => si.Location);
        }
    }
}
