using Battleship2.Core.DataModels;
using Battleship2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Battleship2.Core.Models
{
    public class GameDetails : Entity
    {
        [NotMapped]
        public List<Player> Players
        {
            get
            {
                return PlayerRelationList.Select(gdp => gdp.Player).ToList();
            }
            set
            {
                PlayerRelationList = value.Select(player => new GameDetailsPlayer()
                {
                    Player = player,
                    PlayerId = player.Id,
                    GameDetails = this,
                    GameDetailsId = Id
                }).ToList();
            }
        }
        public void AddPlayer(Player player)
        {
            PlayerRelationList.Add(new GameDetailsPlayer()
            {
                Player = player,
                PlayerId = player.Id,
                GameDetails = this,
                GameDetailsId = Id
            });
        }
        public void RemovePlayer(Guid id)
        {
            PlayerRelationList.RemoveAll(gdp => gdp.PlayerId == id);
        }
        public List<Map> PlayerMaps { get; set; }
        public List<GameShot> ShotList { get; set; }

        public List<GameDetailsPlayer> PlayerRelationList { get; set; } = new List<GameDetailsPlayer>();
    }
}
