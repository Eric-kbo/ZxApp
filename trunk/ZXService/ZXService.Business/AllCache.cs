using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.Cache;
using ZXService.Common;
using ZXService.DataAccess.ZX_AreaDa;
using ZXService.DataContracts.ZX_AreaEntity;

namespace ZXService.Business
{
    /// <summary>
    /// 所有缓存
    /// </summary>
    public class AllCache
    {
        #region 地区缓存

        static AreaCache AreaInfo = new AreaCache();
        static object LockInfoObj = new object();
        /// <summary>
        /// 获取所有区域信息
        /// </summary>
        /// <returns></returns>
        public static List<ZX_AreaInfoEntity> GetAllArea()
        {
            var result = new List<ZX_AreaInfoEntity>();
            try
            {
                if (AreaInfo.AllAreaCache != null && AreaInfo.AllAreaCache != null && AreaInfo.AllAreaCache.Any() && AreaInfo.Expiration >= DateTime.Now)
                {
                    return AreaInfo.AllAreaCache;
                }
                lock (LockInfoObj)
                {
                    if (AreaInfo.AllAreaCache != null && AreaInfo.AllAreaCache != null && AreaInfo.AllAreaCache.Any() && AreaInfo.Expiration >= DateTime.Now)
                    {
                        return AreaInfo.AllAreaCache;
                    }
                    AreaDao dao = new AreaDao();
                    AreaInfo.AllAreaCache = dao.GetAreaList().ToList();
                    AreaInfo.Expiration = DateTime.Now.AddMinutes(60);
                    return AreaInfo.AllAreaCache;
                }
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
            }
            return result;
        }

        #endregion
    }
}
