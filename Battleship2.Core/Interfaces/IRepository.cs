using Battleship2.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship2.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public void Create(TEntity item);
        public TEntity Get(Guid id);
        public void Update(TEntity item);
        public void Delete(Guid id);
        public ICollection<TEntity> GetAll();
    }
}
