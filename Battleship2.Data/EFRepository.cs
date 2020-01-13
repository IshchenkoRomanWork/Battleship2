using Battleship2.Core.Interfaces;
using Battleship2.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship2.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private DbContext _dBcontext;
        DbSet<TEntity> _dbSet;
        public EFRepository(DbContext dBcontext)
        {
            _dBcontext = dBcontext;
            _dbSet = _dBcontext.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _dBcontext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
            _dBcontext.SaveChanges();
        }

        public TEntity Get(Guid id)
        {
            return _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id).Result;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public void Update(TEntity item)
        {
            _dbSet.Update(item);
        }
    }
}
