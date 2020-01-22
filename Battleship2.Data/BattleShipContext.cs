using Battleship2.Core.DataModels;
using Battleship2.Core.Enums;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<Coords> Coords { get; set; }
        public DbSet<Coords> LogInfos { get; set; }
        public DbSet<GameDetailsPlayer> GameDetailsPlayers { get; set; }
        public BattleShipContext(DbContextOptions<BattleShipContext> options)
        : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        { 
            builder.Entity<Ship>().HasKey(s => s.Id);
            builder.Entity<Ship>().Property(s => s.DeckStates)
                                  .HasConversion(dsList => dsList.Select(ds => new DeckStateWrapper(ds)),
                                                 dswList => dswList.Select(dsw => dsw.InnerDeckState).ToList());
            builder.Entity<Ship>().Ignore(s => s.DeckStates);

            builder.Entity<GameShot>().HasKey(gs => gs.Id);
            builder.Entity<GameShot>().HasOne(gs => gs.TargetCoords);
            builder.Entity<GameShot>().HasOne(gs => gs.Shooter);

            builder.Entity<Coords>().HasKey(s => s.Id);

            builder.Entity<GameDetails>().HasKey(gd => gd.Id);
            builder.Entity<GameDetails>().HasMany(gd => gd.PlayerMaps).WithOne();
            builder.Entity<GameDetails>().HasMany(gd => gd.ShotList).WithOne();

            builder.Entity<GameDetailsPlayer>().HasKey(gdp => gdp.Id);
            builder.Entity<GameDetailsPlayer>()
                .HasOne(gdp => gdp.Player)
                .WithMany(p => p.PlayerRelationList)
                .HasForeignKey(gdp => gdp.PlayerId);

            builder.Entity<GameDetailsPlayer>()
                .HasOne(gdp => gdp.GameDetails)
                .WithMany(gd => gd.PlayerRelationList)
                .HasForeignKey(gdp => gdp.GameDetailsId);

            builder.Entity<Player>().HasKey(p => p.Id);
            builder.Entity<Player>().Ignore(p => p.CurrentMap);

            builder.Entity<Map>().HasKey(m => m.Id);
            builder.Entity<Map>().HasMany(m => m.ShipInformationList).WithOne();
            builder.Entity<Map>().HasMany(m => m.ShotCoords).WithOne();

            builder.Entity<ShipInformation>().HasKey(si => si.Id);
            builder.Entity<ShipInformation>().HasOne(si => si.Ship);
            builder.Entity<ShipInformation>().HasOne(si => si.Location);

            builder.Entity<ShipLocation>().HasKey(sl => sl.Id);
            builder.Entity<ShipLocation>().HasOne(sl => sl.Coords);

            builder.Entity<StatisticsItem>().HasKey(si => si.Id);

            builder.Entity<LogInfo>().HasKey(li => li.Id);

            base.OnModelCreating(builder);
        }
    }
}
