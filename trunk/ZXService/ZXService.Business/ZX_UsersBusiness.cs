using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataAccess.ZX_UsersDa;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_UserEntity;

namespace ZXService.Business
{
    /// <summary>
    /// 用户信息的逻辑处理类
    /// </summary>
    public class ZX_UsersBusiness
    {
        /// <summary>
        /// 获取用户信息的实现方法
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static ReturnRespone<List<ZX_UserInfoEntity>> GetUser(string arg)
        {
            //定义一个返回的标准格式对象
            ReturnRespone<List<ZX_UserInfoEntity>> result = new ReturnRespone<List<ZX_UserInfoEntity>>();
            //实例化一个数据处理层接收的参数
            ZX_UserInfoEntity data = new ZX_UserInfoEntity() { ID = arg };
            //实力化一个数据处理的对象
            UserExRepository rep = new UserExRepository();
            //获取数据
            result.ResultInfo = rep.GetUsersEntity(data);

            //不为空就返回 true
            if (result.ResultInfo != null)
            {
                result.IsSuccess = true;
                result.Message = "请求成功";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "没有取到数据";
            }

            return result;
        }
    }
}
