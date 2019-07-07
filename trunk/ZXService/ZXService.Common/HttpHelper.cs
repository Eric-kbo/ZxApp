using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization;
using System.Text;

namespace ZXService.Common
{
    public class HttpHelper
    {
        public string SendRequest(HttpModel model)
        {
            string ret = string.Empty;
            try
            {
                HttpWebRequest webRequest = CreateWebRequest(model);
                using (StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream(), model.Encode))
                {
                    ret = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Log.GetLogService().Error(ex.Message);
                return "服务器错误";
            }
            return ret;
        }

        public string SendRequest(string Url, string Data, string Param = "")
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(Url));

            webRequest.Proxy = null;
            var ret = "";
            System.Net.ServicePointManager.DefaultConnectionLimit = 10000;
            System.GC.Collect();
            byte[] byteArray = Encoding.GetEncoding("UTF-8").GetBytes(Data); //转化

            webRequest.Method = "post";
            webRequest.KeepAlive = false;
            webRequest.Timeout = 10000;
            webRequest.ReadWriteTimeout = 100000;
            webRequest.Accept = "application/json";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = byteArray.Length;


            var newStream = webRequest.GetRequestStream();

            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            var response = (HttpWebResponse)webRequest.GetResponse();


            using (StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                ret = sr.ReadToEnd();
            }

            webRequest.Abort();
            return ret;
        }

        public string SendRequest(string Url, string Param = "")
        {
            string ret = string.Empty;
            var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(Url + Param));
            webRequest.Method = "GET";

            webRequest.CookieContainer = new CookieContainer();

            webRequest.KeepAlive = true;
            webRequest.ReadWriteTimeout = System.Threading.Timeout.Infinite;
            webRequest.UserAgent = "Mozilla / 4.0(compatible; MSIE 8.0; Windows NT 6.1; Trident / 4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2);";

            using (StreamReader sr = new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }

        private HttpWebRequest CreateWebRequest(HttpModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Url))
            {
                throw new Exception("URL为空");
            }
            HttpWebRequest webRequest = null;
            byte[] datas;
            Stream requestStream;

            switch (model.Method)
            {
                case "GET":
                    if (!String.IsNullOrWhiteSpace(model.Param))
                    {
                        model.Url += "?" + model.Param;
                    }
                    if (model.Url.StartsWith("https:"))
                    {
                        System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// SecurityProtocolType.Tls1.2;  | SecurityProtocolType.Ssl3;
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    }


                    webRequest = (HttpWebRequest)WebRequest.Create(model.Url);
                    webRequest.Method = model.Method;
                    if (model.ContentType != null)
                    {
                        webRequest.ContentType = model.ContentType;
                    }

                    webRequest.CookieContainer = new CookieContainer();

                    webRequest.KeepAlive = true;
                    webRequest.ReadWriteTimeout = System.Threading.Timeout.Infinite;
                    webRequest.Timeout = Convert.ToInt32(model.Interval);
                    webRequest.UserAgent = "Mozilla / 4.0(compatible; MSIE 8.0; Windows NT 6.1; Trident / 4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2);";
                    break;
                case "POST":
                    if (!String.IsNullOrWhiteSpace(model.Param))
                    {
                        model.Url += "?" + model.Param;
                    }

                    if (model.Url.StartsWith("https:"))
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    }

                    webRequest = (HttpWebRequest)WebRequest.Create(model.Url);
                    webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                    webRequest.Method = model.Method;
                    if (model.ContentType != null)
                    {
                        webRequest.ContentType = model.ContentType;
                    }
                    webRequest.CookieContainer = new CookieContainer();

                    webRequest.KeepAlive = true;
                    webRequest.ReadWriteTimeout = System.Threading.Timeout.Infinite;
                    webRequest.Timeout = Convert.ToInt32(model.Interval);
                    webRequest.UserAgent = "Mozilla / 4.0(compatible; MSIE 8.0; Windows NT 6.1; Trident / 4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2);";

                    if (!String.IsNullOrWhiteSpace(model.Data))
                    {
                        datas = model.Encode.GetBytes(model.Data);
                    }
                    else
                    {
                        datas = model.Encode.GetBytes(" ");
                    }
                    if (datas != null)
                    {
                        webRequest.ContentLength = datas.Length;
                        requestStream = webRequest.GetRequestStream();
                        requestStream.Write(datas, 0, datas.Length);
                        requestStream.Flush();
                        requestStream.Close();
                    }
                    break;
                default:
                    break;
            }
            return webRequest;
        }

        private bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

    }


    [DataContract]
    public class HttpModel
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        [DataMember]
        public string Url { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [DataMember]
        public string Param { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        [DataMember]
        public string Method { get; set; }

        /// <summary>
        /// 请求类型
        /// </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary>
        /// 编码格式
        /// </summary>
        [DataMember]
        public Encoding Encode { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        [DataMember]
        public DateTime Interval { get; set; }
    }
}
