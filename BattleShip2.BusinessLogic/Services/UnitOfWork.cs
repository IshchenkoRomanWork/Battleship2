using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using BattleShip2.BusinessLogic.Intefaces;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

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
        public List<StatisticsItem> GetStatistics(ICollection<IFilter> filters, SortingItem sorting)
        {
            var allStatistics = _statistics.GetAll();
            foreach(var filter in filters)
            {
                filter.ApplyFilter(ref allStatistics);
            }
            ApplySorting(ref allStatistics, sorting);
            return allStatistics.ToList();
        }
        private void ApplySorting(ref IQueryable<StatisticsItem> statistics, SortingItem sorting)
        {
            bool directionAscending = sorting.SortingDirection == SortingDirection.Ascending;
            statistics = sorting.SortingType switch
            {
                SortingType.DateSorting => directionAscending 
                ? statistics.OrderBy(si => si.GameDate)
                : statistics.OrderByDescending(si => si.GameDate),
                SortingType.RemainingShipsSorting => directionAscending 
                ? statistics.OrderBy(si => si.RemainingShips.Count) 
                : statistics.OrderByDescending(si => si.GameDate),
            };
        }
        public List<GameDetails> GetGameDetailsList(Player player)
        {
            return _gameDetails.GetAll().
                Where(gd => gd.Players.
                Any(pl => pl.Id == player.Id)).
                ToList();
        }
        public GameDetails GetGameDetails(Guid gameDetailsId)
        {
            return _gameDetails.Get(gameDetailsId);
        }
        public void AddStatistics(StatisticsItem item)
        {
            _statistics.Create(item);
        }
        public void AddGameDetails(GameDetails item)
        {
            _gameDetails.Create(item);
        }
        public void CreatePlayer(Player player)
        {
            _players.Create(player);
        }
        public Player GetPlayer(Guid Id)
        {
            return _players.Get(Id);
        }
    }
}
