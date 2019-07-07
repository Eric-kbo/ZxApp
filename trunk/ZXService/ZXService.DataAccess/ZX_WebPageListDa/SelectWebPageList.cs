using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataContracts.ZX_WebPageEntity;

namespace ZXService.DataAccess.ZX_WebPageListDa
{
    public class SelectWebPageList
    {
        public IList<ZX_WebPageInfoEntity> GetWebPageList(ZX_WebPageEntryParameterEntity Entry)
        {
            try
            {
                string sql = @"select * from (
                                SELECT ROW_NUMBER() OVER(Order by ID ) AS RowId ,*  FROM ZX_WebPage
                                    WHERE 1=1 AND ContentType=@ContentType
                                    )
                                as b
                                where RowId between (@PageSize * @PageIndex)- @PageSize+1 and @PageSize * @PageIndex  ORDER BY VisitCount ";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@ContentType",Entry.ContentType),
                    new SqlParameter("@PageSize",Entry.PageSize),
                    new SqlParameter("@PageIndex",Entry.PageIndex)
                };
                IList<ZX_WebPageInfoEntity> taList = new List<ZX_WebPageInfoEntity>();
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.LocalSqlServer, sql, paras);
                taList = IListDataSetHelper.DataSetToIList<ZX_WebPageInfoEntity>(ds, 0);
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
