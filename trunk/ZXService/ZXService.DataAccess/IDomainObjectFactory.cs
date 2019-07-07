using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZXService.DataAccess
{
    public interface IDomainObjectFactory<TDomainObject>
    {
        TDomainObject Construct(IDataReader reader);
    }

    /// <summary>
    /// 用于分页返回
    /// </summary>
    /// <typeparam name="TIdentityObject">对象</typeparam>
    public interface IDomainObjectPageFactory<TIdentityObject>
    {
        /// <summary>
        /// 用于构造分页返回数据
        /// </summary>
        /// <param name="reader">数据读取对象</param>
        /// <param name="idObject">赋值对象</param>
        void Construct(IDataReader reader, TIdentityObject idObject);
    }
}
