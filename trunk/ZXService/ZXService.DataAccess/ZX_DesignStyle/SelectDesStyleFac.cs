using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;


namespace ZXService.DataAccess.ZX_DesignStyle
{
    public class SelectDesStyleFac : ISelectionFactory<ZX_DesignStyleEntity>
    {

        public DbCommand ConstructSelectCommand(Database db, ZX_DesignStyleEntity idObject)
        {
            string sql = @" Select    ID,
                                      StyleType,
                                      ImageFile,
                                      Remark,
                                      ImageDesc
                               From  ZX_DesignStyle";
            DbCommand command = db.GetSqlStringCommand(sql);
            return command;
        }
    }
}
