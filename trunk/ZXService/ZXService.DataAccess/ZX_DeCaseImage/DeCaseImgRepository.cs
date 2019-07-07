using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXService.DataContracts.ZX_DeCaseImage;

namespace ZXService.DataAccess.ZX_DeCaseImage
{
    public class DeCaseImgRepository : Repository<ZX_DeCaseImageEntity>
    {
        public List<ZX_DeCaseImageEntity> SelCaseImgInfo(ZX_DeCaseImageEntity model)
        {
            var selectfac = new SelectDeCaseImgFac();
            return base.Find<ZX_DeCaseImageEntity>(selectfac, new DataDeCaseImgFactory(), model);
        }

        public List<ZX_DeCaseImageEntity> SelCaseImgInfo()
        {
            var selectfac = new SelectDeCaseImgFac();
            return base.Find<ZX_DeCaseImageEntity>(selectfac, new DataDeCaseImgFactory(), new ZX_DeCaseImageEntity());
        }
    }
}
