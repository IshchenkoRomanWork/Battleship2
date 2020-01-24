using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Battleship2.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private BattleShipContext _dBcontext { get; set; }
        DbSet<TEntity> _dbSet;

        public EFRepository(BattleShipContext dBcontext)
        {
            _dBcontext = dBcontext;
            _dbSet = _dBcontext.Set<TEntity>();

            foreach (var item in _dbSet.ToList())
            {
                LoadChilds(_dBcontext.Entry(item));
            }

        }
        public void Create(TEntity item)
        {
            //CheckExistingDisconnectedEntitiesBeforeAttach(item);
            _dbSet.Add(item);
            List<int> entryList = new List<int>();
            UpdateChilds(_dBcontext.Entry(item), entryList);
            _dBcontext.SaveChanges();
        }
        //public void CheckExistingDisconnectedEntitiesBeforeAttach(TEntity item)
        //{
        //    if (item is GameDetails)
        //    {
        //        var gameDetails = item as GameDetails;
        //        foreach (var player in gameDetails.Players.Select(pl => _dBcontext.Find<Player>(pl.Id)))
        //        {
        //            if (player != null)
        //            {
        //                _dBcontext.Remove(player);
        //            }
        //        }
        //    }
        //    _dBcontext.SaveChanges();
        //}
        private void LoadChilds(EntityEntry entityEntry)
        {
            foreach (var child in entityEntry.Navigations)
            {
                if (!child.IsLoaded)
                {
                    child.Load();
                    if (child.CurrentValue != null)
                    {
                        var childEntity = child.CurrentValue;
                        if (childEntity is IEnumerable)
                        {
                            foreach (var childEntElem in childEntity as IEnumerable)
                            {
                                LoadChilds(_dBcontext.Entry(childEntElem));
                            }
                        }
                        else
                        {
                            LoadChilds(_dBcontext.Entry(childEntity));
                        }
                    }
                }
            }
        }
        private void DeleteChilds(EntityEntry entityEntry)
        {
            foreach (var child in entityEntry.Navigations)
            {
                if (child.CurrentValue != null)
                {
                    var childEntity = child.CurrentValue;
                    if (childEntity is IEnumerable)
                    {
                        foreach (var childEntElem in childEntity as IEnumerable)
                        {
                            if(_dBcontext.Entry(childEntElem).State != EntityState.Deleted)
                                {
                                _dBcontext.Entry(childEntElem).State = EntityState.Deleted;
                                DeleteChilds(_dBcontext.Entry(childEntElem));
                            }
                        }
                    }
                    else
                    {
                        if (_dBcontext.Entry(childEntity).State != EntityState.Deleted)
                        {
                            _dBcontext.Entry(childEntity).State = EntityState.Deleted;
                            DeleteChilds(_dBcontext.Entry(childEntity));
                        }
                    }
                }
            }
        }
        private void UpdateChilds(EntityEntry entityEntry, List<int> entryList)
        {
            foreach (var child in entityEntry.Navigations)
            {
                if (child.CurrentValue != null)
                {
                    var childEntity = child.CurrentValue;
                    if (childEntity is IEnumerable)
                    {
                        foreach (var childEntElem in childEntity as IEnumerable)
                        {
                            var dbValues = _dBcontext.Entry(childEntElem).GetDatabaseValues();
                            if (dbValues != null)
                            {
                                _dBcontext.Entry(childEntElem).State = EntityState.Modified;
                            }
                            else
                            {
                                _dBcontext.Entry(childEntElem).State = EntityState.Added;
                            }
                            if (!entryList.Any(g => g == (childEntElem as Entity).Id))
                            {
                                entryList.Add((childEntElem as Entity).Id);
                                UpdateChilds(_dBcontext.Entry(childEntElem), entryList);
                            }
                        }
                    }
                    else
                    {
                        var dbValues = _dBcontext.Entry(childEntity).GetDatabaseValues();
                        if (dbValues != null)
                        {
                            _dBcontext.Entry(childEntity).State = EntityState.Modified;
                        }
                        else
                        {
                            _dBcontext.Entry(childEntity).State = EntityState.Added;
                        }
                        if (!entryList.Any(g => g == (childEntity as Entity).Id))
                        {
                            entryList.Add((childEntity as Entity).Id);
                            UpdateChilds(_dBcontext.Entry(childEntity), entryList);
                        }
                    }
                }
            }
        }

        public void Delete(int id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _dBcontext.SaveChanges();
        }

        public TEntity Get(int id)
        {
            var returneditem = _dbSet.FirstOrDefault(e => e.Id == id);
            return returneditem;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public void Update(TEntity item)
        {
            DeleteChilds(_dBcontext.Entry(_dBcontext.Find<TEntity>(item.Id)));
            _dBcontext.SaveChanges();
            _dbSet.Attach(item);
            List<int> entryList = new List<int>();
            UpdateChilds(_dBcontext.Entry(item), entryList);
            _dBcontext.SaveChanges();
        }
    }
}
