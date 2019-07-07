using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataContracts.ZX_DesignEntity;

namespace ZXService.DataAccess.ZX_Designer
{
    public class SelectDesignerFac : ISelectionFactory<ZX_DesignersEntity>
    {
        public DbCommand ConstructSelectCommand(Database db, ZX_DesignersEntity idObject)
        {
            string sql = @"

	select * FROM	(SELECT DISTINCT
			t.DesignerTitle,
			t.DeName,
			t.Idea,
			t.Price,
			t.Eexpert,
			t.ID,
			t.Photo,
			t.DeLevel,
			t.PreCount,
			d.ImageFile,
			z.AreaName,
			COUNT (d.ImageFile) workCount
		FROM
			ZX_Designer t
		LEFT JOIN ZX_Area z ON t.AreaID = z.ID
		LEFT JOIN ZX_DeCase d ON d.DesignerID = t.ID
		WHERE
    z.ID = @AreaID
		GROUP BY
			t.DesignerTitle,
			t.DeName,
			t.Idea,
			t.Price,
			t.Eexpert,
			t.ID,
			t.Photo,
			t.DeLevel,
			t.PreCount,
			d.ImageFile,
			z.AreaName) aa ";

            DbCommand command = db.GetSqlStringCommand(sql);

            db.AddInParameter(command, "@AreaID", DbType.String, idObject.AreaID);

            return command;
        }

    }
}
