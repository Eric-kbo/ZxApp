using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class SelectDeCaseImgFac : ISelectionFactory<ZX_DecaseEntity>
    {

        public DbCommand ConstructSelectCommand(Database db, ZX_DecaseEntity idObject)
        {
            string sql = @" select   *  FROM    ZX_DeCase
                                where  DesignerID=@ID";
            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入
            db.AddInParameter(command, "@ID", DbType.Guid, idObject.DesignerID);
            return command;
        }
    }

    public class SelectDeCaseFacOne : ISelectionFactory<ZX_DecaseEntity>
    {
        public DbCommand ConstructSelectCommand(Database db, ZX_DecaseEntity idObject)
        {
            string sql = @" select * FROM ZX_DeCase where ImageFile=@ImageFile";
            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入
            db.AddInParameter(command, "@ImageFile", DbType.String, idObject.ImageFile);
            return command;
        }
    }
}
