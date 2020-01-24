using Battleship2.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Battleship2.Core.Models;
using Battleship2.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace Battleship2.Tests
{
    public class DataTests
    {
        private BattleShipContext _dbContext;
        [SetUp]
        public void Setup()
        {

        }

        private BattleShipContext GetDatabaseContext()
        {
            string BattleShipConnectionString = "Server=ISHCHENKO;Database=BattleShip2;User Id=Roman;Password=123456";
            var options = new DbContextOptionsBuilder<BattleShipContext>()
                .UseSqlServer(BattleShipConnectionString)
                .Options;
            var databaseContext = new BattleShipContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Test]
        public void Test_That_Maps_Are_Correctly_Added_And_Extracted()
        {
            MapSeeding();
            var map = _dbContext.Set<Map>().AsQueryable().ToListAsync().Result[0];
            Assert.IsTrue(map.ShipInformationList[0].Location.Coords.CoordX == 1
                && map.ShipInformationList[1].Ship.Type == ShipType.TwoDeck);
        }
        [Test]
        public void Test_That_GameDetails_Are_Correctly_Saved()
        {
            GameDetailsSeeding();
            var set = _dbContext.Set<GameDetails>().AsQueryable();
            var list = set.ToListAsync().Result;
            var details = list[0];
            Assert.IsTrue(details.PlayerMaps.First().ShipInformationList[0].Location.Coords.CoordX == 1 && details.Players.First().Name == "TestName");
        }
        [Test]
        public void Test_Entity_Stat_Modified_On_External_Changes()
        {
            MapSeeding();
            var map = _dbContext.Set<Map>().FirstOrDefault();
            map.ShotCoords.Add(new Coords(1, 2));
            _dbContext.Update(map);
            Assert.AreEqual(EntityState.Modified, _dbContext.Entry(map).State);
        }
        [Test]
        public void Test_Referencial_Entities_Are_Saved()
        {
            MapSeeding();
            var player = new Player()
            {
                Name = "Ioann The Terrible",
                CurrentMap = _dbContext.Set<Map>().FirstOrDefault()
            };
            _dbContext.Add(player);
            _dbContext.SaveChanges();
            Assert.Pass();
        }
        [Test]
        public void Test_ConcurrentBag_Doesnt_break_tracking()
        {
            MapSeeding();
            ConcurrentBag<Map> maps = new ConcurrentBag<Map>();
            maps.Add(_dbContext.Set<Map>().FirstOrDefault());
            var player = new Player()
            {
                Name = "Ioann The Terrible",
                CurrentMap = maps.First()
            };
            _dbContext.Add(player);
            _dbContext.SaveChanges();
            Assert.Pass();
        }
        [Test]
        public void Test_Two_Gets_Make_Saving_Still_Possible()
        {
            MapSeeding();
            var map1 = _dbContext.Set<Map>().FirstOrDefault();
            var map2 = _dbContext.Set<Map>().FirstOrDefault(m => m.Id == map1.Id);
            var player = new Player()
            {
                Name = "Ioann The Terrible",
                CurrentMap = map2
            };
            _dbContext.Add(player);
            _dbContext.SaveChanges();
            Assert.AreEqual(map1, map2);
        }
        [Test]
        public void Test_GameDetails_Are_Saved_If_Given_Existing_Players()
        {
            var _firstdbContext = GetDatabaseContext();
            var efplrepo = new EFRepository<Player>(_firstdbContext);
            var player1 = new Player() { Name = "Ioann, the Terrible", CurrentMap = new Map() };
            efplrepo.Create(player1); //dwada
            var players = new List<Player>() { efplrepo.Get(player1.Id) };
            var _seconddbContext = GetDatabaseContext();
            var efgd2repo = new EFRepository<GameDetails>(_seconddbContext);
            GameDetails gameDetails = new GameDetails()
            {
                PlayersData = new PlayersData()
                {
                    FirstPlayerId=default,
                    SecondPlayerId=default,
                    SecondPlayerName="aaaa",
                    FirstPlayerName="bbb"
                },
                PlayerMaps = players.Select(p => p.CurrentMap).ToList(),
                ShotList = new List<GameShot>()
            };
            efgd2repo.Create(gameDetails); //dawdawd
            var _thirddbContext = GetDatabaseContext();
            var efgd3repo = new EFRepository<GameDetails>(_thirddbContext);
            efgd3repo.Update(gameDetails); //dawdawd
            var testplayer = new EFRepository<Player>(GetDatabaseContext()).Get(player1.Id);
            Assert.IsTrue(testplayer.Name == "Alexander the Great");
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
                    Ship = new Ship(1)
                });
                Map1.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Up,
                        Coords = new Coords(3, 3)
                    },
                    Ship = new Ship(2)
                });
                var Map2 = new Map();
                Map2.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Down,
                        Coords = new Coords(5, 5)
                    },
                    Ship = new Ship(3)
                });
                Map2.AddShip(new ShipInformation()
                {
                    Location = new ShipLocation()
                    {
                        Direction = Direction.Up,
                        Coords = new Coords(7, 7)
                    },
                    Ship = new Ship(4)
                });
                _dbContext.Add(Map1);
                _dbContext.Add(Map2);
                _dbContext.SaveChanges();
            }
        }
        private void GameDetailsSeeding()
        {
            MapSeeding();
            var maps = _dbContext.Set<Map>().AsQueryable().ToListAsync().Result;
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
            _dbContext.SaveChanges();
        }



    }
}