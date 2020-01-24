using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship2.Data.Repositories
{
    //class GameDetailsRepository : IRepository<GameDetails>
    //{
    //    private DbContext _dBcontext { get; set; }
    //    private DbSet<GameDetails> _dBSet;

    //    public GameDetailsRepository(BattleShipContext dBcontext)
    //    {
    //        _dBcontext = dBcontext;
    //        _dBSet = _dBcontext.Set<GameDetails>();
    //    }

    //    public void Create(GameDetails item)
    //    {
    //        foreach (var player in item.Players)
    //        {
    //            var originalPlayer = _dBcontext.Set<Player>().Where(pl => pl.Id == player.Id).SingleOrDefault();

    //            if(originalPlayer !=  null)
    //            {
    //                var originalMap = _dBcontext.Set<Map>().Where(m => m.Id == originalPlayer.CurrentMap.Id);

    //                if (originalMap != null)
    //                {
    //                    var mapEntry = _dBcontext.Entry(originalMap);
    //                    mapEntry.CurrentValues.SetValues(player.CurrentMap);
    //                }

    //            }
    //        }
    //    }

    //    public void Delete(Guid id)
    //    {
    //        _dBSet.Remove(_dBSet.Find(id));
    //        _dBcontext.SaveChanges();
    //    }

    //    public GameDetails Get(Guid id)
    //    {
    //        return _dBSet.FirstOrDefaultAsync(e => e.Id == id).Result;
    //    }

    //    public IQueryable<GameDetails> GetAll()
    //    {
    //        return _dBSet.AsQueryable();
    //    }

    //    public void Update(GameDetails item)
    //    {
    //        _dBSet.Update(item);
    //    }
    //}
}
