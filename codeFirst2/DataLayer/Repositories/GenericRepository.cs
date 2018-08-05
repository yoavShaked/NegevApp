using codeFirst2.DataLayer.Repositories.NegevEntitiesRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class , ITableEntity
    {
        private DbContext m_DBContext;

        public DbContext MyContext
        {
            get { return m_DBContext; }
            set { m_DBContext = value; }
        }

        public GenericRepository()
        {

        }

        public GenericRepository(DbContext i_dbContext)
        {
            m_DBContext = i_dbContext;
        }

        public void DeleteRow(int i_Key)
        {
            m_DBContext.Set<T>().Remove(m_DBContext.Set<T>().Find(i_Key));
        }

        public T GetRowByKey(int i_Key)
        {
            T entity = null;
            entity = m_DBContext.Set<T>().Find(i_Key);

            return entity;
        }

        public IEnumerable<T> GetNegevEntityCollection()
        {
            return m_DBContext.Set<T>().ToList();
        }

        public void AddRow(T i_Entity)
        {
            m_DBContext.Set<T>().Add(i_Entity);
        }

        public void Save()
        {
            m_DBContext.SaveChanges();
        }

        public void UpdateRow(T i_Entity)
        {
            m_DBContext.Entry(i_Entity).State = EntityState.Modified;
        }
    }
}