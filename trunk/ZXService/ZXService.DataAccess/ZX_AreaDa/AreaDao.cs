using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataContracts.ZX_AreaEntity;

namespace ZXService.DataAccess.ZX_AreaDa
{
    public class AreaDao
    {
        public IList<ZX_AreaInfoEntity> GetAreaList()
        {
            try
            {
                string sql = @"SELECT * FROM ZX_Area where 1=@id";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@id","1")
                };
                IList<ZX_AreaInfoEntity> taList = new List<ZX_AreaInfoEntity>();
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.LocalSqlServer, sql, paras);
                taList = IListDataSetHelper.DataSetToIList<ZX_AreaInfoEntity>(ds, 0);
                return taList;
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
                throw;
            }
        }
    }
}
