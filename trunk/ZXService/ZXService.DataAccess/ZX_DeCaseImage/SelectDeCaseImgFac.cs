using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCaseImage;

namespace ZXService.DataAccess.ZX_DeCaseImage
{
    public class SelectDeCaseImgFac : ISelectionFactory<ZX_DeCaseImageEntity>
    {

        public DbCommand ConstructSelectCommand(Database db, ZX_DeCaseImageEntity idObject)
        {
            string sql = @" select 
                             *   from
                             ZX_DeCaseImage";

            if (null != idObject && !string.IsNullOrEmpty(idObject.ID))
            {
                sql += " where CaseID=@ID ";
            }

            DbCommand command = db.GetSqlStringCommand(sql);

            if (null != idObject && !string.IsNullOrEmpty(idObject.ID))
            {
                db.AddInParameter(command, "@ID", DbType.Guid, idObject.CaseID);
            } 

            return command;
        }
    }
}
