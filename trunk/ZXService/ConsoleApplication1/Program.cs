using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("按任意键请求");
                Console.ReadLine();

                string Url = "http://localhost:9223/ZXAppService.svc/GetDesignerList";
                //string Url = "http://www.zjecht.cn/GetDesignerList";
                string Data = "{\"PageSize\":3,\"PageIndex\":1,\"oder\":6}";

                string result = SendRequest(Url, Data);

                Console.WriteLine("按任意键循环");
                Console.ReadLine();

            } while (true);

        }

        private static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static string SendRequest(string Url, string Data, string Param = "")
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
    }
}
