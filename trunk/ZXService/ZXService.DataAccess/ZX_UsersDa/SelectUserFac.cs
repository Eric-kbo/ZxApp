using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_UserEntity;

namespace ZXService.DataAccess.ZX_UsersDa
{
    /// <summary>
    /// 服务器查询类实现，继承查询接口 传T类型数据
    /// </summary>
    public class SelectUserFac : ISelectionFactory<ZX_UserInfoEntity>
    {
        /// <summary>
        /// 实现接口的方法   会自动生成
        /// </summary>
        /// <param name="db"></param>
        /// <param name="idObject">参数实体对象</param>
        /// <returns></returns>
        public DbCommand ConstructSelectCommand(Database db, ZX_UserInfoEntity idObject)
        {
            string sql = @"select * from ZX_User where ID=@ID";
            DbCommand command = db.GetSqlStringCommand(sql);
            //参数形式传入查询条件，可以放sql注入
            db.AddInParameter(command, "@ID", DbType.String, idObject.ID);

            return command;
        }
    }
}
