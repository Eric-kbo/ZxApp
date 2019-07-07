using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ZXService.Common
{
    public class SysConfig
    {
        /// <summary>
        /// 服务器图片路径
        /// </summary>
        public static string IamgePath
        {
            get
            {
                return ConfigurationManager.AppSettings["imageServicePath"].ToString();
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        public static string ImageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["imageService"].ToString();
            }
        }

        /// <summary>
        /// 百度地图接口Apk
        /// </summary>
        public static string BaiduApk
        {
            get
            {
                return ConfigurationManager.AppSettings["BaiduMapKey"].ToString();
            }
        }
    }
}
