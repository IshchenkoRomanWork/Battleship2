using Battleship2.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using Battleship2.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Battleship2.Tests
{
    public class Tests
    {
        private BattleShipContext _dbContext;
        [SetUp]
        public void Setup()
        {
            _dbContext = GetDatabaseContext();
        }

        private BattleShipContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<BattleShipContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new BattleShipContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Test]
        public void Test_DataContext_Are_Created()
        {
            var context = GetDatabaseContext();
            Assert.Pass();
        }

        [Test]
        public void Test_That_Maps_Are_Correctly_Added_And_Extracted()
        {
            MapSeeding();
            var map = _dbContext.Set<Map>().AsNoTracking().ToListAsync().Result[0];
            Assert.IsTrue(_dbContext.Set<Map>().CountAsync().Result == 0
                && map.ShipInformationList[0].Location.Coords.CoordX == 4
                && map.ShipInformationList[1].Ship.Type == ShipType.ThreeDeck);
        }
        [Test]
        public void Test_That_GameDetails_Are_Correctly_Saved()
        {
            GameDetailsSeeding();
            var details = _dbContext.Set<GameDetails>().AsNoTracking().ToListAsync().Result[0];
            Assert.IsTrue(details.PlayerMaps.First().ShipInformationList[0].Location.Coords.CoordX == 4 && details.Players.First().Name == "TestName");
        }
        private void MapSeeding()
        {
            if (_dbContext.Set<Map>().CountAsync().Result == 0)
            {
                var Map1 = new Map();
                Map1.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Down,
                        Coords = new Coords(1, 1)
                    },
                    Ship = new Ship(4)
                });
                Map1.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Up,
                        Coords = new Coords(2, 2)
                    },
                    Ship = new Ship(3)
                });
                var Map2 = new Map();
                Map2.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Down,
                        Coords = new Coords(3, 3)
                    },
                    Ship = new Ship(2)
                });
                Map2.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Up,
                        Coords = new Coords(4, 4)
                    },
                    Ship = new Ship(1)
                });
                _dbContext.Add(Map1);
                _dbContext.Add(Map2);
                _dbContext.SaveChanges();
            }
        }
        private void GameDetailsSeeding()
        {
            MapSeeding();
            var maps = _dbContext.Set<Map>().AsNoTracking().ToListAsync().Result;
            GameDetails details = new GameDetails()
            {
                PlayerMaps = maps,
                Players = new List<Player>()
                {
                    new Player()
                    {
                        CurrentMap = maps[0],
                        Name = "TestName"
                    },
                    new Player()
                    {
                        CurrentMap = maps[1],
                        Name = "SecondTestName"
                    }
                },
                ShotList = new List<GameShot>()
            };
            _dbContext.Add(details);
        }

    }
}