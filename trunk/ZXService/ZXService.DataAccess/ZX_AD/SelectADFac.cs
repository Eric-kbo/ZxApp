using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;

namespace ZXService.DataAccess.ZX_AD
{
    public class SelectADFac : ISelectionFactory<ZX_ADEntity>
    {
        public DbCommand ConstructSelectCommand(Database db, ZX_ADEntity idObject)
        {
            string sql = @" select  Top 3  ID,
                                           Position,  
                                           ImageFile,
                                           Http,
                                           AdName
              from  ZX_AD 
             where Enable=1
             and  BeginTime<@NowTimeInfo   and EndTime>@NowTimeInfo
             and Position=@Position
             Order by CreateTime";
            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入
            db.AddInParameter(command, "@NowTimeInfo", DbType.String, DateTime.Now.ToString());
            db.AddInParameter(command, "@Position", DbType.Int32, idObject.Position);
            return command;
        }
    }
}
