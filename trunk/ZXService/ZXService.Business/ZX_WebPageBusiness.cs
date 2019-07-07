using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataAccess.ZX_AreaDa;
using ZXService.DataAccess.ZX_WebPageListDa;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_WebPageEntity;

namespace ZXService.Business
{
    public class ZX_WebPageBusiness
    {
        public static ReturnRespone<List<ZX_WebPageInfoEntity>> GetWebpageList(ZX_WebPageEntryParameterEntity Entry)
        {
            ReturnRespone<List<ZX_WebPageInfoEntity>> result = new ReturnRespone<List<ZX_WebPageInfoEntity>>();
            SelectWebPageList dao = new SelectWebPageList();
            var r = dao.GetWebPageList(Entry);
            for (int i = 0; i < r.Count; i++)
            {
                r[i].ImageFile = ValidHelper.ChangeImageFill(r[i].ImageFile);
            }

            if (r != null && r.Any())
            {
                result.IsSuccess = true;
                result.Message = "调用成功";
                result.ResultInfo = r.ToList();
            }
            else
            {
                result.IsSuccess = true;
                result.Message = "调用失败";
            }
            return result;
        }
    }
}
