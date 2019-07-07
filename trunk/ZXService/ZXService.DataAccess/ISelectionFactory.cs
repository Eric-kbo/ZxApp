using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ZXService.DataAccess
{
    public interface ISelectionFactory<TIdentityObject>
    {
        DbCommand ConstructSelectCommand(Database db, TIdentityObject idObject);

    }

    public interface ISelectionFactoryWithOutputTotalCount<TIdentityObject>
    {
        DbCommand ConstructSelectCommand(Database db, TIdentityObject idObject);
        DbCommand ConstructSelectCountCommand(Database db, TIdentityObject idObject);
    }
}
