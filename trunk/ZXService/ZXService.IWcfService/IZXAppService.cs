using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using ZXService.DataContracts.Common;
using ZXService.DataContracts.Paras;
using ZXService.DataContracts.ZX_AreaEntity;
using ZXService.DataContracts.ZX_DeImageEntity;
using ZXService.DataContracts.ZX_DesigerListEntity;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataContracts.ZX_DesignEntity;
using ZXService.DataContracts.ZX_UserEntity;
using ZXService.DataContracts.ZX_WebPageEntity;

namespace ZXService.IWcfService
{
    [ServiceContract]
    public interface IZXAppService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetData", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetData();

        /// <summary>
        /// 获取用户（测试）
        /// </summary>
        /// <param name="ID">参数条件，前台传进来的数据</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetUser", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetUser(ZX_UserEntityConds ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetArea", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetArea(string ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddDesigner", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddDesigner(ZX_DesignersEntity model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetDesignerInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetDesignerInfo(ZX_DesignersEntity ID);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/GetDesignerInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string GetDEsignerInfoList();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SelDecaseImg", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SelDecaseImg(DeCaseImg arg);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SelImgInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SelImgInfo(string ID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/POSTDesignerList", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string POSTDesignerList(ZX_DesignerListCondsEntity arg);
          
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SelADEntity", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SelADEntity(ZX_ADEntity model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SelDes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SelDes(ZX_DesignersEntity model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SelectDesignStyle", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SelectDesignStyle();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/WebpageList", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string WebpageList(ZX_WebPageEntryParameterEntity Entry);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetAreaLocation", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetAreaLocation(ZX_AreaCond areaLocation);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetDescImageList", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetDescImageList(ImageDesigner arg);


        ////上传文件
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/UpLoadFile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //ZXService.DataContracts.Common.File.UpFileResult UpLoadFile(ZXService.DataContracts.Common.File.UpFile filestream);

        ////下载文件
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/DownLoadFile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //ZXService.DataContracts.Common.File.DownFileResult DownLoadFile(ZXService.DataContracts.Common.File.DownFile downfile); 




    }

}
