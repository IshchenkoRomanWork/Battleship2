using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        private BattleShipContext _dBWithNoChangesContext;
        DbSet<TEntity> _dbSet;

        public EFRepository(BattleShipContext dBcontext)
        {
            _dBcontext = dBcontext;
            _dBWithNoChangesContext = dBcontext;
            _dbSet = _dBcontext.Set<TEntity>();

            //using (_dBcontext)
            //{
            //foreach (var item in _dBcontext.GameDetails)
            //{
            //    _dBcontext.Entry(item).Collection(item => item.PlayerMaps).Load();
            //    _dBcontext.Entry(item).Collection(item => item.PlayerRelationList).Load();
            //    _dBcontext.Entry(item).Collection(item => item.ShotList).Load();
            //}
            //foreach (var item in _dBcontext.GameShots)
            //{
            //    _dBcontext.Entry(item).Reference(item => item.Shooter).Load();
            //    _dBcontext.Entry(item).Reference(item => item.TargetCoords).Load();
            //}
            //foreach (var item in _dBcontext.Maps)
            //{
            //    _dBcontext.Entry(item).Collection(item => item.ShipInformationList).Load();
            //    _dBcontext.Entry(item).Collection(item => item.ShotCoords).Load();
            //}
            //foreach (var item in _dBcontext.ShipInformations)
            //{
            //    _dBcontext.Entry(item).Reference(item => item.Ship).Load();
            //    _dBcontext.Entry(item).Reference(item => item.Location).Load();
            //}
            //foreach (var item in _dBcontext.ShipLocations)
            //{
            //    _dBcontext.Entry(item).Reference(item => item.Coords).Load();
            //}
            //foreach (var item in _dBcontext.Statistics)
            //{
            //    _dBcontext.Entry(item).Collection(item => item.RemainingShips).Load();
            //}
            //}
            foreach (var item in _dbSet.ToList())
            {
                LoadChilds(_dBcontext.Entry(item));
            }

        }
        public void Create(TEntity item)
        {
            _dbSet.Attach(item);
            List<Guid> entryList = new List<Guid>();
            UpdateChilds(_dBcontext.Entry(item), entryList);
            _dBcontext.SaveChanges();
        }
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
        private void UpdateChilds(EntityEntry entityEntry, List<Guid> entryList)
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

        public void Delete(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _dBcontext.SaveChanges();
        }

        public TEntity Get(Guid id)
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
            _dbSet.Attach(item);
            List<Guid> entryList = new List<Guid>();
            UpdateChilds(_dBcontext.Entry(item), entryList);
            _dBcontext.SaveChanges();
        }
    }
}
