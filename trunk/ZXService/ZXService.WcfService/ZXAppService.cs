using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXService.Business;
using ZXService.Common;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DesignEntity;
using ZXService.DataContracts.ZX_AreaEntity;
using ZXService.DataContracts.ZX_UserEntity;
using ZXService.IWcfService;
using System.Net;
using System.IO;
using ZXService.DataContracts.ZX_DeCase;
using ZXService.DataContracts.ZX_DeCaseImage;
using ZXService.DataContracts.ZX_DesigerListEntity;
using ZXService.DataContracts.Common;
using ZXService.DataContracts.Paras;
using System.Web;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataContracts.ZX_WebPageEntity;
using ZXService.DataContracts.WebApi;
using ZXService.DataContracts.ZX_DeImageEntity;

namespace ZXService.WcfService
{
    public class ZXAppService : IZXAppService
    {
        public string GetData()
        {
            return "请求了！";
        }

        #region 地区相关

        public string GetArea(string ID)
        {
            var r = "";
            //定义返回数据的数据格式  遵循ReturnResponse的返回字符串规则
            ReturnRespone<List<ZX_AreaInfoEntity>> result = new ReturnRespone<List<ZX_AreaInfoEntity>>();

            try
            {
                //调用后台逻辑层代码
                result = ZX_AreaBusiness.GetArea();
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }


        public string GetAreaLocation(ZX_AreaCond areaLocation)
        {
            string r = string.Empty;
            ReturnRespone<ZX_AreaEntity> result = new ReturnRespone<ZX_AreaEntity>();
            try
            {
                //调用后台逻辑层代码
                result = ZX_AreaBusiness.GetArea(areaLocation.location);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }


        #endregion

        #region 用户相关

        //获取用户信息接口的实现方法
        public string GetUser(ZX_UserEntityConds ID)
        {
            var r = "";
            //定义返回数据的数据格式  遵循ReturnResponse的返回字符串规则
            ReturnRespone<List<ZX_UserInfoEntity>> result = new ReturnRespone<List<ZX_UserInfoEntity>>();

            try
            {
                //调用后台逻辑层代码
                result = ZX_UsersBusiness.GetUser(ID.ID);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;

        }

        #endregion

        #region 设计师相关

        /// <summary>
        /// 添加设计师
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddDesigner(ZX_DesignersEntity model)
        {
            var r = "";
            ReturnRespone<ZX_DesignersEntity> result = new ReturnRespone<ZX_DesignersEntity>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();
            try
            {
                ResultImg ImgInfo = new ResultImg();
                model.ExistId = Guid.NewGuid().ToString();

                model.ImageFile = model.ImageFileList.Any() ? model.ImageFileList[0].ImageFile : "";

                //调用后台逻辑层代码
                result = business.AddDesigner(model);
                if (result.IsSuccess == true)
                {
                    string guid = business.SelDesigerId(model.ExistId);
                    if (!string.IsNullOrEmpty(guid))
                    {
                        model.ID = guid;
                        int i = business.AddesigerImg(model);

                        result.Message = i <= 0 ? "设计师作品绑定失败" : "添加成功";
                    }
                    else
                    {
                        result.Message = "没有找到当前设计师";
                    }

                }
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }

        public string POSTDesignerList(ZX_DesignerListCondsEntity arg)
        {
            var r = "";
            //定义返回数据的数据格式  遵循ReturnResponse的返回字符串规则
            ReturnRespone<PageDataReturnObj<ZX_DesignerListEntity>> result = new ReturnRespone<PageDataReturnObj<ZX_DesignerListEntity>>();
            try
            {
                //调用后台逻辑层代码
                result = ZX_DesignerBusiness.GetDesignerList(arg);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }

        /// <summary>
        /// 设计师列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string SelDes(ZX_DesignersEntity model)
        {
            var r = "";
            ReturnRespone<List<ZX_DesignersEntity>> result = new ReturnRespone<List<ZX_DesignersEntity>>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();
            try
            {
                result = business.SelDes(model);
                r = JsonHelper.ObjectToJsonString(result);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            return r;
        }

        /// <summary>
        /// 设计师详情
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string GetDesignerInfo(ZX_DesignersEntity ID)
        {
            var r = "";
            ReturnRespone<ZX_DesignersEntity> result = new ReturnRespone<ZX_DesignersEntity>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();

            try
            {
                if (null != ID && !string.IsNullOrEmpty(ID.ID))
                {
                    //调用后台逻辑层代码
                    result = business.SelDesigner(ID.ID);
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "请传入正确的对象";
                }
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }

        /// <summary>
        /// 装修风格
        /// </summary>
        /// <returns></returns>
        public string SelectDesignStyle()
        {
            var r = "";
            ReturnRespone<List<ZX_DesReComdEntity>> result = new ReturnRespone<List<ZX_DesReComdEntity>>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();

            try
            {
                result = business.SelectDesignStyle();
                r = JsonHelper.ObjectToJsonString(result);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            return r;
        }

        /// <summary>
        /// 设计师详情页查看图片功能
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string GetDescImageList(ImageDesigner arg)
        {
            var r = string.Empty;
            ReturnRespone<ImageDesigner> result = new ReturnRespone<ImageDesigner>();
            ZX_DeImageBusiness business = new ZX_DeImageBusiness();

            try
            {
                //调用后台逻辑层代码
                result = business.GetDeImageList(arg.DeID);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }
        #endregion

        #region 首页相关


        #endregion

        #region 设计师图片功能
        public string SelDecaseImg(DeCaseImg arg)
        {
            var r = "";
            ReturnRespone<List<ZX_DecaseEntity>> result = new ReturnRespone<List<ZX_DecaseEntity>>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();

            try
            {
                //调用后台逻辑层代码
                result = business.SelDecaseImg(arg.ID, arg.PageIndex, arg.PageSize);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }

        public string SelImgInfo(string ID)
        {
            var r = "";
            ReturnRespone<List<ZX_DeCaseImageEntity>> result = new ReturnRespone<List<ZX_DeCaseImageEntity>>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();

            try
            {
                //调用后台逻辑层代码
                result = business.SelImgInfo(ID);
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }

        #endregion

        #region 文章相关
        public string WebpageList(ZX_WebPageEntryParameterEntity Entry)
        {
            var r = "";
            //定义返回数据的数据格式  遵循ReturnResponse的返回字符串规则
            ReturnRespone<List<ZX_WebPageInfoEntity>> result = new ReturnRespone<List<ZX_WebPageInfoEntity>>();

            try
            {
                //调用后台逻辑层代码
                result = ZX_WebPageBusiness.GetWebpageList(Entry);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            //将最后要返回的对象转成json格式传到前端
            r = JsonHelper.ObjectToJsonString(result);
            return r;
        }
        #endregion

        #region 广告

        public string SelADEntity(ZX_ADEntity model)
        {
            var r = "";
            ReturnRespone<List<ZX_ADEntity>> result = new ReturnRespone<List<ZX_ADEntity>>();
            ZX_DesignerBusiness business = new ZX_DesignerBusiness();

            try
            {
                result = business.SelADEntity(model.Position);
                r = JsonHelper.ObjectToJsonString(result);
            }
            catch (Exception ex)
            {
                //如果报错则返回false并且返回错误信息
                result.IsSuccess = false;
                result.Message = ex.Message;
                Log.GetLogService().Error(ex);
            }
            return r;
        }
        #endregion


    }
}
