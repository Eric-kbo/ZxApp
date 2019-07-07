using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataContracts.ZX_DeImageEntity;

namespace ZXService.DataAccess.ZX_DeImageDa
{
    public class DeImageDao
    {
        public ImageDesigner GetDeDisenger(string ID)
        {
            try
            {
                string sql = @"SELECT ID as DeID,* FROM ZX_Designer where ID=@id";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@id",ID)
                };
                ImageDesigner taList = new ImageDesigner();
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.LocalSqlServer, sql, paras);
                taList = IListDataSetHelper.DataSetToIList<ImageDesigner>(ds, 0)[0];
                return taList;
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
                throw;
            }
        }

        public List<ZX_ImageList> GetDeCaseImageList(string ID)
        {
            try
            {
                string sql = @"SELECT ID as DecaseID, * FROM ZX_DeCase where DesignerID=@id";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@id",ID)
                };
                IList<ZX_ImageList> taList = new List<ZX_ImageList>();
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.LocalSqlServer, sql, paras);
                taList = IListDataSetHelper.DataSetToIList<ZX_ImageList>(ds, 0);
                return taList.ToList();
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
                throw;
            }
        }

    }
}
