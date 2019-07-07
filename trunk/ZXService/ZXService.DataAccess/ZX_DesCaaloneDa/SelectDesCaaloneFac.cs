using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_DesCaaloneDa
{
    public class SelectDesCaaloneFac : ISelectionFactory<ZX_DeCaseInfoEntity>
    {
        public DbCommand ConstructSelectCommand(Database db, ZX_DeCaseInfoEntity idObject)
        {
            string sql = @"select * from ZX_DeCase ";

            if (!String.IsNullOrEmpty(idObject.DesignerID))
            {
                sql += " Where DesignerID=@DesignerID ";
            }

            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入  
            db.AddInParameter(command, "@DesignerID", DbType.String, idObject.DesignerID);
            return command;
        }
    }
}
