using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.Common;
using ZXService.DataAccess.ZX_DeImageDa;
using ZXService.DataContracts;
using ZXService.DataContracts.ZX_DeImageEntity;

namespace ZXService.Business
{
    public class ZX_DeImageBusiness
    {
        public ReturnRespone<ImageDesigner> GetDeImageList(string ID)
        {

            ReturnRespone<ImageDesigner> result = new ReturnRespone<ImageDesigner>() { ResultInfo = new ImageDesigner() };
            ImageDesigner r = new ImageDesigner();
            r.DeCaseList = new List<ZX_ImageList>();

            DeImageDao dao = new DeImageDao();

            r = dao.GetDeDisenger(ID);
            var imagelist = dao.GetDeCaseImageList(ID);
            if (imagelist.Any())
            {
                for (int i = 0; i < imagelist.Count; i++)
                {
                    imagelist[i].ImageFile = ValidHelper.ChangeImageFill(imagelist[i].ImageFile);
                }
                r.DeCaseList = imagelist;
            }

            if (r != null)
            {
                result.IsSuccess = true;
                result.Message = "调用成功";
                result.ResultInfo = r;
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
