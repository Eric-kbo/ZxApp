using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.WebApi;

namespace ZXService.Common
{
    public static class WebApi
    {
        public static BadiDuGetArea GetAreaToLoc(string loction)
        {
            HttpHelper http = new HttpHelper();
            string param = "?ak=" + SysConfig.BaiduApk + "&location=" + loction + "&output=json";
            string r = http.SendRequest("https://api.map.baidu.com/geocoder/v2/", param);

            return JsonHelper.JsonStringToObject<BadiDuGetArea>(r);
        }
    }
}
