using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_Designer
{
    public class InsertDesignerFac : IInsertFactory<ZX_DesignersEntity>
    {
        public DbCommand ConstructInsertCommand(Database db, ZX_DesignersEntity domainObj)
        {
            string sql = @"insert  into  ZX_Designer 
                            (DeName,Sex,WorkYear,School,Experience,Idea,Price,Mobile,WxCode,ExistId,Eexpert,AreaID,Photo)
                    values(
                            @DeName,@Sex,@WorkYear,@School,@Experience,@Idea,@Price,@Mobile,@WxCode,@ExistId,@Eexpert,@AreaID,@Photo)";
            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入
            db.AddInParameter(command, "@DeName", DbType.String, domainObj.DeName);
            db.AddInParameter(command, "@Sex", DbType.Boolean, domainObj.Sex);
            db.AddInParameter(command, "@WorkYear", DbType.Int32, domainObj.WorkYear);
            db.AddInParameter(command, "@School", DbType.String, domainObj.School);
            db.AddInParameter(command, "@Experience", DbType.String, domainObj.Experience);
            db.AddInParameter(command, "@Idea", DbType.String, domainObj.Idea);
            db.AddInParameter(command, "@Price", DbType.Int32, domainObj.Price);
            db.AddInParameter(command, "@Mobile", DbType.String, domainObj.Mobile);
            db.AddInParameter(command, "@WxCode", DbType.String, domainObj.WxCode);
            //db.AddInParameter(command, "@Email", DbType.String, domainObj.Email);
            db.AddInParameter(command, "@ExistId", DbType.String, domainObj.ExistId);
            db.AddInParameter(command, "@Eexpert", DbType.String, domainObj.Eexpert);
            db.AddInParameter(command, "@AreaID", DbType.String, domainObj.AreaID);
            db.AddInParameter(command, "@Photo", DbType.String, domainObj.Photo);
            return command;
        }

        public void SetNewID(Database db, DbCommand command, ZX_DesignersEntity domainObj)
        {

        }
    }
}
