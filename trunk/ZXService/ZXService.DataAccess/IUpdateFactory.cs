using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ZXService.DataAccess
{
    public interface IUpdateFactory<TDomainObject>
    {
        DbCommand ConstructUpdateCommand(Database db, TDomainObject domainObject);
    }
}
