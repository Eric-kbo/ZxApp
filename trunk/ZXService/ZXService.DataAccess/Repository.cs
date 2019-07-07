using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;
using ZXService.Common;

namespace ZXService.DataAccess
{
    public class Repository<TDomainObject>
    {
        //毫秒时长定义
        int milsecond = 500;

        private string databaseName;
        private Database db;
        public Repository()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public List<TDomainObject> Find<TIdentity>(ISelectionFactory<TIdentity> selectionFactory, IDomainObjectFactory<TDomainObject> domainObjectFactory, TIdentity identity)
        {
            List<TDomainObject> results = new List<TDomainObject>();
            using (DbCommand command = selectionFactory.ConstructSelectCommand(db, identity))
            {
                using (IDataReader rdr = db.ExecuteReader(command))
                {
                    while (rdr.Read())
                    {
                        results.Add(domainObjectFactory.Construct(rdr));
                    }
                }
            }

            return results;
        }

        public TDomainObject FindOne<TIdentity>(ISelectionFactory<TIdentity> selectionFactory, IDomainObjectFactory<TDomainObject> domainObjectFactory, TIdentity identity)
        {
            TDomainObject result = default(TDomainObject);
            using (DbCommand command = selectionFactory.ConstructSelectCommand(db, identity))
            {
                using (IDataReader rdr = db.ExecuteReader(command))
                {
                    result = domainObjectFactory.Construct(rdr);
                }
            }
            return result;
        }

        /// <summary>
        /// 查询数据，同时输出符合该条件的总记录数,从mssql迁移到mysql
        /// </summary>
        /// <typeparam name="TIdentity"></typeparam>
        /// <param name="selectionFactory"></param>
        /// <param name="domainObjectFactory"></param>
        /// <param name="domainObjectPageFactory">返回数据构造</param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public List<TDomainObject> FindWithOutpuTotalCount<TIdentity>(
    ISelectionFactoryWithOutputTotalCount<TIdentity> selectionFactory,
    IDomainObjectFactory<TDomainObject> domainObjectFactory,
    IDomainObjectPageFactory<TIdentity> domainObjectPageFactory,
    TIdentity identity)
        {
            List<TDomainObject> results = new List<TDomainObject>();

            using (DbCommand command = selectionFactory.ConstructSelectCommand(db, identity))
            {
                var now = DateTime.Now;

                using (IDataReader rdr = db.ExecuteReader(command))
                {
                    while (rdr.Read())
                    {
                        results.Add(domainObjectFactory.Construct(rdr));
                    }
                }
                var i = DateTime.Now.Subtract(now).TotalMilliseconds;
                if (i > milsecond)
                {
                    StringBuilder sb = new StringBuilder("");
                    foreach (var j in command.Parameters)
                    {
                        sb.Append(((DbParameter)j).Value == null ? "" : ((DbParameter)j).Value.ToString());
                        sb.Append(" ");
                    }
                    Log.GetLogService().Info("耗时:" + i + "___分页查询sql:" + command.CommandText);

                    Log.GetLogService().Info("参数:" + sb.ToString());
                }
            }

            using (DbCommand command = selectionFactory.ConstructSelectCountCommand(db, identity))
            {
                var now = DateTime.Now;

                using (IDataReader rdr = db.ExecuteReader(command))
                {
                    while (rdr.Read())
                    {
                        domainObjectPageFactory.Construct(rdr, identity);
                    }
                }

                var i = DateTime.Now.Subtract(now).TotalMilliseconds;
                if (i > milsecond)
                {
                    StringBuilder sb = new StringBuilder("");
                    foreach (var j in command.Parameters)
                    {
                        sb.Append(((DbParameter)j).Value == null ? "" : ((DbParameter)j).Value.ToString());
                        sb.Append(" ");
                    }
                    Log.GetLogService().Info("耗时:" + i + "___计算总记录sql:" + command.CommandText);

                    Log.GetLogService().Info("参数:" + sb.ToString());
                }
            }

            return results;
        }

        public object ExecuteScalar<TIdentity>(ISelectionFactory<TIdentity> selectionFactory, TIdentity identity)
        {
            object result = new object();
            using (DbCommand command = selectionFactory.ConstructSelectCommand(db, identity))
            {
                result = db.ExecuteScalar(command);
            }
            return result;
        }

        public int Add(IInsertFactory<TDomainObject> insertFactory, TDomainObject domainObject)
        {
            int result = 0;
            using (DbCommand command = insertFactory.ConstructInsertCommand(db, domainObject))
            {
                result = db.ExecuteNonQuery(command);
                insertFactory.SetNewID(db, command, domainObject);
            }
            return result;
        }

        public void AddMultiple(IInsertMultipleFactory<TDomainObject> insertFactory, object parentId,
            List<TDomainObject> domainObjs)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                using (DbCommand command = insertFactory.ConstructInsertCommand(db))
                {
                    foreach (TDomainObject obj in domainObjs)
                    {
                        insertFactory.SetParameterValue(db, command, parentId, obj);

                        db.ExecuteNonQuery(command);

                        insertFactory.SetNewID(db, command, obj);
                    }

                }

                ts.Complete();
            }
        }

        public int Delete<TIdentityObject>(IDeleteFactory<TIdentityObject> deleteFactory, TIdentityObject identityObject)
        {
            int result = 0;
            using (DbCommand command = deleteFactory.ConstructDeleteCommand(db, identityObject))
            {
                result = db.ExecuteNonQuery(command);
            }
            return result;
        }

        public int Update(IUpdateFactory<TDomainObject> updateFactory, TDomainObject domainObject)
        {
            int result = 0;
            using (DbCommand command = updateFactory.ConstructUpdateCommand(db, domainObject))
            {
                result = db.ExecuteNonQuery(command);
            }
            return result;
        }

        public int UpdateMultiple(IUpdateMultipleFactory<List<TDomainObject>> updateMultipleFactory, List<TDomainObject> domainObjects, int size)
        {
            int index = 1;
            int result = 0;
            if (size == 0)
            {
                size = domainObjects.Count;
            }
            int count = (int)Math.Ceiling((double)domainObjects.Count / (double)size);
            do
            {
                using (DbCommand command = updateMultipleFactory.ConstructUpdateCommand(db, domainObjects, index, size))
                {
                    result += db.ExecuteNonQuery(command);
                }
            } while (index <= count);
            return result;
        }

    }
}
