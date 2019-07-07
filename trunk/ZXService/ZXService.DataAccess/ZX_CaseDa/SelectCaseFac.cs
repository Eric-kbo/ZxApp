using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeCase;

namespace ZXService.DataAccess.ZX_CaseDa
{
    public class SelectCaseFac : ISelectionFactoryWithOutputTotalCount<PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity>>
    {
        public DbCommand ConstructSelectCommand(Database db, PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity> idObject)
        {
            string sql = @" SELECT * FROM
	                            (
		                            SELECT
			                            ROW_NUMBER () OVER (ORDER BY a.ClickCount) Rank,
			                            a.*
		                            FROM
			                            (
				                            SELECT
					                            d.imageFile,
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
				                            LEFT JOIN ZX_DeCase d ON t.ID = d.DesignerID
			                            ) a
	                            ) aa 
                            WHERE
	                            Rank > {0}
                            AND Rank <= {1}
";
            sql = string.Format(sql, idObject.PageSize * (idObject.PageIndex - 1), idObject.PageSize * (idObject.PageIndex));

            DbCommand command = db.GetSqlStringCommand(sql);

            return command;
        }

        public DbCommand ConstructSelectCountCommand(Database db, PageDataEntity<ZX_DeCaseInfoEntity, ZX_DeCaseCondsEntity> idObject)
        {
            string sql = @"  SELECT
	                        COUNT (1) RN
                        FROM
	                        (
		                        SELECT
			                        d.imageFile,
			                        t.*
		                        FROM
			                        ZX_Designer t
		                        LEFT JOIN ZX_DeCase d ON t.ID = d.DesignerID
	                        ) a
        ";

            DbCommand command = db.GetSqlStringCommand(sql);
            return command;
        }

    }
}
