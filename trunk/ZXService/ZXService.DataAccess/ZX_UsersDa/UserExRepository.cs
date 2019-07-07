using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_UserEntity;

namespace ZXService.DataAccess.ZX_UsersDa
{
    /// <summary>
    /// 通过继承数据访问层的类可以实现正删改查，传T类型
    /// </summary>
    public class UserExRepository : Repository<ZX_UserInfoEntity>
    {
        /// <summary>
        /// 获取用户信息的方法
        /// </summary>
        /// <param name="arg">T实体对象</param>
        /// <returns>返回获取到的用户信息数组</returns>
        public List<ZX_UserInfoEntity> GetUsersEntity(ZX_UserInfoEntity arg)
        {
            //定义数据库查询字符串
            var selectfac = new SelectUserFac();
            //父类继承的查找方法   参数 分别为查询字符串，查询出来的数据的实体对象，和查询条件参数
            return Find<ZX_UserInfoEntity>(selectfac, new DataUserFactory(), arg);
        }
    }
}
