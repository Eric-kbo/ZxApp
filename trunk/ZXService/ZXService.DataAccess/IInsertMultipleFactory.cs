using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ZXService.DataAccess
{
    public interface IInsertMultipleFactory<TDomainObject>
    {
        DbCommand ConstructInsertCommand(Database db);

        /// <summary>
        /// 设置Command对象参数值
        /// </summary>
        /// <param name="db"></param>
        /// <param name="command"></param>
        /// <param name="domainObj">Domain object to insert.</param>
        void SetParameterValue(Database db, DbCommand command, object parentId, TDomainObject domainObj);

        /// <summary>
        /// Read the ID of the newly inserted object out of the command
        /// and set it in the domain object.
        /// </summary>
        /// <param name="db">EntLib database object that the command was generated with.</param>
        /// <param name="command">Successfully executed command that now holds the id. This should be the same command object that was returned from ConstructInsertCommand.</param>
        /// <param name="domainObj">Domain object that was passed in to ConstructInsertCommand. This will have the ID assigned by the database set.</param>
        void SetNewID(Database db, DbCommand command, TDomainObject domainObj);

    }
}
