using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataAccess.ZX_AreaDa;
using ZXService.DataContracts;
using ZXService.DataContracts.WebApi;
using ZXService.DataContracts.ZX_AreaEntity;

namespace ZXService.Business
{
    public class ZX_AreaBusiness
    {

        /// <summary>
        /// 获取用户信息的实现方法
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        //public static ReturnRespone<ZX_AreaInfoEntity> GetUser(string arg)
        //{
        //    //定义一个返回的标准格式对象
        //}

        public static ReturnRespone<List<ZX_AreaInfoEntity>> GetArea()
        {
            ReturnRespone<List<ZX_AreaInfoEntity>> result = new ReturnRespone<List<ZX_AreaInfoEntity>>();
            AreaDao dao = new AreaDao();
            var r = dao.GetAreaList();
            if (r != null && r.Any())
            {
                result.IsSuccess = true;
                result.Message = "调用成功";
                result.ResultInfo = r.ToList();
            }
            else
            {
                result.IsSuccess = true;
                result.Message = "调用失败";
            }
            return result;
        }

        public static ReturnRespone<ZX_AreaEntity> GetArea(string location)
        {
            BadiDuGetArea api = WebApi.GetAreaToLoc(location);
            var AllArea = AllCache.GetAllArea();
            ReturnRespone<ZX_AreaEntity> result = new ReturnRespone<ZX_AreaEntity>();
            ZX_AreaEntity r = new ZX_AreaEntity();

            r.country = api.result.addressComponent.country;
            r.formatted_address = api.result.formatted_address;
            r.country_code = api.result.addressComponent.country_code;
            r.city_level = api.result.addressComponent.city_level;

            r.provinceID = LikSearch(api.result.addressComponent.province, AllArea).ID;
            r.cityID = LikSearch(api.result.addressComponent.city, AllArea).ID;
            r.districtID = LikSearch(api.result.addressComponent.district, AllArea).ID;

            r.province = LikSearch(api.result.addressComponent.province, AllArea).AreaName;
            r.city = LikSearch(api.result.addressComponent.city, AllArea).AreaName;
            r.district = LikSearch(api.result.addressComponent.district, AllArea).AreaName;

            if (r != null)
            {
                result.IsSuccess = true;
                result.Message = "调用成功";
                result.ResultInfo = r;
            }
            else
            {
                result.IsSuccess = true;
                result.Message = "调用失败";
            }
            return result;
        }

        private static ZX_AreaInfoEntity LikSearch(string arg, List<ZX_AreaInfoEntity> list)
        {
            ZX_AreaInfoEntity r = new ZX_AreaInfoEntity();
            if (null != list && list.Any())
            {
                foreach (var item in list)
                {
                    if (item.AreaName.Contains(arg))
                    {
                        r = item;
                    }
                    else if (arg.Contains(item.AreaName))
                    {
                        r = item;
                    }
                }
            }
            return r;
        }
    }
}
