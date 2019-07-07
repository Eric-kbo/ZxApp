using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_DeCaseImage
{
    public class UpdateImgFIile : SqlHelper
    {
        public static int UpImgFile(string path, string DeId)
        {
            int i = ExecuteSql(LocalSqlServer, string.Format(@" UPDATE   ZX_DeCase  SET  ImageFile={0}   Where DesignerID='{1}'", path, DeId));
            return i;
        }

        public static string SelDesigerId(string guid)
        {
            var DesigerId = ExecuteDataSet(LocalSqlServer, string.Format("Select  ID   From  ZX_Designer  Where ExistId='{0}'", guid));

            return DesigerId.Tables[0].Rows[0]["ID"].ToString();
        }

        public static int AddDeCase(ZX_DesignersEntity model)
        {
            int imgCount = model.ImageFileList.Count;
            StringBuilder sql = new StringBuilder();
            string sqlStr = string.Empty;

            sql.Append(@" Insert into ZX_DeCase(DesignerID,CaseTitle,ImageFile) values ");

            for (int i = 0; i < imgCount; i++)
            {
                sql.Append(string.Format("(@DesignerID{0},@CaseTitle{0},@ImageFile{0}),", i));
            }

            List<SqlParameter> ilistStr = new List<SqlParameter>();

            for (int i = 0; i < imgCount; i++)
            {
                ilistStr.Add(new SqlParameter("@DesignerID" + i, model.ID));
                ilistStr.Add(new SqlParameter("@CaseTitle" + i, model.ImageFileList[i].ImageTitle));
               // ilistStr.Add(new SqlParameter("@CaseDesc" + i, model.CaseDesc));
                ilistStr.Add(new SqlParameter("@ImageFile" + i, model.ImageFileList[i].ImageFile));
            }

            SqlParameter[] paras = ilistStr.ToArray();

            sqlStr = sql.ToString().Trim(',');

            int r = ExecuteSql(LocalSqlServer, sqlStr, paras);
            return r;
        }

        public static int AddDeCaseImg(ZX_DesignersEntity model)
        {
            int imgCount = model.ImageFileList.Count;
            StringBuilder sql = new StringBuilder();
            string sqlStr = string.Empty;
            sql.Append(@" Insert into ZX_DeCaseImage(CaseID,ImageTitle,ImageFile) values ");

            for (int i = 0; i < imgCount; i++)
            {
                sql.Append(string.Format("(@CaseID{0},@ImageTitle{0},@ImageFile{0}),", i));
            }

            List<SqlParameter> ilistStr = new List<SqlParameter>();

            for (int i = 0; i < imgCount; i++)
            {
                ilistStr.Add(new SqlParameter("@CaseID" + i, model.ID));
                ilistStr.Add(new SqlParameter("@ImageTitle" + i, model.ImageFileList[i].ImageTitle));
                ilistStr.Add(new SqlParameter("@ImageFile" + i, model.ImageFileList[i].ImageFile));
            }

            SqlParameter[] paras = ilistStr.ToArray();

            sqlStr = sql.ToString().Trim(',');

            int r = ExecuteSql(LocalSqlServer, sqlStr, paras);
            return r;
        }

    }
}
