using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataAccess.ZX_CaseDa;
using ZXService.DataAccess.ZX_DeCaseImage;
using ZXService.DataAccess.ZX_DesCaaloneDa;
using ZXService.DataAccess.ZX_Designer;
using ZXService.DataAccess.ZX_DesignerListDa;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeCase;
using ZXService.DataContracts.ZX_DeCaseImage;
using ZXService.DataContracts.ZX_DesigerListEntity;
using ZXService.DataContracts.ZX_DesignEntity;
using ZXService.Common;
using ZXService.DataContracts.ZX_DesigerReComd;
using ZXService.DataAccess.ZX_AD;
using ZXService.DataAccess.ZX_DesignStyle;

namespace ZXService.Business
{
    public class ZX_DesignerBusiness
    {
        public ReturnRespone<ZX_DesignersEntity> AddDesigner(ZX_DesignersEntity model)
        {
            ReturnRespone<ZX_DesignersEntity> result = new ReturnRespone<ZX_DesignersEntity>();

            DesignersExRepository dep = new DesignersExRepository();
            if (null != model)
            {

                if (ValidHelper.IsMobile(model.Mobile))
                {
                    dep.AddDesignerInfo(model);
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "手机号格式不对";
                }
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "请求失败,数据为空";
            }
            return result;
        }

        /// <summary>
        /// 设计师详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ReturnRespone<ZX_DesignersEntity> SelDesigner(string ID)
        {
            ReturnRespone<ZX_DesignersEntity> result = new ReturnRespone<ZX_DesignersEntity>();
            result.ResultInfo = new ZX_DesignersEntity();

            DesignersExRepository dep = new DesignersExRepository();
            DesCaaLoneRepository repCase = new DesCaaLoneRepository();

            var data = new ZX_DesignersEntity();

            var Caselist = new List<ZX_DeCaseInfoEntity>();

            var r = dep.SelDesignerDetail(new ZX_DesignersEntity { ID = ID });
            Caselist = repCase.GetDeCaseEntityList(ID);

            if (r.Any())
            {
                data = r.FirstOrDefault();
                for (int i = 0; i < Caselist.Count(); i++)
                {
                    Caselist[i].ImageFile = ValidHelper.ChangeImageFill(Caselist[i].ImageFile);
                }
                data.Photo = ValidHelper.ChangeImageFill(data.Photo);
                data.CaseList = Caselist;
            }

            result.ResultInfo = data;
            result.IsSuccess = true;
            result.Message = "";

            if (result.ResultInfo != null)
            {
                result.IsSuccess = true;
                result.Message = "请求成功";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "没有取到数据";
            }

            return result;
        }

        /// <summary>
        /// 查询广告
        /// </summary>
        /// <returns></returns>
        public ReturnRespone<List<ZX_ADEntity>> SelADEntity(int Position)
        {
            ReturnRespone<List<ZX_ADEntity>> result = new ReturnRespone<List<ZX_ADEntity>>() { ResultInfo = new List<ZX_ADEntity>() };


            ADExRepository ad = new ADExRepository();
            var data = ad.SelAdInfo(new ZX_ADEntity() { Position = Position });
            if (null != data && data.Any())
            {
                foreach (var item in data)
                {
                    ZX_ADEntity addata = new ZX_ADEntity();
                    addata = item;
                    addata.ImageFile = ValidHelper.ChangeImageFill(item.ImageFile);
                    result.ResultInfo.Add(addata);
                }

            }

            result.IsSuccess = true;
            result.Message = "请求成功";
            if (result.ResultInfo == null)
            {
                result.IsSuccess = false;
                result.Message = "请求错误";
            }

            return result;
        }

        /// <summary>
        /// 查询设计师列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnRespone<List<ZX_DesignersEntity>> SelDes(ZX_DesignersEntity model)
        {
            ReturnRespone<List<ZX_DesignersEntity>> result = new ReturnRespone<List<ZX_DesignersEntity>>() { ResultInfo = new List<ZX_DesignersEntity>() };

            DesignersExRepository des = new DesignersExRepository();

            var desList = des.SelDesignerInfo(model);

            for (int i = 0; i < desList.Count(); i++)
            {
                desList[i].ImageFile = ValidHelper.ChangeImageFill(desList[i].ImageFile);
                desList[i].Photo = ValidHelper.ChangeImageFill(desList[i].Photo);
                if (!result.ResultInfo.Exists(x => x.ID == desList[i].ID))
                {
                    result.ResultInfo.Add(desList[i]);
                }
            }

            result.Message = "请求成功";
            result.IsSuccess = true;

            if (result.ResultInfo == null)
            {
                result.Message = "请求失败";
                result.IsSuccess = false;
            }
            return result;
        }

        /// <summary>
        /// 查询设计风格
        /// </summary>
        /// <returns></returns>
        public ReturnRespone<List<ZX_DesReComdEntity>> SelectDesignStyle()
        {
            ReturnRespone<List<ZX_DesReComdEntity>> result = new ReturnRespone<List<ZX_DesReComdEntity>>();
            List<ZX_DesignStyleEntity> desStylelist = new List<ZX_DesignStyleEntity>();
            List<ZX_DesReComdEntity> list = new List<ZX_DesReComdEntity>();

            DesStyleRepository style = new DesStyleRepository();

            desStylelist = style.SelDesStyleInfo();
            List<int> Typelist = null;
            if (desStylelist != null && desStylelist.Count > 0)
            {
                Typelist = desStylelist.Select(a => a.StyleType).Distinct().ToList();
            }

            result.Message = "请求失败";
            result.IsSuccess = false;
            if (Typelist != null && Typelist.Count > 0)
            {
                foreach (var item in Typelist)
                {
                    List<ZX_DesignStyleEntity> TypeInfo = desStylelist.Where<ZX_DesignStyleEntity>(a => a.StyleType == item).ToList();
                    ZX_DesReComdEntity des = new ZX_DesReComdEntity();

                    for (int i = 0; i < TypeInfo.Count(); i++)
                    {
                        TypeInfo[i].ImageFile = ValidHelper.ChangeImageFill(TypeInfo[i].ImageFile);
                    }

                    des.DesStyleList = TypeInfo;
                    des.Count = TypeInfo.Count();
                    des.ImageDesc = TypeInfo.First().ImageDesc;
                    list.Add(des);
                }
                result.ResultInfo = list;
                result.Message = "请求成功";
                result.IsSuccess = true;
            }
            return result;
        }



        #region 暂无用
        public ReturnRespone<List<ZX_DecaseEntity>> SelDecaseImg(string ID, int PageIndex, int PageSize)
        {
            ReturnRespone<List<ZX_DecaseEntity>> result = new ReturnRespone<List<ZX_DecaseEntity>>();

            DeCaseInfoRepository dei = new DeCaseInfoRepository();


            result.ResultInfo = dei.GetDecaseInfo(new ZX_DecaseEntity { DesignerID = Guid.Parse(ID) }).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();


            if (result.ResultInfo != null)
            {
                result.IsSuccess = true;
                result.Message = "请求成功";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "没有取到数据";
            }

            return result;
        }

        public ReturnRespone<List<ZX_DeCaseImageEntity>> SelImgInfo(string ID)
        {
            ReturnRespone<List<ZX_DeCaseImageEntity>> result = new ReturnRespone<List<ZX_DeCaseImageEntity>>();

            DeCaseImgRepository dci = new DeCaseImgRepository();

            result.ResultInfo = dci.SelCaseImgInfo(new ZX_DeCaseImageEntity { CaseID = ID });


            if (result.ResultInfo != null)
            {
                result.IsSuccess = true;
                result.Message = "请求成功";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "没有取到数据";
            }

            return result;
        }
        #endregion

        public static ReturnRespone<PageDataReturnObj<ZX_DesignerListEntity>> GetDesignerList(ZX_DesignerListCondsEntity arg)
        {
            PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity> pagedata = new PageDataEntity<ZX_DesignerListEntity, ZX_DesignerListCondsEntity>();

            ReturnRespone<PageDataReturnObj<ZX_DesignerListEntity>> result = new ReturnRespone<PageDataReturnObj<ZX_DesignerListEntity>>();
            PageDataReturnObj<ZX_DesignerListEntity> pr = new PageDataReturnObj<ZX_DesignerListEntity>();
            pr.DateList = new List<ZX_DesignerListEntity>();
            var r = new List<ZX_DesignerListEntity>();
            var t = new List<ZX_DeCaseInfoEntity>();

            pagedata.PageSize = arg.PageSize == 0 ? 10 : arg.PageSize;
            pagedata.PageIndex = arg.PageIndex == 0 ? 1 : arg.PageIndex;
            pagedata.ObjQueryConditions = arg;

            DesRepository rep = new DesRepository();
            DesCaaLoneRepository repCase = new DesCaaLoneRepository();

            r = rep.GetDesignerList(pagedata);
            t = repCase.GetDeCaseEntityList();

            if (r == null)
            {
                result.ResultInfo = pr;
                result.IsSuccess = false;
                result.Message = "无数据";
            }
            else
            {
                pr.TotalCount = pagedata.TotalCount;
                pr.PageCount = pagedata.TotalCount / pagedata.PageSize;
                pr.PageIndex = pagedata.PageIndex;
                if (r.Any())
                {
                    if (null != t && t.Any())
                    {
                        //foreach (var item in r)
                        //{
                        //    var casedata = t.Where(a => a.ID == item.ID).FirstOrDefault();

                        //    item.ImageFile = casedata != null ? casedata.ImageFile : "";
                        //}
                        for (int i = 0; i < r.Count; i++)
                        {
                            var casedata = t.Where(a => a.DesignerID == r[i].ID).FirstOrDefault();

                            r[i].ImageFile = casedata != null ? casedata.ImageFile : "";

                        }
                    }
                    pr.DateList = r;
                }
                result.ResultInfo = pr;
                result.IsSuccess = true;
                result.Message = "";
            }

            return result;
        }


        public int UpFileImg(string path, string DeId)
        {
            int i = UpdateImgFIile.UpImgFile(path, DeId);
            return i;
        }

        public string SelDesigerId(string guid)
        {
            string DesignerId = UpdateImgFIile.SelDesigerId(guid);
            return DesignerId;
        }

        public int AddesigerImg(ZX_DesignersEntity model)
        {
            int i = UpdateImgFIile.AddDeCase(model);

            //if (i >= 0 && model.ImageFileList.Any())
            //{
            //    ZX_DecaseEntity data = new ZX_DecaseEntity();
            //    data.ImageFile = model.ImageFileList[0].ImageFile;
            //    DeCaseInfoRepository dei = new DeCaseInfoRepository();

            //    ZX_DecaseEntity caseData = dei.GetDacsInfoDao(data);
            //    model.ID = caseData.ID.ToString();

            //    int j = UpdateImgFIile.AddDeCaseImg(model);
            //}

            return i;
        }

    }
}
