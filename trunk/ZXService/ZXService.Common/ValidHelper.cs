using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ZXService.Common
{
    public class ValidHelper
    {
        public static bool IsMobile(string text)
        {
            bool bl = true;
            try
            {

                //电信
                string dianxin = @"^1[3578][013479]\d{8}$";
                Regex dReg = new Regex(dianxin);
                //联通手机号正则       
                string liantong = @"^1[34578][01256]\d{8}$";
                Regex tReg = new Regex(liantong);
                //移动手机号正则        
                string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
                Regex yReg = new Regex(yidong);

                if (!dReg.IsMatch(text.Trim()) && !tReg.IsMatch(text.Trim()) && !yReg.IsMatch(text.Trim()))//用正则表达式验证手机号码是否符合3大运营商的号码规则
                {
                    bl = false;
                }
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
            }
            return bl;
        }

        public static bool IsEmail(string text)
        {

            bool bl = true;
            try
            {
                //电信
                string dianxin = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
                Regex dReg = new Regex(dianxin);

                if (!dReg.IsMatch(text.Trim()))//用正则表达式验证手机号码是否符合3大运营商的号码规则
                {
                    bl = false;
                }
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
            }
            return bl;
        }

        /// <summary>
        /// 截取数据库图片地址，获取服务器真实图片地址
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string ChangeImageFill(string arg)
        {
            string r = string.Empty;
            try
            {

                if (!string.IsNullOrEmpty(arg))
                {
                    string a = string.Empty;
                    if (!arg.StartsWith("https:") && !arg.StartsWith("http:"))
                        r = SysConfig.ImageUrl + arg.Substring(37);
                    else
                        r = arg;
                }
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex);
            }
            return r;
        }
    }
}
