using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataAccess.ZX_Designer;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class DeCaseInfoRepository : Repository<ZX_DecaseEntity>
    {
        public List<ZX_DecaseEntity> GetDecaseInfo(ZX_DecaseEntity model)
        {
            var sql = new SelectDeCaseImgFac();
            return base.Find(sql, new DataDeCaseFactory(), model);
        }

        public ZX_DecaseEntity GetDecaseInfoOne(ZX_DecaseEntity model)
        {
            var sql = new SelectDeCaseFacOne();
            return FindOne(sql, new DataDeCaseFactory(), model);
        }

        public ZX_DecaseEntity GetDacsInfoDao(ZX_DecaseEntity model)
        {
            //参数形式传入查询条件，可以放sql注入 
            try
            {
                string sql = @" select * FROM ZX_DeCase where ImageFile=@ImageFile";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@ImageFile",model.ImageFile)
                };
                IList<ZX_DecaseEntity> taList = new List<ZX_DecaseEntity>();
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.LocalSqlServer, sql, paras);
                taList = IListDataSetHelper.DataSetToIList<ZX_DecaseEntity>(ds, 0);
                return taList.Any() ? taList[0] : new ZX_DecaseEntity();
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
                throw;
            }

        }
    }
}
