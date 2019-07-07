using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DesigerListEntity;

namespace ZXService.DataAccess.ZX_DesignerListDa
{
    class SelectDesFac : ISelectionFactoryWithOutputTotalCount<PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity>>
    {
        public DbCommand ConstructSelectCommand(Database db, PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity> idObject)
        {
            string sql = @"  SELECT * FROM
	                            (
		                            SELECT
			                            ROW_NUMBER () OVER (ORDER BY {2}) Rank,
			                            a.*
		                            FROM
			                            (
				                            SELECT
					                            t.ID,
					                            t.AreaID,
					                            t.DeName,
					                            t.Sex,
					                            t.WorkYear,
					                            t.School,
					                            t.Experience,
					                            t.Idea,
					                            t.Price,
					                            t.Mobile,
					                            t.WxCode,
					                            t.Email,
					                            t.ClickCount,
					                            t.ClinchCount,
					                            t.CheckState,
					                            t.CheckDesc
				                            FROM
					                            ZX_Designer t
			                            ) a
	                            ) aa 
                            WHERE
	                            Rank > {0}
                            AND Rank <= {1}
                            ";
            if (!string.IsNullOrEmpty(idObject.ObjQueryConditions.SortBy))
            {
                sql = string.Format(sql, idObject.PageSize * (idObject.PageIndex - 1), idObject.PageSize * (idObject.PageIndex), idObject.ObjQueryConditions.SortBy+" "+idObject.ObjQueryConditions.SortOrder);
            }
            else
            {
                sql = string.Format(sql, idObject.PageSize * (idObject.PageIndex - 1), idObject.PageSize * (idObject.PageIndex), " a.ClickCount desc ");
            }

            DbCommand command = db.GetSqlStringCommand(sql);

            return command;
        }

        public DbCommand ConstructSelectCountCommand(Database db, PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity> idObject)
        {
            string sql = @"  SELECT
	                        COUNT (1) RN
                        FROM
	                        (
		                        SELECT
			                        t.*
		                        FROM
			                        ZX_Designer t
	                        ) a";

            DbCommand command = db.GetSqlStringCommand(sql);
            return command;
        }
    }
}
