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
    public class SelectDesignerDetaillFac : ISelectionFactory<ZX_DesignersEntity>
    { 
        public DbCommand ConstructSelectCommand(Database db, ZX_DesignersEntity idObject)
        {
            string sql = @" select   t.DesignerTitle,
                                     t.DeName,
                                     t.Idea,
                                     t.Price,
                                     t.Eexpert,
                                     t.ID,
                                     t.Photo                       
              from  ZX_Designer  t  where t.ID=@ID";

            DbCommand command = db.GetSqlStringCommand(sql);

            if (!String.IsNullOrEmpty(idObject.ID))
            {
                db.AddInParameter(command, "@ID", DbType.String, idObject.ID);
            } 

            return command;
        }
    }
}
