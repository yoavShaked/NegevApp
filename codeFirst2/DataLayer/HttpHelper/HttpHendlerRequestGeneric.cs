using codeFirst2.DataLayer.LightWightesEntities;
using codeFirst2.DataLayer.Repositories;
using System.Collections.Generic;
using System.Data.Entity;

namespace codeFirst2.DataLayer.HttpHelper
{
    public abstract class HttpHendlerRequestGeneric<T> where T : class,ITableEntity
    {
        GenericRepository<T> m_Repository;

        public GenericRepository<T> Repository
        {
            get { return m_Repository; }
            set { m_Repository = value; }
        }

        public HttpHendlerRequestGeneric(GenericRepository<T> repository)
        {
            m_Repository = repository;
        }

        public void DeleteRow(int i_ID, DbContext i_Context)
        {
            m_Repository.MyContext = i_Context;
            m_Repository.DeleteRow(i_ID);
            m_Repository.Save();
        }

        protected abstract ILightWeight GetLight(T i_Table, DbContext i_Context);

        public IEnumerable<ILightWeight> GetAllTable(DbContext i_Context)
        {
            List<ILightWeight> to_return = new List<ILightWeight>();
            m_Repository.MyContext = i_Context;
            IEnumerable<T> table = m_Repository.GetNegevEntityCollection();

            foreach(T data in table)
            {
                to_return.Add(GetLight(data, i_Context));
            }

            return to_return;
        }

        public ILightWeight GetRowByID(int i_ID, DbContext i_DbContext)
        {
            m_Repository.MyContext = i_DbContext;
            T data = m_Repository.GetRowByKey(i_ID);

            return GetLight(data, i_DbContext);
        }

        public void PostNewRow(T i_NewRow, DbContext i_Context)
        {
            m_Repository.MyContext = i_Context;
            m_Repository.AddRow(i_NewRow);
            m_Repository.Save();
        }

        public void UpdateRow(T i_RowToUpdate, DbContext i_Context)
        {
            m_Repository.MyContext = i_Context;
            m_Repository.UpdateRow(i_RowToUpdate);
            m_Repository.Save();
        }
    }
}