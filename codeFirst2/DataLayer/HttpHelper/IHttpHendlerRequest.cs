using codeFirst2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codeFirst2.DataLayer.HttpHelper
{
    public interface IHttpHendlerRequest
    {
        IEnumerable<ITableEntity> GetAllTable(DbContext i_Context);
        ITableEntity GetRowByID(int i_ID, DbContext i_Context);
        void DeleteRow(int i_ID, DbContext i_Context);
        void UpdateRow(ITableEntity i_RowToUpdate, DbContext i_Context);
        void PostNewRow(ITableEntity i_NewRow, DbContext i_Context);
    }
}
