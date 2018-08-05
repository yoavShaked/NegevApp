using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace codeFirst2.DataLayer.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetNegevEntityCollection();
        T GetRowByKey(int i_Key);
        void AddRow(T i_Entity);
        void DeleteRow(int i_Key);
        void UpdateRow(T i_Entity);
        void Save();
    }
}