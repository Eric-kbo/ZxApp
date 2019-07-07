using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ZXService.DataAccess
{
    public interface IInsertFactory<TDomainObject>
    {
        /// <summary>
        /// Generate the insert command.
        /// </summary>
        /// <param name="db">Entlib Database object to generate command for.</param>
        /// <param name="domainObj">Domain object to insert.</param>
        /// <returns>Initialized DbCommand object.</returns>
        DbCommand ConstructInsertCommand(Database db, TDomainObject domainObj);

        /// <summary>
        /// 从命令中读取新插入的对象的标识
        /// 并将它设置在域对象中。
        /// </summary>
        /// <param name="db">该命令产生entlib数据库对象。</param>
        /// <param name="command">成功地执行命令，现在持有的ID。这应该是相同的命令对象，返回constructinsertcommand。</param>
        /// <param name="domainObj">域对象，通过对constructinsertcommand。这将有由数据库集分配的标识</param>
        void SetNewID(Database db, DbCommand command, TDomainObject domainObj);

    }
}
