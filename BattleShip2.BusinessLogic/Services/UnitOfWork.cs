using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip2.BusinessLogic.Services
{
    public class UnitOfWork
    {
        private IRepository<Map> _maps;
        private IRepository<Player> _players;
        private IRepository<GameDetails> _gameDetails;
        private IRepository<StatisticsItem> _statistics;
        public UnitOfWork(IRepository<Map> maps,
            IRepository<Player> players,
            IRepository<GameDetails> gameDetails,
            IRepository<StatisticsItem> statistics)
        {
            _maps = maps;
            _players = players;
            _gameDetails = gameDetails;
            _statistics = statistics;
        }
        public ICollection<StatisticsItem> GetStatistics() { return default; }
        public ICollection<GameDetails> GetGameDetailsList() { return default; }
        public GameDetails GetGameDetails() { return default; }
        public void AddStatistics() { }
        public void AddGameDetails() { }
    }
}
