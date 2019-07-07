using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_Upload;
using ZXService.Common;
using System.Configuration;

namespace ZXService.WebService
{
    /// <summary>
    /// ImgUpload 的摘要说明
    /// </summary>
    public class ImgUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            ReturnRespone<ZX_UploadReturnEntity> result = new ReturnRespone<ZX_UploadReturnEntity>() { ResultInfo = new ZX_UploadReturnEntity() };
            string configString = SysConfig.IamgePath;

            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                string servicePath = @"serviceImg/";

                if (!Directory.Exists(path + servicePath))
                {
                    Directory.CreateDirectory(path + servicePath);
                }

                var file = context.Request.Files[0];
                if (file.FileName == "")
                {
                    result.IsSuccess = false;
                    result.Message = "请先导入文件";
                }
                else
                {

                    Stream stream = file.InputStream;
                    //这里可以对文件流做些什么  

                    servicePath += file.FileName;
                    if (file.InputStream.Length > 2000000)
                    {
                        result.IsSuccess = false;
                        result.Message = "图片大小不能超过2M";
                    }
                    else
                    {
                        byte[] buffer = new byte[file.InputStream.Length];

                        FileStream fs = new FileStream(path + servicePath, FileMode.Create, FileAccess.Write);

                        int count = 0;
                        while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, count);
                        }
                        //清空缓冲区
                        fs.Flush();
                        //关闭流
                        fs.Close();
                        result.IsSuccess = true;
                        result.Message = "上传成功！";
                        result.ResultInfo.servicePath = configString + servicePath;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.ToString();
            }
            context.Response.Write(JsonHelper.ObjectToJsonString(result));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}