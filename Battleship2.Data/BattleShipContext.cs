using Battleship2.Core.Enums;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        public DbSet<LogInfo> LogInfos { get; set; }
        public DbSet<PlayersData> PlayersDatas { get; set; }
        public BattleShipContext(DbContextOptions<BattleShipContext> options)
        : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Ship>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<GameShot>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<GameDetails>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<Player>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<Map>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<ShipInformation>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<ShipLocation>().Property(e => e.Id).HasDefaultValueSql("newid()");
            //builder.Entity<PlayersData>().Property(e => e.Id).HasDefaultValueSql("newid()");

            builder.Entity<Ship>().HasKey(e => e.Id);
            builder.Entity<GameShot>().HasKey(e => e.Id);
            builder.Entity<GameDetails>().HasKey(e => e.Id);
            builder.Entity<Player>().HasKey(e => e.Id);
            builder.Entity<Map>().HasKey(e => e.Id);
            builder.Entity<ShipInformation>().HasKey(e => e.Id);
            builder.Entity<ShipLocation>().HasKey(e => e.Id);
            builder.Entity<StatisticsItem>().HasKey(e => e.Id);
            builder.Entity<Coords>().HasKey(e => e.Id);
            builder.Entity<LogInfo>().HasKey(e => e.Id);
            builder.Entity<PlayersData>().HasKey(e => e.Id);


            builder.Entity<Ship>().Ignore(s => s.DeckStates);

            builder.Entity<GameShot>().HasOne(gs => gs.TargetCoords);

            builder.Entity<GameDetails>().HasMany(gd => gd.PlayerMaps).WithOne();
            builder.Entity<GameDetails>().HasMany(gd => gd.ShotList).WithOne();
            builder.Entity<GameDetails>().HasOne(gd => gd.PlayersData);
            builder.Entity<GameDetails>().Ignore(gd => gd.Players);

            builder.Entity<Player>().Ignore(p => p.CurrentMap);

            builder.Entity<Map>().HasMany(m => m.ShipInformationList).WithOne();
            builder.Entity<Map>().HasMany(m => m.ShotCoords).WithOne();

            builder.Entity<ShipInformation>().HasOne(si => si.Ship);
            builder.Entity<ShipInformation>().HasOne(si => si.Location);

            builder.Entity<ShipLocation>().HasOne(sl => sl.Coords);


            base.OnModelCreating(builder);
        }

        //public override EntityEntry Attach(object entity)
        //{
        //    var originEntity = Find(entity.GetType(), (entity as Entity).Id);
        //    if (originEntity != null)
        //    {
        //        Remove(originEntity);
        //        SaveChanges();
        //    }
        //    return base.Attach(entity);
        //}
    }
}
